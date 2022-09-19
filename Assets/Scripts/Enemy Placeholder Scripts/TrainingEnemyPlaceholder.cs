using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingEnemyPlaceholder : MonoBehaviour
{
    // Adjust the speed for the application.
    float speed = 2f;
    public float distance = 10f;
    public float turnSpeed = 20f;

    float speedVelocityReference;
    public float targetSpeed = 0;

    // The target (cylinder) position.
    public Transform target;
    public GameObject bloodPrefab;
    public gameController mainGC;

    private Animator myAnim;
    private _Settings mySettings;
    Rigidbody rb;

    public void eventDeath()
    {
        Destroy(gameObject);
    }
    public void eventIsUp()
    {
        speed = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Hammer") return;
        var blood = Instantiate(bloodPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        blood.transform.position = transform.position;
        if (mySettings) mySettings.jotunsBashed++;
        Destroy(gameObject);

    }
    void Awake()
    {
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //float size = Random.Range(1f, 2f);
        //transform.localScale = new Vector3(size, size, size);

        myAnim.SetFloat("variation", Random.Range(-1,2));

        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        // Check if the position of the cube and sphere are approximately equal.
        if ( distance < 2f)
        {
            myAnim.SetBool("isAttacking", true);
            myAnim.SetFloat("CurrentSpeed", 0);
            myAnim.SetInteger("Random", Random.Range(0,2));

        } else {
            // Move our position a step closer to the target.
            //float step = speed * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            //speed = 2f;

            if(targetSpeed < speed - 0.05)
                targetSpeed = Mathf.SmoothDamp(targetSpeed, speed, ref speedVelocityReference, 3f);
            else
                targetSpeed = speed;

            Vector3 targetVelocity = transform.forward * targetSpeed;
            targetVelocity.y = rb.velocity.y;

            rb.velocity = targetVelocity;
            myAnim.SetFloat("CurrentSpeed", targetSpeed / 2);
            

        }
        var enemyRotation = Quaternion.LookRotation(target.position - transform.position);

        enemyRotation.z = 0;
        enemyRotation.x = 0;

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, enemyRotation, turnSpeed));

        //transform.LookAt(target.position);

    }
}
