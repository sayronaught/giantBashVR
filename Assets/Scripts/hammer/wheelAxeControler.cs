using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wheelAxeControler : MonoBehaviour
{
    public targetControl mytc;

    public GameObject axeprefab;
    public GameObject fireballPrefab;
    public Transform axeposition;

    public TextMeshProUGUI Axecounter;
    public int Axes = 0;

    public bool spinning = false;
    public bool holding = false;

    private hammerControllerEndlessMode myHC;
    private Rigidbody myRB;
    private AudioSource myAS;
    private BoxCollider myBC;

    // Start is called before the first frame update
    void Start()
    {
        myHC = GetComponent<hammerControllerEndlessMode>();
        myRB = GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
        myBC = GetComponent<BoxCollider>();
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
     /*private void OnCollisionEnter(Collision collision)
      {
          spinning = false;
          myAS.volume = 0f;
          if (collision.gameObject.tag == "WheelTarget" )
          { 
              //if (!collision.collider.bounds.Contains(collision.GetContact(0).point)) return;
              //if (!myBC.bounds.Contains(collision.GetContact(0).point)) return;

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
              var fireball = Instantiate(fireballPrefab, collision.GetContact(0).point, Quaternion.identity);
              fireball.transform.SetParent(collision.transform);
              fireball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
              Destroy(fireball, 30f);
              Debug with:
              if (hitToTest.collider.bounds.Contains(telePosition))
              {
                  print("point is inside collider");
              }
              
          }
      } */
    private void OnTriggerEnter(Collider other)
    {
        spinning = false;
        myAS.volume = 0f;
        if (other.gameObject.tag == "WheelTarget")
        {
            var axeshadow = Instantiate(axeprefab, transform.position, transform.rotation);
            transform.position = axeposition.position;
            transform.rotation = axeposition.rotation;
            if (other.transform.name == "targetCollider")
                axeshadow.transform.SetParent(other.transform.parent);
            else
                axeshadow.transform.SetParent(other.transform);
            axeshadow.transform.localScale = new Vector3(5f, 5f, 5f);
            myRB.velocity = Vector3.zero;
            myRB.angularVelocity = Vector3.zero;
            Destroy(axeshadow, 30f);
            Axes++;
            Axecounter.text = Axes.ToString();
        }
    }
}
