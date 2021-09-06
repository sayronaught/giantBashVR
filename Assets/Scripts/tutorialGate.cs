using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialGate : MonoBehaviour
{
    public gameController mainCG;
    private Rigidbody myRB;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.magnitude > 3f)
        {
            myRB.isKinematic = false;
            mainCG.smashedGate();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
