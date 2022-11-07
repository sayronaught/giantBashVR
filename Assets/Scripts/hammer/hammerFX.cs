using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hammerFX : MonoBehaviour
{
    public float statsAoeDamage = 5f;
    public float statsAoeChargeKnockback = 25f;
    public float statsAoeBaseKnockback = 2500f;
    public float statsAoeRange = 5f;

    public bool statsCameraPointSpecial = false;
    [DrawIf("statsCameraPointSpecial", true)]
    public Vector3 statsCameraPoint;
    [DrawIf("statsCameraPointSpecial", true)]
    public float statsCameraPointDistance = 0.1f;
    [DrawIf("statsCameraPointSpecial", true)]
    public float statsCameraPointRepeatTimer = 10f;
    [DrawIf("statsCameraPointSpecial", true)]
    public AudioClip statsCameraPointAudioClip;
    [DrawIf("statsCameraPointSpecial", true)]
    public float statsCameraPointHealing = 0f;

    public GameObject prefabLightningExp;

    public ParticleSystem myTrail;
    public hammerController myHC;
    public hammerControllerEndlessMode myHcEm;
    public ParticleSystem myLightning;
    public AudioSource myLightningSFX;
    public gameController mainGC;
    public Text debugText;

    public AudioClip[] thunderclaps;

    public AudioSource myAS;
    private Rigidbody myRB;
    private _Settings mySettings;

    private void OnCollisionEnter(Collision collision)
    {
        float charge = 0f;
        if (myHC) charge = myHC.chargeLightning;
        if (myHcEm) charge = myHcEm.chargeLightning;
        //debugText.text = "Debug: Explosion collision";
        if (charge > 0f)
        {
            var explosion = Instantiate(prefabLightningExp);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = new Vector3(charge, charge, charge);
            var explosionAS = explosion.GetComponent<AudioSource>();
            if (charge > 5f )
            {
                explosionAS.clip = thunderclaps[3];
            } else if (charge > 3f) {
                explosionAS.clip = thunderclaps[2];
            } else if (charge > 1f) {
                explosionAS.clip = thunderclaps[1];
            } else {
                explosionAS.clip = thunderclaps[0];
            }
            explosionAS.Play();
            if (mainGC)
            {
                foreach (var target in mainGC.targetList)
                {
                    if (!(Vector3.Distance(target.transform.position, transform.position) <
                          (myHC.chargeLightning*0.5f))) continue;
                    if (target.isHit)
                    {
                        target.myRB.velocity = Vector3.zero;
                        target.myRB.rotation = Quaternion.identity;
                    }
                    else
                    {
                        mainGC.addPoints(target.pointValue);
                        target.isHit = true;
                        target.myRB.isKinematic = false;
                    }
                    var force = (target.transform.position - transform.position) * 100f;
                    target.gameObject.GetComponent<Rigidbody>().AddForce(force.normalized * (25f + (25f * charge)));
                }
            }
            if (myHcEm)
            {
                var Colliders = Physics.OverlapSphere(transform.position, (charge*statsAoeRange + 1f));
                for (int i = 0; i <= Colliders.Length - 1; i++)
                {
                    //print(Colliders[i].gameObject.transform.name);
                    dynamicEnemy enemy;
                    Colliders[i].gameObject.TryGetComponent<dynamicEnemy>(out enemy);
                    if ( enemy )
                    {
                        int newdam = (int)((charge* statsAoeDamage + 1f) - Vector3.Distance(transform.position, enemy.transform.position));
                        if ( newdam > 0 )
                        {
                            enemy.takeDamage(newdam);
                            var force = (enemy.transform.position - transform.position) * 100f;
                            enemy.gameObject.GetComponent<Rigidbody>().AddForce(force.normalized * (statsAoeBaseKnockback + (statsAoeChargeKnockback * charge)));
                        }
                    }
                }
            }
        }
        if ( myHC )
        {
            myHC.changeLightning(0f);
            myHC.supercharged = false;
        }
        if ( myHcEm )
        {
            myHcEm.changeLightning(0f);
            myHcEm.supercharged = false;
        }
        myLightning.Clear();
    }

    // Start is called before the first frame update
    private void Start()
    {
        myAS = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if ( permObj ) mySettings = permObj.GetComponent<_Settings>();
        if ( mySettings )
        { // spawn hammerskin
            var hammerSkin = Instantiate(mySettings.currentHammerSkin, Vector3.zero, Quaternion.identity);
            hammerSkin.transform.parent = GameObject.Find("HammerSkinSlot").transform;
            hammerSkin.transform.localPosition = mySettings.currentHammerSkin.transform.localPosition;
            hammerSkin.transform.localRotation = mySettings.currentHammerSkin.transform.localRotation;
            hammerSkin.transform.localScale = mySettings.currentHammerSkin.transform.localScale;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        myRB.velocity = Vector3.ClampMagnitude(myRB.velocity, 80);
        myAS.volume = myRB.velocity.magnitude * 0.05f;
        if (myHC)
        {
            if (myHC.beingSummoned())
            {
                if (myHC.beingHeld())
                {
                    myAS.volume = 0f;
                }
                else
                {
                    myAS.volume = myHC.summonSpeed() * 0.10f;
                }
            }
            else
            {
                myTrail.emissionRate = myRB.velocity.magnitude * 0.5f;
            }
            if (myHC.supercharged != false || !(transform.position.y > 10f)) return;
            myHC.supercharged = true;
            myHC.changeLightning(myHC.chargeLightning * 3f);
        }
    }
}
