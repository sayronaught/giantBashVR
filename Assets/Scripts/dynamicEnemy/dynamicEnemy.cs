using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicEnemy : MonoBehaviour
{
    public monsterList killType;
        
    [System.Serializable]
    public enum mobTypes
    {
        Melee,
        Ranged,
        Caster
    };
    [Header("Basic Mob Type")]
    public mobTypes mobtype;

    public bool ranged = false;

    [DrawIf("mobtype", mobTypes.Ranged)]
    public GameObject missilePrefabs;

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
        public float animSpeedMultiplier = 1f;
        public float dropableClothesForce = 300f;
        public float dropableClothesRotation = 150f;
        public float dropableClothesPercentage = 10f;
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
        public string[] attackMelee;
        public string[] attackRange;
        public string[] conversations;
        public string[] taunts;
        public string[] surrenders;
    }
    public animList anims;
    public bool animIdle = true;
    public GameObject instantDeathPrefab;
    public GameObject damageTextPrefab;
    public GameObject deathLootPrefab;
    public Transform hitbar;

    [System.Serializable]
    public class soundList
    {
        public AudioClip[] steps;
        public AudioClip[] idleChatter;
        public float chatterPercentageChance = 0.01f;
        public AudioClip[] hurt;
        public AudioClip[] death;
        public AudioClip[] attackMelee;
        public float attackMeleePercentageChance = 20f;
        public AudioClip[] attackRanged;
        public float attackRangedPercentageChance = 20f;
        public AudioClip[] taunts;
        public AudioClip[] surrenders;
    }
    public soundList sounds;

    [System.Serializable]
    public enum moveTypes
    {
        MeleeAttack,
        ThrowMissile,
        AreaHeal
    };

    [System.Serializable]
    public class combatMovesList
    {
        [Header("Constant Stats")]
        [Tooltip("What is the move called?")]
        public string name;
        [Tooltip("The type determines everything")]
        public moveTypes moveType = moveTypes.MeleeAttack;
        [Tooltip("List of Animations")]
        public string[] anims;
        [Tooltip("List of Speech/Samples")]
        public AudioClip[] speech;
        [Tooltip("Percantage change to play a sample")]
        public float speechPercentageChance = 20f;
        [Space(20)]
        [Header("Type Stats")]
        [DrawIf("moveType", moveTypes.ThrowMissile)]
        public GameObject missilePrefab;
        
    }
    [Header("Combat Moves")]
    public combatMovesList[] combatMoves;



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

    public List<GameObject> dropableArmor;

    private int currentWaypoint;
    private Transform finalWaypoint;
    private Transform temporaryTarget;
    private Vector3 targetPostCalc;

    private float lastActionDelay = 0f;
    private float showDamageDelay = 0f;
    private float showDamageTaken = 0f;

    private Animator myAnim;
    private AudioSource mySound;
    private Rigidbody myRB;
    private _Settings mySettings;

    private Vector3 targetRandomizer(Vector3 thisTarget, float variation)
    {
        thisTarget += new Vector3(Random.Range(-variation, variation), Random.Range(-variation, variation), Random.Range(-variation, variation));
        return thisTarget;
    }

    private void dropArmorPiece()
    {
        if (dropableArmor.Count == 0) return;
        for (var i = dropableArmor.Count - 1; i > -1; i--)
        {
            if (!dropableArmor[i])
                dropableArmor.RemoveAt(i);
        }
        dropableArmor.RemoveAll(GameObject => GameObject == null);
        if (dropableArmor.Count == 0) return;
        int rnd = Random.Range(0, dropableArmor.Count - 1);
        GameObject armor = dropableArmor[rnd];
        dropableArmor.Remove(armor);
        GameObject newArmor = Instantiate(armor, armor.transform.position, armor.transform.rotation);
        var rb = newArmor.AddComponent<Rigidbody>();
        var force = targetRandomizer(Vector3.zero, 1f) + (Vector3.up*5f);
        rb.AddForce(force.normalized * (stats.dropableClothesForce*Random.Range(0.5f,1.5f)));
        rb.AddTorque(targetRandomizer(Vector3.zero, stats.dropableClothesRotation));
        newArmor.AddComponent<MeshCollider>();
        var skin = newArmor.GetComponent<SkinnedMeshRenderer>();
        var newmeshrender = newArmor.AddComponent<MeshRenderer>();
        var newmeshfilter = newArmor.AddComponent<MeshFilter>();
        newmeshfilter.mesh = skin.sharedMesh;
        newmeshrender.materials = skin.materials;
        Destroy(skin);
        Destroy(newArmor, 5f);
        Destroy(armor);
    }
    private void dropGold()
    {

        GameObject gold = Instantiate(deathLootPrefab, transform.position, Quaternion.identity);
        var rb = gold.GetComponent<Rigidbody>();
        var force = targetRandomizer(Vector3.zero, 1f) + (Vector3.up * 5f);
        rb.AddForce(force.normalized * (stats.dropableClothesForce * Random.Range(0.5f, 1.5f)));
        Destroy(gold, 5f);
    }
    public void spawnSetDifficulty(float modifier)
    {
        stats.maxHealth *= modifier*3;
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
    public void animEventMeleeAttack()
    {
        playerScript.damagePlayer(stats.strength);
    }
    public void animEventReleaseProjectile()
    {
        Debug.Log("release missle");
        if (!heldMissile) return;
        Debug.Log("found");
        heldMissile.transform.SetParent(null);
        var missileScript = heldMissile.GetComponent<genericMissile>();
        //var spellScript = heldMissile.GetComponent<genericSpell>();
        if ( missileScript )
        {
            missileScript.target = playerScript.transform.position + new Vector3(Random.Range(-.5f, .5f),0f, Random.Range(-.5f, .5f));
            missileScript.flying = true;
        }
        heldMissile.GetComponent<AudioSource>().Play();
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
        if (sounds.death.Length > 0) playRandomSound(sounds.death, true);
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
        myAnim.SetFloat("CurrentSpeed", speedAnim*stats.animSpeedMultiplier);
        stats.speed = stats.maxSpeed * speedAnim;
    }
    private void showDamage()
    {
        if ( showDamageTaken < 1f )
        {
            showDamageTaken = 0f;
            return;
        }
        if (damageTextPrefab && showDamageDelay <= 0f)
        {
            var spawn = Instantiate(damageTextPrefab, hitbar.position, Quaternion.identity);
            //spawn.transform.rotation = hitbar.rotation;
            spawn.transform.LookAt(playerScript.transform.position + Vector3.up);
            spawn.transform.RotateAround(spawn.transform.position, Vector3.up, 180);
            int damage = (int)showDamageTaken;
            spawn.GetComponent<damageText>().setText(damage.ToString());
            showDamageTaken = 0f;
            showDamageDelay = 0.5f;
        }
    }
    public void takeDamage(float dam)
    {
        if (dam <= 0f) return;
        if (dam / stats.maxHealth > Random.value) playRandomSound(sounds.hurt, true);
        Hitpoints -= dam;
        if ( anims.hurt.Length > 0 )
        {
            myAnim.SetFloat("CurrentSpeed", 0);
            stats.speed = 0;
            playRandomAnim(anims.hurt);
        }
        if (hitbar)
        {
            showDamageTaken += dam;
            showDamage();
            float width = stats.hitbarMaxWidth * (Hitpoints / stats.maxHealth);
            myAnim.SetFloat("Wounded", 1f- (Hitpoints / stats.maxHealth));
            if (width <= 0f) hitbar.gameObject.SetActive(false);
            hitbar.localScale = new Vector3(width, hitbar.localScale.y, hitbar.localScale.z);
        }
        if (mySettings)
        {
            mySettings.damageDone += (int)dam;
            if ((int)dam > mySettings.damageHighest) mySettings.damageHighest = (int)dam;
        }
        if (Hitpoints < 0f)
        {
            if (mySettings) mySettings.jotunsBashed++;
            playerScript.addPoints(stats.pointValue);
            if ( stats.pointValue > 0)
            {
                for ( int i = 0; i < stats.pointValue; i++) dropGold();
            }
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
        if ( dam > stats.maxHealth * (stats.dropableClothesPercentage*0.01f)) dropArmorPiece();
    }

    void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
        mySound = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
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
        showDamageDelay -= Time.deltaTime;
        if (showDamageTaken > 0f && showDamageDelay <= 0f) showDamage();
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
                playRandomAnim(anims.attackRange);
                if (heldMissile) return;
                playerScript.damagePlayer(stats.strength);
                var missile = Instantiate(missileWeaponPrefabs[Random.Range(0,missileWeaponPrefabs.Length)]);
                missile.transform.position = hand.position;
                missile.transform.rotation = hand.rotation;
                missile.transform.SetParent(hand);
                heldMissile = missile;
                Debug.Log("spawned missile");
                if (Random.value * 100f < sounds.attackRangedPercentageChance) playRandomSound(sounds.attackRanged);
            }
            return; // no melee attacks or navigation after ranged attacks
        }
        if ( distance > stats.meleeAttackRange) // melee attacks and melee stats for navigation through waypoints
        { // far away
            if ( travelSpeed == 0f ) travelSpeed = Random.Range(0.25f, 1f);
            lookAt(target.position);
            myRB.MovePosition(Vector3.MoveTowards(transform.position, target.position, stats.speed * Time.fixedDeltaTime*2f));
            changeSpeed(travelSpeed);
            if (Random.value * 100f < sounds.chatterPercentageChance) playRandomSound(sounds.idleChatter);
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
                    playRandomAnim(anims.attackMelee);
                    if ( Random.value*100f < sounds.attackMeleePercentageChance) playRandomSound(sounds.attackMelee);
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
    }

    void playRandomSound( AudioClip[] acList , bool doOverride = false)
    {
        if ( acList.Length < 1 ) return;
        if (!doOverride && mySound.isPlaying) return;
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
