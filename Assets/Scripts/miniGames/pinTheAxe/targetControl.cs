using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class targetControl : MonoBehaviour
{
    public int difficulty;
    public int stage;
    public float rotationDirection = 0f;
    public float rotationSpeed = 100f;
    public float rotationValue;
    private Vector3 rotation;
    private Vector3 rotationPoint;
    public float rangeTimer;
    public float rangeReset;
    public bool rangeChange;
    public bool beenHit = false;

    bool donePlaying = false;
    private float randomSpeed = 0;

    public GameObject mySpin;
    public GameObject bloodSplat;
    public GameObject magicSplat;
    public AudioSource resultCheer;
    public TMP_Text stageCounter;
    public pinTheAxeController myGM;

    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty)
        {
            case 1:
                rangeReset = 1.7f;
                rotationSpeed = 100f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 3);
                break;
            case 2:
                rangeReset = 1.2f;
                rotationSpeed = 175f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 5);
                break;
            case 3:
                rangeReset = 0.7f;
                rotationSpeed = 250f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 7);
                break;
            case 4:
                rangeReset = 0.4f;
                rotationSpeed = 350f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 10);
                break;
            case 5:
                rangeReset = 0.3f;
                rotationSpeed = 350f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 14);
                break;
            case 6:
                rangeReset = 0.3f;
                rotationSpeed = 350f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 14);
                break;
            case 7:
                rangeReset = 0.3f;
                rotationSpeed = 400f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 14);
                break;
            case 8:
                rangeReset = 0.3f;
                rotationSpeed = 400f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 14);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != 0 && stage != 6) on();
        switch( stage )
        {
            case 1:
                stage1();
                break;
            case 2:
                stage2();
                break;
            case 3:
                stage3();
                break;
            case 4:
                stage4();
                rangeUpdate();
                break;
            case 5:
                stage5();
                rangeUpdate();
                break;
            case 6:
                stage6();
                break;
            default:
                // if everything misses, typically an error message
                break;
        }

    }

    void dif9()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 3f) * 4f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.7f, 14 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4), Time.deltaTime);
        rotationPoint = new Vector3(-11 + Mathf.Sin(Time.fixedTime * rotationDirection) * 11, rotationValue * 0.1f, 0);
        mySpin.transform.rotation = Quaternion.Slerp(mySpin.transform.rotation, Quaternion.Euler(rotationPoint), Time.deltaTime);
    }
    void on()
    {
        rotationValue += rotationDirection * rotationSpeed * Time.deltaTime;
        rotation = new Vector3(0f, 180f , rotationValue);
        transform.localRotation = Quaternion.Euler(rotation);
        switch (difficulty)
        {
            
            case 1:
                break;
            case 2:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime) * 2f, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);
                break;
            case 3:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 1.5f) * 3f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.4f, transform.localPosition.z), Time.deltaTime);
                break;
            case 4:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 2f) * 3f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.4f, 10 + Mathf.Sin(Time.fixedTime * randomSpeed) * 2), Time.deltaTime);
                break;
            case 5:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 3f) * 4f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.7f, 14 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4), Time.deltaTime);
                break;
            case 6:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 3f) * 4f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.7f, 14 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4), Time.deltaTime);
                rotationPoint = new Vector3(0f, rotationValue * 0.1f, 0);
                mySpin.transform.rotation = Quaternion.Euler(rotationPoint);
                break;
            case 7:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 3f) * 4f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.7f, 14 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4), Time.deltaTime);
                rotationPoint = new Vector3(0f, rotationValue * 0.1f, 0);
                mySpin.transform.rotation = Quaternion.Euler(rotationPoint);
                break;
            case 8:
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 3f) * 4f, 2f - Mathf.Cos(Time.fixedTime * 3) * 0.7f, 14 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4), Time.deltaTime);
                rotationPoint = new Vector3(-11 + Mathf.Sin(Time.fixedTime * rotationDirection) * 11, rotationValue * 0.1f, 0);
                mySpin.transform.rotation = Quaternion.Slerp(mySpin.transform.rotation, Quaternion.Euler(rotationPoint) ,Time.deltaTime);
                break;
            case 9:
                dif9();
                break;
            default:
                // if everything misses, typically an error message
                break;
        }
                                       
    }
    void stage1()
    {
        rotationDirection = 1f;
    }
    void stage2()
    {
        rotationDirection = 1.5f;
    }
    void stage3() 
    {
        rotationDirection = -1.5f;
    }
    void stage4()
    {
        if (rangeChange && difficulty != 7)
        {
            rotationDirection = Random.Range(0.15f, 1.5f);
            rangeChange = false;
        }
        else if (rangeChange && difficulty == 7)
        {
            rotationDirection = Random.Range(0.3f, 2.5f);
            rangeChange = false;
        }
    }
    void stage5()
    {
        if (donePlaying) donePlaying = false;
        if (rangeChange && difficulty != 7)
        {
            rotationDirection = Random.Range(-2f, 2f);
            rangeChange = false;
        }
        else if (rangeChange && difficulty == 7)
        {
            rotationDirection = Random.Range(-3f, 3f);
            rangeChange = false;
        }
    }
    void stage6()
    {
        rotationDirection = 0;
        if (!donePlaying)
        {
            donePlaying = true;
            resultCheer.Play();
            myGM.diffIncrease();
        }
    }
    void rangeUpdate()
    {
        rangeTimer -= Time.deltaTime;
        if (rangeTimer <= 0)
        {
            if (difficulty != 6 && difficulty != 7 && difficulty != 8) rangeTimer = rangeReset;
            else rangeTimer = Random.Range(0.2f, 1f);
            if (difficulty == 4)
            {
                randomSpeed = Random.Range(-2f, 2f);
            }
            if (difficulty == 5)
            {
                randomSpeed = Random.Range(-4f, 4f);
            }
            rangeChange = true;
        }
    }
}
