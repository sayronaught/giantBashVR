using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
//using DG.Tweening;

public class simpleInteractableOption : MonoBehaviour
{

    private XRSimpleInteractable mySimple;

    public void HoverOn()
    {
        transform.position = transform.position + transform.up*0.1f;
    }
    public void HoverOff()
    {
        transform.position = transform.position + transform.up * -0.1f;
    }
    public void clickedThis()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        mySimple = GetComponent<XRSimpleInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
