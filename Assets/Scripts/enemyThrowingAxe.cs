using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyThrowingAxe : MonoBehaviour
{
    public AudioClip AxeHitting;

    public bool isThrown = false;
    public bool isStuck = false;
    private Rigidbody myRB;
    private AudioSource myAS;

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrown && !isStuck)
        {
            isStuck = true;
            myRB.isKinematic = true;
            myAS.Stop();
            myAS.loop = false;
            myAS.clip = AxeHitting;
            myAS.pitch = 1f;
            myAS.Play();
            Destroy(gameObject, 10f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( isThrown && !isStuck ) myRB.AddRelativeTorque(Vector3.up * 15000f);
        
    }
}
