using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelAxeControler : MonoBehaviour
{
    public targetControl mytc;

    public GameObject axeprefab;
    public Transform axeposition;

    public TextMesh Axecounter;
    public int Axes = 0;

    public bool spinning = false;
    public bool holding = false;

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
        if (myHC.beingHeld())
        { // holding the axe
            mytc.beenHit = false;
            holding = true;
            spinning = false;
        } else { // not holding the axe
            if ( holding )
            {
                holding = false;
                spinning = true;
            }
            if ( spinning )
            {
                transform.Rotate(Vector3.left, -1500f * Time.deltaTime * myRB.velocity.magnitude);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        spinning = false;
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
            Axes++;
            Axecounter.text = Axes.ToString();
        }
    }
}
