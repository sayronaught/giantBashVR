using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class pinTheAxeController : MonoBehaviour
{
    public targetControl myTC;
    public wheelAxeControler myWAC;
    public falseTargetController myFTC1;
    public falseTargetController myFTC2;
    public tallyBoard myTally;
    public circusMan myMan;
    public mapObj myMO;
    public multiSplineAnimator[] mySA;

    public float chickRespawn = 0;
    bool chicklive;
    public GameObject debugSpinPoint;
    public bool DegDebug90 = false;


    public TMP_Text watch;
    public Material watchGUI;
    public float timeLimit = 180;
    public float debug = 0;
    public bool failed = false;

    // Start is called before the first frame update
    void Start()
    {
        myTC.stageCounter.text = ("stage\n" + myTC.stage.ToString() + "\ndifficulty\n" + myTC.difficulty.ToString());
        watch.text = "3:00";
    }

    // Update is called once per frame
    void Update()
    {
        if ((myTC.difficulty == 5 || myTC.difficulty == 7) && failed == false)
        {
            myFTC1.gameObject.SetActive(true);
            myFTC2.gameObject.SetActive(true);
        }
        else
        {
            myFTC1.gameObject.SetActive(false);
            myFTC2.gameObject.SetActive(false);
        }

        if (chickRespawn >= 0) chickRespawn -= Time.deltaTime;
        if (chickRespawn < 0 && !chicklive) myMO.myChicken.SetActive(true);

        if (myTC.difficulty >= 6 && DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * -90);
            DegDebug90 = false;
        }
        else if (myTC.difficulty < 6 && !DegDebug90)
        {
            debugSpinPoint.transform.Rotate(Vector3.forward * 90);
            DegDebug90 = true;
        }

        var timeInSecondsInt = (int)myMan.time * -1 + (int)timeLimit;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
        var minutes = timeInSecondsInt / 60;  //Get total minutes
        var seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
        watch.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        watchGUI.SetFloat("Vector1_4", ((myMan.time / timeLimit) - 0.5f) * -1);

        if (myMan.time > timeLimit)
        {
            watch.text = "You Lost";
        }
    }

    public void timedOut()
    {
        myTC.gameObject.SetActive(false);
        myFTC1.gameObject.SetActive(false);
        myFTC2.gameObject.SetActive(false);
        failed = true;
        myMan.buzzer.Play();
        myTC.difficulty = -1;
        myTally.newScore(myTC.stage , myTC.difficulty , myWAC.Axes, null);
        diffIncrease(30000);
    }

    public async Task diffIncrease(int delay)
    {
        await Task.Delay(delay);
        if (!Application.isEditor || Application.isPlaying)
        {
            myMan.started = false;
            myTC.stage = 0;
            myTC.gameObject.SetActive(true);
            failed = false;
            myFTC1.reset = true;
            myFTC2.reset = true;
            myTC.transform.localRotation = Quaternion.Euler(0, 180, 0);
            myTC.rotationValue = 0;
            myMan.time = 0;
            foreach (multiSplineAnimator Animator in mySA)
            {
                Animator.gameObject.SetActive(false);
            }
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
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 6:
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 350f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 7:
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 400f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 8:
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 400f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 9:
                    myTC.rangeReset = 0.3f;
                    myTC.rotationSpeed = 400f;
                    myTC.transform.localPosition = new Vector3(0, 3, 14);
                    myTC.mySpin.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    foreach (multiSplineAnimator Animator in mySA)
                    {
                        Animator.gameObject.SetActive(true);
                    }
                    break;
                default:
                    myTC.difficulty = 1;
                    myTC.rangeReset = 1.7f;
                    myTC.rotationSpeed = 100f;
                    myTC.transform.position = new Vector3(0, 2, 3);
                    myTC.mySpin.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
            myWAC.Axes = 0;
            myWAC.Axecounter.text = myWAC.Axes.ToString();
            myTC.stageCounter.text = ("stage\n" + myTC.stage.ToString() + "\ndifficulty\n" + myTC.difficulty.ToString());
        }   
        await Task.Yield();
    }
}
