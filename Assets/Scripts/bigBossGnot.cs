using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bigBossGnot : MonoBehaviour
{
    public bool isAwake = false;
    public float Hitpoints = 281f;
    public RawImage hitPointBar;
    public hammerController mainHC;

    public AudioClip clipRoar;
    public AudioClip[] footSteps;

    public Transform posThrow1;
    public Transform posThrow2;
    public Transform posFinal;
    public Transform posProjectileSpawn;
    public Transform throwTarget1;
    public GameObject throwTarget1GO;
    public Transform throwTarget2;
    public GameObject throwTarget2GO;
    public GameObject heldProjectile;
    public GameObject flyingProjectile;
    public GameObject prefabProjectile;
    public bool reeling = false;

    private AudioSource myAS;
    private Animator myAnim;
    public float distance;
    public int bossStage = 0; // 0 = wake up, 1 = advance to throw1, 2= throw1, 3 = advance to throw2, 4= throw2, 5 = advance fully, 6 = attack player

    public void playRoar()
    {
        myAS.clip = clipRoar;
        myAS.pitch = 0.5f;
        myAS.Play();
    }
    public void playTakeStep()
    {
        myAS.clip = footSteps[Random.Range(0, footSteps.Length)];
        myAS.pitch = 1f;
        myAS.Play();
    }
    public void eventDoneRoar()
    {
        bossStage = 1;
    }
    public void eventReelingRecover()
    {
        reeling = false;
    }
    public void eventPickupProjectile()
    {
        heldProjectile = Instantiate(prefabProjectile, Vector3.zero, Quaternion.identity) as GameObject;
        heldProjectile.transform.SetParent(posProjectileSpawn);
        heldProjectile.transform.localScale=new Vector3(0.1f, 0.1f, 0.1f);
        heldProjectile.transform.localPosition = Vector3.zero;
        heldProjectile.transform.localRotation = Quaternion.identity;
    }
    public void eventReleaseProjectile()
    {
        heldProjectile.transform.SetParent(null);
        flyingProjectile = heldProjectile;
        heldProjectile = null;
    }
    public void wakeUp()
    {
        isAwake = true;
        myAnim.SetBool("wakeup", true);
        myAnim.SetTrigger("wakeup");
        bossStage = 0;
    }
    public void takeDamage(float damage)
    {
        Hitpoints -= damage;
        hitPointBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Hitpoints, 70);
        if (damage > 5f)
        {
            myAnim.SetTrigger("takedamage");
            reeling = true;
            myAnim.SetBool("walking", false);
            myAnim.SetBool("running", false);
            myAnim.SetBool("throw", false);
            myAnim.SetBool("dance", false);
            if (heldProjectile) Destroy(heldProjectile);
        }
        myAnim.SetFloat("hitpoints", Hitpoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector3.Distance(transform.position, currentTarget.position);
        //myAnim.SetFloat("moveSpeed", distance);
        if ( !reeling && isAwake )
        {
            switch (bossStage)
            {
                case 1: // advance to throw 1
                    myAnim.SetBool("walking", true);
                    transform.position = Vector3.MoveTowards(transform.position, posThrow1.position, Time.deltaTime * 1.5f);
                    transform.LookAt(posThrow1.position);
                    distance = Vector3.Distance(transform.position, posThrow1.position);
                    if (distance < 1f)
                    {
                        myAnim.SetBool("walking", false);
                        bossStage = 2;
                    }
                    break;
                case 2: // throw1
                    transform.LookAt(throwTarget1.transform.position);
                    if ( !flyingProjectile )
                    {
                        myAnim.SetBool("throw", true);
                        myAnim.SetBool("dance", true);
                    } else {
                        myAnim.SetBool("throw", false);
                        distance = Vector3.Distance(flyingProjectile.transform.position, throwTarget1.transform.position);
                        flyingProjectile.transform.position = Vector3.MoveTowards(flyingProjectile.transform.position, throwTarget1.transform.position + (Vector3.up*distance*0.45f), Time.deltaTime * 5f);
                        if ( distance < 1f)
                        {
                            throwTarget1GO.SetActive(false);
                            Destroy(flyingProjectile);
                            bossStage = 3;
                        }
                    }
                    break;
                case 3: // run to throw 2
                    myAnim.SetBool("walking", true);
                    myAnim.SetBool("running", true);
                    transform.position = Vector3.MoveTowards(transform.position, posThrow2.position, Time.deltaTime * 3f);
                    transform.LookAt(posThrow2.position);
                    distance = Vector3.Distance(transform.position, posThrow2.position);
                    if (distance < 1f)
                    {
                        myAnim.SetBool("walking", false);
                        myAnim.SetBool("running", false);
                        bossStage = 4;
                    }
                    break;
                case 4: // throw 2
                    break;
                case 5: // walk to final
                    break;
                case 6: // attack player
                    break;
                default: // wake up
                    break;
            }
        }
    }
}
