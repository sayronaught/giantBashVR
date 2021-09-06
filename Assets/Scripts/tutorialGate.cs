using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialGate : MonoBehaviour
{
    public gameController mainCG; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.magnitude > 2f)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
            mainCG.smashedGate();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
