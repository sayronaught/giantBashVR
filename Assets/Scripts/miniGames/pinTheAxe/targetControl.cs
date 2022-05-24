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
    public float difficulty;
    public bool beenHit = false;
    public GameObject bloodSplat;

    private float randomSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (difficulty == 1)
        {
            rangeReset = 1.7f;
            rotationSpeed = 100f;
            transform.position = new Vector3(transform.position.x, transform.position.y , 3 );
        }
        if (difficulty == 2)
        {
            rangeReset = 1.2f;
            rotationSpeed = 175f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 5 );
        }
        if (difficulty == 3)
        {
            rangeReset = 0.7f;
            rotationSpeed = 250f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 7);
        }
        if (difficulty == 4)
        {
            rangeReset = 0.5f;
            rotationSpeed = 350f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        on();
        if (stage == 1) stage1();
        if (stage == 2) stage2();
        if (stage == 3) stage3();
        if (stage == 4) stage4();
        if (stage == 5) stage5();
        if (stage == 6) stage6();
        if (stage == 4 || stage == 5) rangeUpdate();
        if (difficulty == 2) transform.position = new Vector3(Mathf.Sin(Time.fixedTime ) * 3f,transform.position.y , transform.position.z);
        if (difficulty == 3) transform.position = new Vector3(Mathf.Sin(Time.fixedTime * 1.5f) * 3f, 1f - Mathf.Cos(Time.fixedTime * 3) * 0.4f, transform.position.z);
        if (difficulty == 4) transform.position = new Vector3(Mathf.Sin(Time.fixedTime * 2f) * 3f, 1f - Mathf.Cos(Time.fixedTime * 3) * 0.4f, 10 + Mathf.Sin(Time.fixedTime * randomSpeed) * 4);
    }
    void on()
    {
        rotationValue += rotationDirection * rotationSpeed * Time.deltaTime;
        rotation = new Vector3(rotationValue, -90f , 90f);
        transform.rotation = Quaternion.Euler(rotation);
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
            rangeChange = false;
        }
    }
    void stage5()
    {
        if (rangeChange == true)
        {
            rotationDirection = Random.Range(-2f, 2f);
            rangeChange = false;
        }
    }
    void stage6()
    {
        rotationDirection = 0;
    }
    void rangeUpdate()
    {
        rangeTimer -= Time.deltaTime;
        if (rangeTimer <= 0)
        {
            rangeTimer = rangeReset;
            randomSpeed = Random.Range(-4f, 4f);
            rangeChange = true;

        }
    }
}
