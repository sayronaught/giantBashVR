using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicEnemy : MonoBehaviour
{
    public monsterList killType;

    [System.Serializable]
    public class statList
    {
        public int pointValue = 1;
        public float mass = 1f;
        public float strength = 1f;
        public float speed = 0f;
        public float maxSpeed = 1f;
        public float jumpForce = 1000f;
        public float damageReduction = 1f;
        public float maxHealth = 100f;
        public float meleeAttackRange = 5f;
        public float rangedAttackRange = 5f;
        public float attackCooldown = 2f;
        public float hitbarMaxWidth = 0.1f;
    }
    public statList stats;
    private float speedUp = 1.1f;
    private float speedDown = 0.9f;
    private float speedAnim = 0f;
    private float travelSpeed = 0f;

    [System.Serializable]
    public class animList
    {
        public string[] idles;
        public string[] hurt;
        public string[] death;
        public string[] attackMeleeLight;
        public string[] attackMeleeheavy;
        public string[] attackRangeLight;
        public string[] attackRangeHeavy;
        public string[] conversations;
        public string[] taunts;
        public string[] surrenders;
    }
    public animList anims;
    public bool animIdle = true;
    public GameObject instantDeathPrefab;
    public GameObject damageTextPrefab;
    public Transform hitbar;

    [System.Serializable]
    public class soundList
    {
        public AudioClip[] steps;
        public AudioClip[] idleChatter;
        public AudioClip[] hurt;
        public AudioClip[] death;
        public AudioClip[] attackLight;
        public AudioClip[] attackHeavy;
        public AudioClip[] taunts;
        public AudioClip[] surrenders;
    }
    public soundList sounds;

    public AudioSource beingHitAS;

    public GameObject[] meleeWeapons;
    public GameObject[] rangeWeapons;
    public GameObject[] missileWeaponPrefabs;
    public bool isRanged = false;
    public Transform hand;
    public GameObject heldMissile;

    public float Hitpoints = 100f;
    public bool isAlive = true;

    public Transform[] waypoints;

    public EndlessPlayerScript playerScript;
    public EffectBank myEB;

    private int currentWaypoint;
    private Transform finalWaypoint;
    private Transform temporaryTarget;
    private Vector3 targetPostCalc;

    private float lastActionDelay = 0f;

    private Animator myAnim;
    private AudioSource mySound;
    private Rigidbody myRB;

    private Vector3 targetRandomizer(Vector3 thisTarget, float variation)
    {
        thisTarget += new Vector3(Random.Range(-variation, variation), Random.Range(-variation, variation), Random.Range(-variation, variation));
        return thisTarget;
    }

    public void spawnSetDifficulty(float modifier)
    {
        stats.maxHealth *= modifier;
        Hitpoints = stats.maxHealth * Random.Range(0.8f, 1f);
        stats.strength *= modifier * 1.5f;
        stats.damageReduction *= modifier*2f;
        stats.maxSpeed *= Random.Range(0.75f, 1.25f)*modifier;
        speedUp = Random.Range(1.01f, 1.05f);
        speedDown = Random.Range(0.95f, 0.99f);
        travelSpeed = Random.Range(0.25f, 1f);
        if ( !myRB ) myRB = GetComponent<Rigidbody>();
        myRB.mass = stats.mass * 6f;
        float size = Random.Range(.5f * stats.mass, 0.7f * stats.mass);
        transform.localScale = new Vector3(size, size, size);
        myAnim.SetFloat("variation", Random.Range(-1f,1f));
        if (meleeWeapons.Length > 0)
            meleeWeapons[Random.Range(0, meleeWeapons.Length)].SetActive(true);
    }

    public void setWaypoints(Transform spawnpoint)
    {
        waypoints = spawnpoint.GetComponentsInChildren<Transform>();
    }
    public void animEventReleaseProjectile()
    {
        heldMissile.transform.SetParent(null);
        heldMissile.GetComponent<enemyThrowingAxe>().isThrown = true;
        heldMissile.GetComponent<AudioSource>().Play();
        heldMissile.GetComponent<BoxCollider>().enabled = true;
        var rb = heldMissile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(((targetRandomizer(playerScript.transform.position, 1f) + (Vector3.up * 5f)) - heldMissile.transform.position) * 50f);
        rb.maxAngularVelocity = Mathf.Infinity;
        rb.AddRelativeTorque(Vector3.up * 4f);
        heldMissile = null;
    }
    public void animEventBackToIdle()
    {
        animIdle = true;
    }
    private void deathSplat()
    {
        var blood = Instantiate(instantDeathPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        blood.transform.position = transform.position;
        Destroy(gameObject);
    }
    private void deathAnim()
    {
        playRandomAnim(anims.death);
        Destroy(gameObject, 10);
        Destroy(myRB);
        Destroy(GetComponent<BoxCollider>());
        isAlive = false;
    }
    private void changeSpeed(float desiredSpeed)
    {
        if ( desiredSpeed > speedAnim )
        { // speedUp
            speedAnim = speedAnim * speedUp + 0.00001f;
            if (speedAnim > desiredSpeed) speedAnim = desiredSpeed;
        }
        if (desiredSpeed < speedAnim)
        { // slowDown
            speedAnim = speedAnim * speedDown- 0.00001f;
            if (speedAnim < desiredSpeed) speedAnim = desiredSpeed; 
        }
        myAnim.SetFloat("CurrentSpeed", speedAnim);
        stats.speed = stats.maxSpeed * speedAnim;
    }
    public void takeDamage(float dam)
    {
        Hitpoints -= dam;
        if (hitbar)
        {
            if (damageTextPrefab)
            {
                var spawn = Instantiate(damageTextPrefab, hitbar.position, Quaternion.identity);
                //spawn.transform.rotation = hitbar.rotation;
                spawn.transform.LookAt(playerScript.transform.position+Vector3.up);
                spawn.transform.RotateAround(spawn.transform.position, Vector3.up, 180);
                int damage = (int)dam;
                spawn.GetComponent<damageText>().setText(damage.ToString());
            }
            float width = stats.hitbarMaxWidth * (Hitpoints / stats.maxHealth);
            myAnim.SetFloat("Wounded", 1f- (Hitpoints / stats.maxHealth));
            if (width <= 0f) hitbar.gameObject.SetActive(false);
            hitbar.localScale = new Vector3(width, hitbar.localScale.y, hitbar.localScale.z);
        }
        if (Hitpoints < 0f)
        {
            playerScript.addPoints(stats.pointValue);
            if (instantDeathPrefab && anims.death.Length > 0)
            { // both model and deathsplat present. 20% chance for splat
                if (Random.Range(0f, 1f) < 0.2f)
                {
                    deathSplat();
                }
                else
                {
                    deathAnim();
                }
            }
            else if (instantDeathPrefab)
            { // remove model and add bloodsplat or other prefab
                deathSplat();
            }
            else
            { // play death anim
                deathAnim();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Hammer") return;
        if (!isAlive) return;
        var dam = (collision.gameObject.GetComponent<hammerControllerEndlessMode>().chargeLightning * 3f + 5f) - stats.damageReduction;
        dam += (collision.rigidbody.velocity.magnitude * 0.2f);
        takeDamage(dam);
        playRandomBeingHitSound(myEB.sounds.hammerHitMeat);
    }

    void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
        mySound = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        // look at target
        if (temporaryTarget)
            transform.LookAt(temporaryTarget);
        else
            transform.LookAt(finalWaypoint);
        lastActionDelay -= Time.deltaTime;
        if (lastActionDelay > 0f || waypoints.Length < 1) return; // still doing animation
        Transform target = waypoints[currentWaypoint];
        float distance = Vector3.Distance(transform.position, target.position);
        if (isRanged && Vector3.Distance(transform.position,playerScript.transform.position) < stats.rangedAttackRange)
        { // is inside ranged attackrange
            changeSpeed(0f);
            if (lastActionDelay < 0f && stats.speed < 0.025f)
            {
                lookAt(playerScript.transform.position);
                lastActionDelay = stats.attackCooldown;
                playRandomAnim(anims.attackRangeLight);
                playerScript.damagePlayer(stats.strength);
                var missile = Instantiate(missileWeaponPrefabs[Random.Range(0,missileWeaponPrefabs.Length)]);
                missile.transform.position = hand.position;
                missile.transform.rotation = hand.rotation;
                missile.transform.SetParent(hand);
                heldMissile = missile;
            }
            return; // no melee attacks or navigation after ranged attacks
        }
        if ( distance > stats.meleeAttackRange) // melee attacks and melee stats for navigation through waypoints
        { // far away
            if ( travelSpeed == 0f ) travelSpeed = Random.Range(0.25f, 1f);
            lookAt(target.position);
            myRB.MovePosition(Vector3.MoveTowards(transform.position, target.position, stats.speed * Time.fixedDeltaTime));
            changeSpeed(travelSpeed);
        } else { // close
            if (currentWaypoint < waypoints.Length - 1)
            { // More waypoints to go
                currentWaypoint++;
                travelSpeed = Random.Range(0.25f, 1f);
            } else { // last waypoint, near player
                changeSpeed(0f);
                if (lastActionDelay < 0f && stats.speed < 0.025f)
                {
                    lookAt(playerScript.transform.position);
                    lastActionDelay = stats.attackCooldown;
                    playRandomAnim(anims.attackMeleeLight);
                    playerScript.damagePlayer(stats.strength);
                }
            }
        }
    }

    void lookAt( Vector3 position)
    {
        targetPostCalc = position;
        targetPostCalc.y = transform.position.y;
        myRB.MoveRotation(Quaternion.LookRotation(targetPostCalc - transform.position));
    }

    void playRandomAnim( string[] animlist )
    {
        if ( animlist.Length < 1 ) return;
        myAnim.CrossFade(animlist[Random.Range(0, animlist.Length)], 0.3f);
        //Changed to CrossFade instead of Play to add blending between animations
        //myAnim.Play(animlist[Random.Range(0, animlist.Length)]);
    }

    void playRandomSound( AudioClip[] acList )
    {
        if ( acList.Length < 1 ) return;
        mySound.clip = acList[Random.Range(0, acList.Length)];
        mySound.Play();
    }

    void playRandomBeingHitSound(AudioClip[] acList)
    {
        if (acList.Length < 1) return;
        beingHitAS.clip = acList[Random.Range(0, acList.Length)];
        beingHitAS.Play();
    }
}
