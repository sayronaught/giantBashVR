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
        public float meleeAttackRange = 1.5f;
        public float heavyAttackCooldown = 2f;
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

    public GameObject[] meleeWeapons;
    public GameObject[] rangeWeapons;
    public GameObject[] missileWeaponPrefabs;

    public float Hitpoints = 100f;

    public Transform[] waypoints;

    public EndlessPlayerScript playerScript;

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
        Hitpoints *= modifier;
        stats.strength *= modifier;
        stats.damageReduction *= modifier;
        stats.maxSpeed *= Random.Range(0.75f, 1.25f);
    }

    public void setWaypoints(Transform spawnpoint)
    {
        waypoints = spawnpoint.GetComponentsInChildren<Transform>();
    }

    public void animEventBackToIdle()
    {
        animIdle = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Hammer") return;
        var dam = (collision.gameObject.GetComponent<hammerControllerEndlessMode>().chargeLightning * 3f + 5f) - stats.damageReduction;
        Hitpoints -= (dam + (collision.rigidbody.velocity.magnitude * 0.2f));
        if ( hitbar )
        {
            hitbar.localScale = new Vector3( stats.hitbarMaxWidth*(Hitpoints/stats.maxHealth), hitbar.localScale.y,hitbar.localScale.z);
        }
        if (Hitpoints < 0f)
        {
            if ( instantDeathPrefab && anims.death.Length > 0 )
            { // both model and deathsplat present. 50% chance for each
                if ( Random.Range(0,1) < 0.5f)
                {
                    var blood = Instantiate(instantDeathPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    blood.transform.position = transform.position;
                    Destroy(gameObject);
                } else {
                    playRandomAnim(anims.death);
                    Destroy(gameObject, 10);
                    playerScript.addPoints(stats.pointValue);
                }
            } else if ( instantDeathPrefab )
            { // remove model and add bloodsplat or other prefab
                var blood = Instantiate(instantDeathPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                blood.transform.position = transform.position;
                Destroy(gameObject);
            } else { // play death anim
                playRandomAnim(anims.death);
                Destroy(gameObject, 10);
                playerScript.addPoints(stats.pointValue);
            }
            playerScript.addPoints(stats.pointValue);
        }   
    }

    void Awake()
    {
        Hitpoints = stats.maxHealth;
        myAnim = GetComponentInChildren<Animator>();
        mySound = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
        float size = Random.Range(.8f*stats.mass, 1.2f*stats.mass);
        transform.localScale = new Vector3(size, size, size);
        if (meleeWeapons.Length > 0)
            meleeWeapons[Random.Range(0, meleeWeapons.Length)].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
                    lastActionDelay = stats.heavyAttackCooldown;
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
}
