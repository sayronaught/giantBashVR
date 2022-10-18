using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bigBossGnot : MonoBehaviour
{
    public bool isAwake = false;
    public float Hitpoints = 281f;
    public RawImage hitPointBar;
    public hammerController mainHC;
    public gameController mainGC;

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
    public int bossStage = 0; // 0 = wake up, 1 = advance to throw1, 2= throw1, 3 = advance to throw2, 4= throw2, 5 = dance until spawners destroyed, 6 = advance fully, 7 = attack player
    public bool spawnersDestroyed = false; // when they are destroyed, he roars and runs to kill player
    
    private static readonly int Reset1 = Animator.StringToHash("reset");
    private static readonly int Wakeup = Animator.StringToHash("wakeup");
    private static readonly int Takedamage = Animator.StringToHash("takedamage");
    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Property = Animator.StringToHash("throw");
    private static readonly int Dance = Animator.StringToHash("dance");
    private static readonly int Hitpoints1 = Animator.StringToHash("hitpoints");

    private _Settings mySettings;

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
    public void eventReeling()
    {
        reeling = true;
    }
    public void eventReelingRecover()
    {
        reeling = false;
    }
    public void eventPickupProjectile()
    {
        heldProjectile = Instantiate(prefabProjectile, Vector3.zero, Quaternion.identity) as GameObject;
        heldProjectile.transform.SetParent(posProjectileSpawn);
        heldProjectile.transform.localScale=new Vector3(0.0015f, 0.0015f, 0.0015f);
        heldProjectile.transform.localPosition = Vector3.zero;
        heldProjectile.transform.localRotation = Quaternion.identity;
        myAnim.SetBool(Property, false);
    }
    public void eventReleaseProjectile()
    {
        heldProjectile.transform.SetParent(null);
        flyingProjectile = heldProjectile;
        heldProjectile = null;
    }
    public void animEventReleaseProjectile()
    {
        eventReleaseProjectile();
    }
    public void Reset()
    {
        isAwake = false;
        myAnim.SetTrigger(Reset1);
        transform.position = new Vector3(-4.01f, 0.48f, 55.92f);
        transform.LookAt(throwTarget1.position);
    }
    public void wakeUp()
    {
        isAwake = true;
        myAnim.SetBool(Wakeup, true);
        myAnim.SetTrigger(Wakeup);
        bossStage = 0;
    }
    public void takeDamage(float damage)
    {
        if (Hitpoints <= 0f) return;
        Hitpoints -= damage;
        hitPointBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Hitpoints, 65);
        if (damage > 30f)
        {
            myAnim.SetTrigger(Takedamage);
            reeling = true;
            myAnim.SetBool(Walking, false);
            myAnim.SetBool(Running, false);
            myAnim.SetBool(Property, false);
            myAnim.SetBool(Dance, false);
            if (heldProjectile) Destroy(heldProjectile);
        }
        if ( damage > 10f)
        {
            mainGC.addPoints((int)(damage * 0.1f));
        }
        if ( mySettings )
        {
            mySettings.damageDone += (int)damage;
            if ((int)damage > mySettings.damageHighest) mySettings.damageHighest = (int)damage;
        }
        if ( Hitpoints <= 0f )
        {
            if (mySettings) mySettings.jotunsBashed++;
            mainGC.gameStage = 4;
            mainGC.gamestageCountDown = 30f;
            //SceneManager.LoadScene(0);
            //mainGC.uiVinder.SetActive(true);
        }
        myAnim.SetFloat(Hitpoints1, Hitpoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hitpoints <= 0f) return;
        if ( flyingProjectile )
        {
            switch (bossStage)
            {
                case 2:
                    distance = Vector3.Distance(flyingProjectile.transform.position, throwTarget1.position);
                    flyingProjectile.transform.position = Vector3.MoveTowards(flyingProjectile.transform.position, throwTarget1.position + (Vector3.up * (distance * 0.45f)), Time.deltaTime * 5f);
                    if (distance < 1f)
                    {
                        throwTarget1GO.SetActive(false);
                        Destroy(flyingProjectile);
                        bossStage = 3;
                    }
                    break;
                case 4:
                    distance = Vector3.Distance(flyingProjectile.transform.position, throwTarget2.position);
                    flyingProjectile.transform.position = Vector3.MoveTowards(flyingProjectile.transform.position, throwTarget2.position + (Vector3.up * (distance * 0.45f)), Time.deltaTime * 5f);
                    if (distance < 1f)
                    {
                        throwTarget2GO.SetActive(false);
                        Destroy(flyingProjectile);
                        bossStage = 5;
                        mainGC.releaseTheSmurfs();
                    }
                    break;
                default:
                    distance = Vector3.Distance(flyingProjectile.transform.position, mainGC.posGameOn.position);
                    flyingProjectile.transform.position = Vector3.MoveTowards(flyingProjectile.transform.position, mainGC.posGameOn.position + (Vector3.up * distance * 0.45f), Time.deltaTime * 5f);
                    if (distance < 0.5f)
                    {
                        // something bad
                        Destroy(flyingProjectile);
                    }
                    break;
            }
        }
        //distance = Vector3.Distance(transform.position, currentTarget.position);
        //myAnim.SetFloat("moveSpeed", distance);
        if (reeling || !isAwake) return;
        switch (bossStage)
        {
            case 1: // advance to throw 1
                myAnim.SetBool(Walking, true);
                transform.position = Vector3.MoveTowards(transform.position, posThrow1.position, Time.deltaTime * 1.5f);
                transform.LookAt(posThrow1.position);
                distance = Vector3.Distance(transform.position, posThrow1.position);
                if (distance < 1f)
                {
                    myAnim.SetBool(Walking, false);
                    bossStage = 2;
                }
                break;
            case 2: // throw1
                transform.LookAt(throwTarget1.position);
                myAnim.SetBool(Property, !flyingProjectile);
                break;
            case 3: // run to throw 2
                myAnim.SetBool(Walking, true);
                myAnim.SetBool(Running, true);
                transform.position = Vector3.MoveTowards(transform.position, posThrow2.position, Time.deltaTime * 3f);
                transform.LookAt(posThrow2.position);
                distance = Vector3.Distance(transform.position, posThrow2.position);
                if (distance < 1f)
                {
                    myAnim.SetBool(Walking, false);
                    myAnim.SetBool(Running, false);
                    bossStage = 4;
                }
                break;
            case 4: // throw 2
                transform.LookAt(throwTarget2.position);
                myAnim.SetBool(Property, !flyingProjectile);
                break;
            case 5: // dance until spawners destroyed
                myAnim.SetBool(Dance, true);
                transform.LookAt(posFinal.position);
                if (spawnersDestroyed)
                {
                    //myAnim.SetTrigger("roar");
                    myAnim.SetBool(Dance, false);
                    bossStage = 6;
                }
                break;
            case 6: // walk to final
                myAnim.SetBool(Walking, true);
                transform.position = Vector3.MoveTowards(transform.position, posFinal.position, Time.deltaTime * 1.5f);
                transform.LookAt(posFinal.position);
                distance = Vector3.Distance(transform.position, posFinal.position);
                if (distance < 1f)
                {
                    myAnim.SetBool(Walking, false);
                    bossStage = 7;
                }
                break;
            case 7: // attack player
                transform.LookAt(mainGC.posGameOn.position);
                myAnim.SetBool(Property, !flyingProjectile);
                break;
            default: // wake up
                break;
        }
    }
}
