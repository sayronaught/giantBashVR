using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTargetChanger : MonoBehaviour
{
    public bool resetBool;
    public int reset;
    public bool difficultyBool;
    public int difficulty;
    public bool hellMode;
    public targetControl myTC;
    public wheelAxeControler myHC;
    public falseTargetController myFTC;
    public falseTargetController myFTC2;
    // Start is called before the first frame update
    void Start()
    {
        //myTC = GetComponent<targetControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (resetBool)
        {
            myTC.stage = reset;
            myHC.Axes = -1;
            myFTC.reset = true;
            myFTC2.reset = true;
        }
        if (difficultyBool)
        {
            if (difficulty == 1)
            {
                myTC.difficulty = 1;
                myTC.rangeReset = 1.7f;
                myTC.rotationSpeed = 100f;
                myTC.transform.position = new Vector3(0,3 , 3);
            }
            if (difficulty == 2)
            {
                myTC.difficulty = 2;
                myTC.rangeReset = 1.2f;
                myTC.rotationSpeed = 175f;
                myTC.transform.position = new Vector3(0,3, 5);
            }
            if (difficulty == 3)
            {
                myTC.difficulty = 3;
                myTC.rangeReset = 0.7f;
                myTC.rotationSpeed = 250f;
                myTC.transform.position = new Vector3(0 , 3 , 7);
            }
            if (difficulty == 4)
            {
                myTC.difficulty = 4;
                myTC.rangeReset = 0.4f;
                myTC.rotationSpeed = 350f;
                myTC.transform.position = new Vector3(0, 3, 10);
            }
            
        }
        if (hellMode)
        {
            myTC.stage = reset;
            myHC.Axes = -1;
            myTC.difficulty = 5;
            myTC.rangeReset = 0.3f;
            myTC.rotationSpeed = 350f;
            myTC.transform.position = new Vector3(0, 3, 14);
            myFTC.reset = true;
            myFTC2.reset = true;
        }

    }
}
