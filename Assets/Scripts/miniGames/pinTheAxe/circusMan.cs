using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circusMan : MonoBehaviour
{
    public pinTheAxeController controller;
    public AudioSource tikTok;

    public float time = 0;
    public float tikvol = 0;
    public float tikpich = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < controller.timeLimit)
        {
            time += Time.deltaTime;
            tikvol = Mathf.Clamp(-controller.timeLimit + time + 100, 0, 100);
            tikTok.volume = tikvol / 100;
            tikpich = Mathf.Clamp(-controller.timeLimit + time + 125, 100, 125);
            tikTok.pitch = tikpich / 100;
        }
        else
        {
            tikTok.volume = 0;
            tikTok.pitch = 0;
        }
    }
}
