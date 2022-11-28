using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
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

    public circusMan myMan;
    public TMP_Text watch;
    public Material watchGUI;
    public float timeLimit = 180;
    public float debug = 0;
    private bool failed = false;

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

        if (myTC.difficulty != 6 && myTC.difficulty != 7 && DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * -90);
            DegDebug90 = false;
        }
        else if (!DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * 90);
            DegDebug90 = true;
        }

        var timeInSecondsInt = (int)myMan.time * -1 + (int)timeLimit;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
        var minutes = timeInSecondsInt / 60;  //Get total minutes
        var seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
        watch.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        watchGUI.SetFloat("Vector1_4",( (myMan.time / timeLimit )-0.5f) * -1);

        if(myMan.time > timeLimit && !failed)
        {
            myTC.gameObject.SetActive(false);
            failed = true;
            myMan.buzzer.Play();
        }
       
    }
    public async Task diffIncrease()
    {
        await Task.Delay(5000);
        if (!Application.isEditor || Application.isPlaying)
        {
            myTC.stage = 0;
            myFTS1.reset = true;
            myFTS2.reset = true;
            myTC.transform.localRotation = Quaternion.Euler(0, 180, 0);
            myTC.rotationValue = 0;
            myMan.time = 0;
            switch (myTC.difficulty++)
            {
                case 1:
                    myTC.rangeReset = 1.7f;
                    myTC.rotationSpeed = 100f;
                    myTC.transform.position = new Vector3(0, 2, 3);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 2:
                    myTC.rangeReset = 1.2f;
                    myTC.rotationSpeed = 175f;
                    myTC.transform.localPosition = new Vector3(0, 2, 5);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 3:
                    myTC.rangeReset = 0.7f;
                    myTC.rotationSpeed = 250f;
                    myTC.transform.localPosition = new Vector3(0, 2, 7);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 4:
                    myTC.rangeReset = 0.4f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.localPosition = new Vector3(0, 3, 10);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 5:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 6:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 7:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 400f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 8:
                    myTC.stage = 0;
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 400f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                default:
                    myTC.difficulty = 1;
                    myTC.rangeReset = 1.7f;
                    myTC.rotationSpeed = 100f;
                    myTC.transform.position = new Vector3(0, 2, 3);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
            myHC.Axes = 0;
            myHC.Axecounter.text = myHC.Axes.ToString();
            myTC.stageCounter.text = ("stage" + myTC.stage.ToString());
        }   
        await Task.Yield();
    }
}
