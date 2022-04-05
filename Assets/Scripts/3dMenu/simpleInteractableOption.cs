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

    public int doLoadScene = -1;
    public Transform doTeleport;
    public bool doQuit = false;
    public bool doNextPortal = false;
    public portalHandler doNextPortalHandler;

    private XRSimpleInteractable mySimple;
    private TextMesh myTxt;
    private _Settings mySettings;

    private float clickdelay = 0f;

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
        if (clickdelay > 0f) return;
        if (doLoadScene > -1)
        {
            SceneManager.LoadSceneAsync(doLoadScene, LoadSceneMode.Single);
            myMenu.beginLoadScreen();
        }
        if (doTeleport)
        {
            foreach (Transform child in transform.parent)
            {
                child.gameObject.SetActive(true);
            }
            //transform.parent.GetChild
            GameObject.Find("XR Rig").transform.position = doTeleport.position;
            gameObject.SetActive(false);
        }
        if ( doQuit )
        {
            Application.Quit();
        }
        if (doNextPortal)
        {
            doNextPortalHandler.nextPortal();
            clickdelay = 1f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySimple = GetComponent<XRSimpleInteractable>();
        myTxt = GetComponent<TextMesh>();
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        clickdelay -= Time.deltaTime;
    }
}
