using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDancer : MonoBehaviour
{
    Rigidbody myRB;
    CapsuleCollider myCC;
    Animation myAni;
    

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myCC = GetComponent<CapsuleCollider>();
        myAni = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            myAni.Play();
        }
    }
}
