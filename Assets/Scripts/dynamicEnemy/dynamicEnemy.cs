using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicEnemy : MonoBehaviour
{
    [System.Serializable]
    public class statList
    {
        public int pointValue = 1;
        public float mass = 1f;
        public float strength = 1f;
        public float speed = 1f;
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

    public void spawnSetDifficulty(float modifier)
    {
        stats.maxHealth *= modifier;
        Hitpoints = stats.maxHealth * Random.Range(0.8f, 1f);
        stats.strength *= modifier * 1.5f;
        stats.damageReduction *= modifier*2f;
        stats.maxSpeed *= Random.Range(0.75f, 1.25f)*modifier;
        myRB.mass = stats.mass * 2f;
        float size = Random.Range(.5f * stats.mass, 0.7f * stats.mass);
        transform.localScale = new Vector3(size, size, size);
        if (meleeWeapons.Length > 0)
            meleeWeapons[Random.Range(0, meleeWeapons.Length)].SetActive(true);
    }

    public void setWaypoints(Transform spawnpoint)
    {
        waypoints = spawnpoint.GetComponentsInChildren<Transform>();
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
        if (lastActionDelay > 0f) return; // still doing animation
        Transform target = waypoints[currentWaypoint];
        float distance = Vector3.Distance(transform.position, target.position);
        if ( distance > stats.meleeAttackRange)
        { // far away
            lookAt(target.position);
            myRB.MovePosition(Vector3.MoveTowards(transform.position, target.position, stats.maxSpeed * Time.fixedDeltaTime));
            stats.speed = stats.maxSpeed;
        } else { // close
            stats.speed = 0f;
            if (currentWaypoint < waypoints.Length-1) currentWaypoint++;
            else
            { // last waypoint, near player
                if ( lastActionDelay < 0f )
                {
                    //transform.LookAt(playerScript.transform.position);
                    lookAt(playerScript.transform.position);
                    lastActionDelay = stats.attackCooldown;
                    playRandomAnim(anims.attackMeleeLight);
                    playerScript.damagePlayer(stats.strength);
                }
            }
        }
        myAnim.SetFloat("CurrentSpeed", stats.speed);
        //myRB.MovePosition(pos);
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
        myAnim.Play(animlist[Random.Range(0, animlist.Length)]);
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
