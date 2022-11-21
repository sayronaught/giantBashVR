using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacles : MonoBehaviour
{
    public cart cart;
    [Tooltip("can only take dmg if waypoints match")]
    public int waypoint = 0;
    [Tooltip("dies on 0")]
    public int health=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Hammer")
        {
            if (cart.waypointInt == waypoint)
            {
                if (health <= 1) Destroy(gameObject);
                else health--;
            }
        }
    }
}
