using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class pinTheAxeController : MonoBehaviour
{
    public targetControl myTC;
    public wheelAxeControler myHC;
    public GameObject myFTC1;
    public GameObject myFTC2;
    public falseTargetController myFTS1;
    public falseTargetController myFTS2;
    public mapObj myMO;
    public float chickRespawn = 0;
    bool chicklive;
    public GameObject debugSpinPoint;
    public bool DegDebug90 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myTC.difficulty == 5 || myTC.difficulty == 7)
        {
            myFTC1.SetActive(true);
            myFTC2.SetActive(true);
        }
        else
        {
            myFTC1.SetActive(false);
            myFTC2.SetActive(false);
        }

        if (chickRespawn >= 0) chickRespawn -= Time.deltaTime;
        if (chickRespawn < 0 && !chicklive) myMO.myChicken.SetActive(true);

        if (myTC.difficulty == 6 && !DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * 90);
            DegDebug90 = true;
        }
        if (myTC.difficulty != 6 && DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * -90);
            DegDebug90 = false;
        }
    }
    public async Task diffIncrease()
    {
        await Task.Delay(5000);
        if (!Application.isEditor || Application.isPlaying)
        {
            Debug.Log("c");
            myTC.stage = 0;
            myFTS1.reset = true;
            myFTS2.reset = true;
            myTC.transform.rotation = Quaternion.Euler(0, 180, 0);
            myTC.rotationValue = 0;

            switch (myTC.difficulty++)
            {
                case 1:
                    Debug.Log("1");
                    break;
                case 2:
                    Debug.Log("2");
                    myTC.rangeReset = 1.2f;
                    myTC.rotationSpeed = 175f;
                    myTC.transform.position = new Vector3(0, 3, 5);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 3:
                    Debug.Log("3");
                    myTC.rangeReset = 0.7f;
                    myTC.rotationSpeed = 250f;
                    myTC.transform.position = new Vector3(0, 3, 7);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 4:
                    myTC.rangeReset = 0.4f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.position = new Vector3(0, 3, 10);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 5:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.position = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    myFTS1.reset = true;
                    myFTS2.reset = true;
                    break;

                case 6:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.position = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    myFTS1.reset = true;
                    myFTS2.reset = true;
                    break;

                    default:
                    Debug.Log("fuck");
                    break;
            }
            myHC.Axes = 0;
            myHC.Axecounter.text = myHC.Axes.ToString();
            myTC.stageCounter.text = myTC.stage.ToString();
            Debug.Log("d");
        }   
        await Task.Yield();
    }
}
