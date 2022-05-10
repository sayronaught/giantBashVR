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
    [DrawIf("doNextPortal", true)]
    public portalHandler doNextPortalHandler;
    public bool holdToActivate = false;
    [DrawIf("holdToActivate", true)]
    public float holdTime = 5f;
    private float currentHoldTime = 0f;
    [DrawIf("holdToActivate", true)]
    public Material holdMaterial;

    private XRSimpleInteractable mySimple;
    private TextMesh myTxt;
    private _Settings mySettings;

    private float clickdelay = 0f;

    private XRRayInteractor leftHandRay;
    private XRRayInteractor rightHandRay;
    private UnityEngine.XR.InputDevice lefty;
    private UnityEngine.XR.InputDevice righty;
    private float updateControllerTimer;
    private bool rightPress;
    private bool leftPress;

    private void updateController()
    {
        if (Application.isEditor) return;
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        lefty = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        righty = rightHandDevices[0];
    }

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
        lefty.IsPressed(InputHelpers.Button.Trigger, out leftPress);
        righty.IsPressed(InputHelpers.Button.Trigger, out rightPress);
        if ( rightPress ) rightHandRay.SendHapticImpulse(1f, 0.2f);
        if (leftPress) leftHandRay.SendHapticImpulse(1f, 0.2f);
        if (holdToActivate)
        {
            currentHoldTime += Time.deltaTime * 2f;
        }
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
        leftHandRay = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHandRay = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( holdToActivate )
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime < 0f) currentHoldTime = 0f;
            holdMaterial.SetFloat("Vector1_4", 0f-(holdTime/currentHoldTime));
        }
        clickdelay -= Time.deltaTime;
        updateControllerTimer -= Time.deltaTime;
        if (!(updateControllerTimer < 0f)) return;
        updateController();
        updateControllerTimer = 2f;
    }
}
