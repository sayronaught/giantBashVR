using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossHitLocation : MonoBehaviour
{
    public bigBossGnot bigBossScript;
    public float damageMultiplier=1f;

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.transform.tag == "Hammer")
        {
            float dam = (bigBossScript.mainHC.chargeLightning + 1f)*damageMultiplier;
            bigBossScript.takeDamage(dam);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
