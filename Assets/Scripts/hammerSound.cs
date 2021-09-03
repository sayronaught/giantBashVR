using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerSound : MonoBehaviour
{

    private AudioSource myAS;
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
        myAS.volume = myRB.velocity.magnitude*0.1f;
    }
}
