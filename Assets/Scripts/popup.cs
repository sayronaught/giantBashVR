using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    private float opacity = 2f;

    private RawImage myImg;
    private RectTransform myRect;

    // Start is called before the first frame update
    private void Start()
    {
        myImg = GetComponent<RawImage>();
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        myImg.color = new Color(1f, 1f, 1f, Mathf.Clamp(opacity,0f,1f));
        //myMR.material.color = new Color(1f, 1f, 1f, opacity);
        opacity -= (Time.deltaTime * 0.5f);
        if (opacity < 0f) Destroy(gameObject);
        myRect.localPosition = new Vector3(0f, myRect.localPosition.y + (Time.deltaTime * 50f), 0f);
        //transform.position = Vector3.MoveTowards(transform.position + (Vector3.up*0.005f)+(Vector3.forward*0.005f), transform.position, Time.deltaTime*0.0000001f);
    }
}
