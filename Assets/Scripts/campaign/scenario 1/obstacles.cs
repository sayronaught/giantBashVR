using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacles : MonoBehaviour
{
    [Tooltip("+1")]
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
            if (health == 0) Destroy(gameObject);
            else health--;
        }
    }
}
