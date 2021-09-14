using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    public int pointValue = 1;
    public gameController mainGC;

    public Material[] targetMats;

    public Rigidbody myRB;
    public AudioSource myAS;
    public bool isHit = false;
    public float disappearTimer = 5f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if ( !isHit )
        {
            mainGC.addPoints(pointValue);
            isHit = true;
            myRB.isKinematic = false;
            Vector3 force = collision.transform.position - transform.position;
            myRB.AddForce(force.normalized * 150f);
            myAS.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
        int mat = Random.Range(0, targetMats.Length);
        transform.GetChild(0).GetComponent<MeshRenderer>().material = targetMats[mat];
    }

    // Update is called once per frame
    void Update()
    {
        if ( isHit )
        {
            disappearTimer -= Time.deltaTime;
            if ( disappearTimer < 0f )
            {
                mainGC.targetList.Remove(gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
