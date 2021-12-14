using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class simpleInteractableOption : MonoBehaviour
{
    public _MenuHandler myMenu;

    private XRSimpleInteractable mySimple;
    private TextMesh myTxt;

    public void HoverOn()
    {
        transform.position = transform.position + transform.up*0.2f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        myTxt.color = Color.red;
    }
    public void HoverOff()
    {
        transform.position = transform.position + transform.up * -0.2f;
        transform.localScale = new Vector3(1, 1f, 1f);
        myTxt.color = Color.white;
    }
    public void clickedThis()
    {
        //SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
        myMenu.beginLoadScreen();
    }

    // Start is called before the first frame update
    void Start()
    {
        mySimple = GetComponent<XRSimpleInteractable>();
        myTxt = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
