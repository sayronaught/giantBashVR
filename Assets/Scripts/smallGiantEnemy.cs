using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallGiantEnemy : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 0f;

    // The target (cylinder) position.
    public Transform target;

    private Animator myAnim;

    public void eventDeath()
    {
        Destroy(gameObject);
    }
    public void eventIsUp()
    {
        speed = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            myAnim.SetBool("dancing", true);
        } else {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        transform.LookAt(target.position);

    }
}
