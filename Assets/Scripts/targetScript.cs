using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    public gameController mainGC;

    private Rigidbody myRB;
    private AudioSource myAS;
    private bool isHit = false;
    private float disappearTimer = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if ( !isHit )
        {
            mainGC.addPoints(1);
            isHit = true;
            myRB.isKinematic = false;
            myAS.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( isHit )
        {
            disappearTimer -= Time.deltaTime;
            if ( disappearTimer < 0f )
            {
                Destroy(this.gameObject);
            }
        }
    }
}
