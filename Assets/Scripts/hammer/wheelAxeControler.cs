using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelAxeControler : MonoBehaviour
{
    public targetControl mytc;

    private hammerControllerEndlessMode myHC;

    // Start is called before the first frame update
    void Start()
    {
        myHC = GetComponent<hammerControllerEndlessMode>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myHC.beingHeld()) mytc.beenHit = false;
    }
}
