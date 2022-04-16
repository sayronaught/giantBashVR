using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    Rigidbody rb;
    public bool chase;
    public bool stop;
    public GameObject dog;

    public Animator anima;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anima = transform.GetChild(0).GetComponent<Animator>();         
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (chase == true)
        {
            //anim.animation
            speed = 2;
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(pos);
            transform.LookAt(target);
            anima.SetBool("isChasing", true);
            anima.SetBool("isStop", false);


        }
        else
        {
            speed = 0;
            anima.SetBool("isChasing", false);
            anima.SetBool("isStop", true);
        }
        





    }
    private void OnTriggerEnter(Collider other)
    {
        chase = false;
        stop = true;
        //anim.Play("metarig|iDLE");
    }

    private void OnTriggerExit(Collider other)
    {
        chase = true;
        stop = false;
        speed = 2;
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target);
        
    }
}
