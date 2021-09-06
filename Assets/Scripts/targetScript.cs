using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    public gameController mainGC;

    private Rigidbody myRB;
    private bool isHit = false;
    private float disappearTimer = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if ( !isHit )
        {
            myRB.isKinematic = false;
            mainGC.addPoints(1);
            isHit = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>(); 
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
