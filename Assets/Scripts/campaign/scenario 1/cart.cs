using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : MonoBehaviour
{
   [Tooltip("constant move speed of the player")]
   public float cartSpeed = 0.4f;
   [Tooltip("size of the waypoints")]
   public float waypointHitbox = 0.5f;


    private bool move = true;
    private float waitTime = 0;

    [System.Serializable]

    public class waypoint
    {
        [Tooltip("waypoint")]
        public Transform whereToGo;

        [Tooltip("should it stop?")]
        public bool stopEvent = false;

        [DrawIf("stopEvent", true)]
        [Tooltip("should it be a timer?")]
        public bool timer = false;

        [DrawIf("timer", true)]
        [Tooltip("how long should it stop?")]
        public float waitTime = 3f;


    }
    [Tooltip("list of waypoint (counts from 0)")]
    public waypoint[] waypoints;
    [Tooltip("current waypoint on the list")]
    public int waypointInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position += transform.forward * Time.deltaTime * cartSpeed;
        
            Vector3 relativePos = waypoints[waypointInt].whereToGo.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
        }
        if (!move)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0) move = true;
        }

        if (Vector3.Distance(transform.position, waypoints[waypointInt].whereToGo.position) <= waypointHitbox && waypointInt < waypoints.Length - 1f)
        {
            if (waypoints[waypointInt].stopEvent)
            {
                move = false;
                if(waypoints[waypointInt].timer) waitTime = waypoints[waypointInt].waitTime;
            }
            waypointInt++;
        }
    }
}
