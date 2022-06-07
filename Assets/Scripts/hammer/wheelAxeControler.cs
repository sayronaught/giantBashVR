using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelAxeControler : MonoBehaviour
{
    public targetControl mytc;

    public GameObject axeprefab;
    public Transform axeposition;

    private hammerControllerEndlessMode myHC;
    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myHC = GetComponent<hammerControllerEndlessMode>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHC.beingHeld()) mytc.beenHit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WheelTarget" )
        {
            var axeshadow = Instantiate(axeprefab, transform.position, transform.rotation);
            transform.position = axeposition.position;
            transform.rotation = axeposition.rotation;
            axeshadow.transform.localScale = new Vector3(5f, 5f, 5f);
            axeshadow.transform.SetParent(collision.transform);
            myRB.velocity = Vector3.zero;
            myRB.angularVelocity = Vector3.zero;
            Destroy(axeshadow, 30f);
        }
    }
}
