using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerFX : MonoBehaviour
{
    public ParticleSystem myTrail;
    public hammerController myHC;
    public ParticleSystem myLightning;
    public AudioSource myLightningSFX;
    public hammerController mainHC;

    public AudioSource myAS;
    private Rigidbody myRB;

    private void OnCollisionEnter(Collision collision)
    {
        mainHC.changeLightning(0f);
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
        myAS.volume = myRB.velocity.magnitude * 0.05f;
        if (myHC.beingSummoned()) myAS.volume = myHC.summonSpeed() * 0.05f;
        if (myHC.beingHeld())
        {
            myAS.volume = 0f;
        } else {
            myTrail.emissionRate = myRB.velocity.magnitude * 0.05f;
        }
    }
}
