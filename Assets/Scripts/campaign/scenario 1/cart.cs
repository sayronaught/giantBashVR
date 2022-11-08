using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : MonoBehaviour
{
    [Tooltip("constant move speed of the player")]
    public float cartSpeed = 0.4f;
    [Tooltip("size of the waypoints")]
    public float waypointHitbox = 0.5f;
    [Tooltip("list of waypoints (counts form 0 and up)")]
    public Transform[] cartWaypoints;
    [Tooltip("current waypoint its moving to on the waypoint list (counts form 0 and up)")]
    public int waypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * cartSpeed;

        Vector3 relativePos = cartWaypoints[waypoint].position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);

        if (Vector3.Distance(transform.position, cartWaypoints[waypoint].position) <= waypointHitbox && waypoint < cartWaypoints.Length -1f) waypoint++;
    }
}
