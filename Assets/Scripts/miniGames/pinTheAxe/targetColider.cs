using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetColider : MonoBehaviour
{
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
        if (collision.gameObject.tag == "Hammer")
        {
            GetComponentInParent<targetControl>().stage += 1;
            Destroy(gameObject);
            Debug.Log("hit");
        }
    }
}
