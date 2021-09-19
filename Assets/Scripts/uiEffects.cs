using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiEffects : MonoBehaviour
{
    public bool scaleTo = false;

    public Vector3 targetScale;
    public float scaleSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( scaleTo )
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime*scaleSpeed);
            if (transform.localScale == targetScale) scaleTo = false;
        }
    }
}
