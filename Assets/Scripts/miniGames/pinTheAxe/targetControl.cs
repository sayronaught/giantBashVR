using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetControl : MonoBehaviour
{
    public int stage;
    public float rotationDirection = 0f;
    public float rotationSpeed = 100f;
    public float rotationValue;
    public Vector3 rotation;
    public float rangeTimer;
    public float rangeReset;
    public bool rangeChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        on();
        stage1();
        stage2();
        stage3();
        stage4();
        stage5();
        stage6();
        if (stage == 4 || stage == 5)
        {
            rangeUpdate();
        }

    }
    void on()
    {
        rotationValue += rotationDirection * rotationSpeed * Time.deltaTime;
        rotation = new Vector3(rotationValue, -90f , 90f);
        transform.rotation = Quaternion.Euler(rotation);
    }
    void stage1()
    {
        if(stage == 1)
        {
            rotationDirection = 1f;
        }
    }
    void stage2()
    {
        if(stage == 2)
        {
            rotationDirection = 1.5f;
        }
    }
    void stage3()
    {
        if(stage == 3)
        {
            rotationDirection = -1.5f;
        }
    }
    void stage4()
    {
        if(stage == 4)
        {
            if(rangeChange == true)
            {
                rotationDirection = Random.Range(0.15f, 1.5f);
                rangeChange = false;
            }
        }
    }
    void stage5()
    {
        if(stage == 5)
        {
            if(rangeChange == true)
            {
                rotationDirection = Random.Range(-2f, 2f);
                rangeChange = false;
            }
        }
    }
    void stage6()
    {
        if(stage == 6)
        {
            rotationDirection = 0;
        }
    }
    void rangeUpdate()
    {
        rangeTimer -= Time.deltaTime;
        if (rangeTimer <= 0)
        {
            rangeTimer = rangeReset;
            rangeChange = true;

        }
    }
}
