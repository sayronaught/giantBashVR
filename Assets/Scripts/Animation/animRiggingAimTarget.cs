using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animRiggingAimTarget : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(cam != null)
            if(cam.transform.position != transform.position)
                transform.position = cam.transform.position;
    }
}
