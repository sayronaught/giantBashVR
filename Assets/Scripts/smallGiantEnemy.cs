using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallGiantEnemy : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 0f;
    public float distance = 10f;

    // The target (cylinder) position.
    public Transform target;
    public GameObject bloodPrefab;
    public gameController mainGC;

    private Animator myAnim;
    private _Settings mySettings;

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
        float size = Random.Range(1f, 4f);
        transform.localScale = new Vector3(size, size, size);
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        // Check if the position of the cube and sphere are approximately equal.
        if ( distance < 0.5f)
        {
            myAnim.SetBool("dance", true);
        } else {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        transform.LookAt(target.position);

    }
}
