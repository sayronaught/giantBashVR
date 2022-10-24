using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetColider : MonoBehaviour
{
    public targetControl mytc;
    private MeshRenderer myMR;
    private BoxCollider myBC;
    public GameObject child;
    public bool falseTarget;
    // Start is called before the first frame update
    void Start()
    {
        mytc = GameObject.Find("Small Jotunn Target").GetComponent<targetControl>();
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
            if (transform.childCount > 0) child.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (mytc.beenHit == false)
        {
            if (collision.gameObject.tag == "Hammer")
            {
                mytc.beenHit = true;
                if(mytc.bloodSplat && !falseTarget) Instantiate(mytc.bloodSplat, transform.position, Quaternion.identity);
                else if (mytc.magicSplat) Instantiate(mytc.magicSplat, transform.position, Quaternion.identity);
                myMR.enabled = false;
                myBC.enabled = false;
                if (transform.childCount > 0) child.SetActive(false);
                if (!falseTarget) mytc.stage += 1;
                if (mytc.stageCounter) mytc.stageCounter.text = ("stage" + mytc.stage.ToString());


            }
        }
    }
}
