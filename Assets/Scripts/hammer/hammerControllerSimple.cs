using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerControllerSimple : MonoBehaviour
{

    private PlayerVrControls myVrControls;

    // Start is called before the first frame update
    void Start()
    {
        myVrControls = GameObject.FindObjectOfType<PlayerVrControls>();
        if ( myVrControls == null ) Debug.LogError("I could not find any 'PlayerVrControls', you need to place one on your XR Rig");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
