using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerFX : MonoBehaviour
{
    public ParticleSystem myTrail;
    public hammerController myHC;

    public AudioSource myAS;
    private Rigidbody myRB;

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
        if (myHC.beingSummoned()) myAS.volume = myHC.summonSpeed() * 0.1f;
        if (myHC.beingHeld())
        {
            myAS.volume = 0f;
        } else {
            myTrail.emissionRate = myRB.velocity.magnitude * 0.05f;
        }
    }
}
