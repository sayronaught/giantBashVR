using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeToLive : MonoBehaviour
{
    private float myTimeToLive = 7f;
    
    // Update is called once per frame
    private void Update()
    { 
        myTimeToLive -= Time.deltaTime;
        if ( myTimeToLive < 0f )
        {
            Destroy(gameObject);
        }
        
    }
}
