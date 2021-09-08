using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialGate : MonoBehaviour
{
    public gameController mainGC;

    private AudioSource myAS;
    private float deleteTimer = 10f;
    private bool smashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.magnitude > 2f)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
            mainGC.smashedGate();
            smashed = true;
            myAS.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if ( smashed )
        {
            deleteTimer -= Time.deltaTime;
            if (deleteTimer < 0f)
                Destroy(gameObject);
        }
    }
}
