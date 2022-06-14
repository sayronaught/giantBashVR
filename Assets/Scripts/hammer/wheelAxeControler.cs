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
    private AudioSource myAS;

    // Start is called before the first frame update
    void Start()
    {
        myHC = GetComponent<hammerControllerEndlessMode>();
        myRB = GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHC.beingHeld() )
        { // holding the axe
            mytc.beenHit = false;
            holding = true;
            spinning = false;
            myAS.volume = 0;
        } else { // not holding the axe
            if ( holding )
            {
                holding = false;
                spinning = true;
                myAS.volume = 1f;
                
            }
            if ( spinning )
            {
                myAS.pitch = 0.2f + (Mathf.Abs(myRB.velocity.magnitude) * 0.025f);
                transform.Rotate(Vector3.left, -150f * Time.deltaTime * (myRB.velocity.magnitude*3f));  
            }
            if (myHC.beingSummoned())
            {
                spinning = false;
                myAS.volume = 0f;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        spinning = false;
        myAS.volume = 0f;
        if (collision.gameObject.tag == "WheelTarget" )
        {
            var axeshadow = Instantiate(axeprefab, transform.position, transform.rotation);
            transform.position = axeposition.position;
            transform.rotation = axeposition.rotation;
            if ( collision.transform.name == "targetCollider")
                axeshadow.transform.SetParent(collision.transform.parent);
            else
                axeshadow.transform.SetParent(collision.transform);
            axeshadow.transform.localScale = new Vector3(5f, 5f, 5f);
            myRB.velocity = Vector3.zero;
            myRB.angularVelocity = Vector3.zero;
            Destroy(axeshadow, 30f);
            Axes++;
            Axecounter.text = Axes.ToString();
        }
    }
}
