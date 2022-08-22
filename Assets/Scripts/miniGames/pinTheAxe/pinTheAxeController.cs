using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinTheAxeController : MonoBehaviour
{
    public targetControl myTC;
    public wheelAxeControler myHC;
    public GameObject myFTC1;
    public GameObject myFTC2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myTC.difficulty == 5)
        {
            myFTC1.SetActive(true);
            myFTC2.SetActive(true);
        }
        if (myTC.difficulty != 5)
        {
            myFTC1.SetActive(false);
            myFTC2.SetActive(false);
        }
    }
}
