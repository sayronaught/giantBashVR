using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetColider : MonoBehaviour
{
    public targetControl mytc;
    private MeshRenderer myMR;
    private BoxCollider myBC;
    // Start is called before the first frame update
    void Start()
    {
        mytc = GetComponentInParent<targetControl>();
        myMR = GetComponent<MeshRenderer>();
        myBC = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mytc.stage == 0)
        {
            myMR.enabled = true;
            myBC.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (mytc.beenHit == false)
        {
            if (collision.gameObject.tag == "Hammer")
            {
                mytc.beenHit = true;
                mytc.stage += 1;
                Instantiate(mytc.bloodSplat, transform.position, Quaternion.identity);
                myMR.enabled = false;
                myBC.enabled = false;
            }
        }
    }
}
