using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericMissile : MonoBehaviour
{
    public Vector3 target;
    public float arcUp = 0.45f;
    public float speedForward = 5f;
    public Vector3 Spin = Vector3.zero;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target);
        transform.position = Vector3.MoveTowards(transform.position, target + (Vector3.up * (distance * arcUp)), Time.deltaTime * speedForward);
        transform.Rotate(Spin);
    }
}
