using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chick : MonoBehaviour
{
    Rigidbody myRB;
    CapsuleCollider myCC;
    public GameObject bloodSplat;
    public pinTheAxeController myGM;

    float deathTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myCC = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            myGM.chickRespawn = deathTime;
            gameObject.SetActive(false);
        }
    }
}