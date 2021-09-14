using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeToLive : MonoBehaviour
{
    private float myTimeToLive = 7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        myTimeToLive -= Time.deltaTime;
        if ( myTimeToLive < 0f )
        {
            Destroy(gameObject);
        }
        
    }
}
