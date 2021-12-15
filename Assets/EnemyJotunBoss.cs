using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyJotunBoss : MonoBehaviour
{

    private Rigidbody rb;
    public Transform target;
    public GameObject effect;
    public GameObject effectDeath;
    private AudioSource audioSource;
    private Animator anim;
    
    public float speed = 5f;    
    public float health = 5;
    
    public bool ifCollision = false;
    static int score;





    private void Awake()
    {
        //GameControl.count++;
    }

    void Start()
    {       
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);                              
        transform.LookAt(target);

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(effectDeath, transform.position, Quaternion.identity);
            //audioSource.Play("DeathSound");
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            ifCollision = true;
            speed = 0f;
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);

        }
    }





}

