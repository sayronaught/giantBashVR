using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetColider : MonoBehaviour
{
    public targetControl mytc;
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
        if (GetComponentInParent<targetControl>().beenHit == false)
        {
            if (collision.gameObject.tag == "Hammer")
            {
                mytc.beenHit = true;
                mytc.stage += 1;
                Instantiate(mytc.bloodSplat, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
