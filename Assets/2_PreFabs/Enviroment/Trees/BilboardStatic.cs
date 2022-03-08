using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardStatic : MonoBehaviour
{
    Camera cam;

    void OnEnable()
    {
        //Sets original transfom to keep X & Z values
        Transform originalTrans = transform;

        cam = Camera.main;
        //Looks at camera
        transform.LookAt(cam.transform);

        //Sets new transform of looking at camera to keep Y value
        Transform newTrans = transform;

        //Sets the orignal transform X & Z values and the new Y value
        transform.rotation = Quaternion.Euler(originalTrans.rotation.eulerAngles.x, newTrans.rotation.eulerAngles.y, originalTrans.rotation.eulerAngles.z);
    }

}
