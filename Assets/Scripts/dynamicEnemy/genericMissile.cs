using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericMissile : MonoBehaviour
{
    public Vector3 target;
    public float arcUp = 0.45f;
    public float speedForward = 5f;
    public bool flyStraigt = true;
    public Vector3 Spin = Vector3.zero;
    public AudioClip landSound;
    public float landLifetime = 5f;
    public bool aimCamera = false;

    public bool flying = false;

    private AudioSource myAS;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flying) return;
        if ( aimCamera ) target = Camera.main.transform.position;
        distance = Vector3.Distance(transform.position, target);
        if ( distance < 0.1f )
        {
            myAS.clip = landSound;
            myAS.Play();
            myAS.loop = false;
            Destroy(gameObject, landLifetime);
            flying = false;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, target + (Vector3.up * (distance * arcUp)), Time.deltaTime * speedForward);
            if ( flyStraigt)
            {
                transform.LookAt(target);
            } else {
                transform.Rotate(Spin);
            }
        }
    }
}
