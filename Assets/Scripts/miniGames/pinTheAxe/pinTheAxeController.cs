using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinTheAxeController : MonoBehaviour
{
    public targetControl myTC;
    public wheelAxeControler myHC;
    public GameObject myFTC1;
    public GameObject myFTC2;
    public mapObj myMO;

    public float chickRespawn = 0;
    bool chicklive;
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

        if (chickRespawn >= 0) chickRespawn -= Time.deltaTime;
        if (chickRespawn < 0 && !chicklive) myMO.myChicken.SetActive(true);
            
    }
}
