using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falseTargetController : MonoBehaviour
{
    public targetControl myTC;
    public GameObject myTGO;

    public float rotationDirection = 0f;
    public float rotationSpeed = 100f;
    public float rotationValue;
    public Vector3 rotation;

    public bool rangeChange;
    public float rangeTimer;
    public float rangeReset;
    public float offset =1f;

    public float distanceFromTarget = 1f;

    public bool reset = false;
    public float startPos;

    private float randomSpeed = 1;
    void OnEnable()
    {

        rangeReset = 0.3f;
        rotationSpeed = 350f;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, myTGO.transform.position.z +4);
        
    }
    void Update()
    {
        if (myTC.stage > 0) on();
        if (myTC.stage == 1) stage1();
        if (myTC.stage == 2) stage2();
        if (myTC.stage == 3) stage3();
        if (myTC.stage == 4) stage4();
        if (myTC.stage == 5) stage5();
        if (myTC.stage == 6) stage6();
        if (myTC.stage == 4 || myTC.stage == 5) rangeUpdate();
        if (reset)
        {
            reset = false;
            transform.position = new Vector3(startPos, 2, myTGO.transform.position.z);
        }
    }
    void on()
    {
        rotationValue += rotationDirection * rotationSpeed * Time.deltaTime * randomSpeed;
        rotation = new Vector3(0f, 180f, rotationValue);
        transform.localRotation = Quaternion.Euler(rotation);
        transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(Mathf.Sin(Time.fixedTime * 2f * offset) * 6f, 2f - Mathf.Cos(Time.fixedTime * 2f * offset) * 1.5f, myTC.transform.localPosition.z + distanceFromTarget), Time.deltaTime);
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
        if (rangeChange == true)
        {
            rotationDirection = Random.Range(0.15f, 1.5f);
            offset = Random.Range(-1.5f, 1.5f);
            rangeChange = false;
        }
    }
    void stage5()
    {
        if (rangeChange == true)
        {
            rotationDirection = Random.Range(-2f, 2f);
            offset = Random.Range(-1.5f, 1.5f);
            rangeChange = false;
        }
    }
    void stage6()
    {
        rotationDirection = 0;
    }
    void rangeUpdate()
    {
        if (myTC.difficulty != 6) rangeTimer = rangeReset;
        else rangeTimer = Random.Range(0.2f, 1f);
        if (rangeTimer <= 0)
        {
            rangeTimer = rangeReset;
            randomSpeed = Random.Range(-10f, 10f);
            rangeChange = true;

        }
    }
}
