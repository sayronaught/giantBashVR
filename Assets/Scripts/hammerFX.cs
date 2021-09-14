using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hammerFX : MonoBehaviour
{
    public GameObject prefabLightningExp;

    public ParticleSystem myTrail;
    public hammerController myHC;
    public ParticleSystem myLightning;
    public AudioSource myLightningSFX;
    public hammerController mainHC;
    public gameController mainGC;
    public Text debugText;

    public AudioClip[] thunderclaps;

    public AudioSource myAS;
    private Rigidbody myRB;

    private void OnCollisionEnter(Collision collision)
    {
        debugText.text = "Debug: Explosion collision";
        if (mainHC.chargeLightning > 0f)
        {
            debugText.text = "Debug: Explosion collision charge";
            var explosion = Instantiate(prefabLightningExp);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = new Vector3(mainHC.chargeLightning, mainHC.chargeLightning, mainHC.chargeLightning);
            var explosionAS = explosion.GetComponent<AudioSource>();
            if ( mainHC.chargeLightning > 5f )
            {
                explosionAS.clip = thunderclaps[3];
            } else if (mainHC.chargeLightning > 3f) {
                explosionAS.clip = thunderclaps[2];
            } else if (mainHC.chargeLightning > 1f) {
                explosionAS.clip = thunderclaps[1];
            } else {
                explosionAS.clip = thunderclaps[0];
            }
            explosionAS.Play();
            debugText.text = "Debug: Explosion collision charge 2";
            /*int valid = 0;
            debugText.text = "Debug: Explosion collision charge 2,1 "+mainGC.targetList.Count.ToString();
            foreach ( var target in mainGC.targetList )
            //for ( int i = mainGC.targetList.Count-1; i >= 0; i--)
            //mainGC.targetList.ForEach(var target)
            {
                debugText.text = "Debug: Explosion collision charge 3";
                if ( Vector3.Distance(target.transform.position, transform.position) < (mainHC.chargeLightning*25f+3f))
                {
                    debugText.text = "Debug: Explosion collision charge 4";
                    valid++;/*
                    if (target.isHit)
                    {
                        target.myRB.velocity = Vector3.zero;
                        target.myRB.rotation = Quaternion.identity;
                    } else {
                        mainGC.addPoints(target.pointValue);
                        target.isHit = true;
                        target.myRB.isKinematic = false;
                    }*//*
                    debugText.text = "Debug: Explosion collision charge 5";
                    Vector3 force = transform.position - target.transform.position;
                    myRB.AddForce(force.normalized * (50f*mainHC.chargeLightning));
                }
            }
            debugText.text = "Debug: Explosion " + mainGC.targetList.Count.ToString() + " / " + valid.ToString() + " / " + mainHC.chargeLightning.ToString();
            */
        }
        mainHC.changeLightning(0f);
        mainHC.supercharged = false;
        myLightning.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myAS.volume = myRB.velocity.magnitude * 0.1f;
        if (myHC.beingSummoned())
        {
            if (myHC.beingHeld())
            {
                myAS.volume = 0f;
            } else
            {
                myAS.volume = myHC.summonSpeed() * 0.1f;
            }
        } else {
            myTrail.emissionRate = myRB.velocity.magnitude * 0.2f;
        }
        if ( mainHC.supercharged == false && transform.position.y > 10f )
        {
            mainHC.supercharged = true;
            mainHC.changeLightning(mainHC.chargeLightning * 3f);
        }
    }
}
