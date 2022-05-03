using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine;

public class shopItem : MonoBehaviour
{

    public bool bought = false;
    public bool activated = false;
    public int shopPrice = 0;

    public GameObject hammerSkinPrefab;

    public TextMesh myTitle;
    public TextMesh myStory;
    public TextMesh myStats;
    public TextMesh myPrice;
    public GameObject productDisplay;
    public pointBank myBank;

    private _Settings mySettings;
    private float localScale;

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
        updateControllerTimer -= Time.deltaTime;
        if (!(updateControllerTimer < 0f)) return;
        updateControllerTimer = 2f;
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        lefty = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        righty = rightHandDevices[0];
    }
    private void hapticFeedback()
    {
        lefty.IsPressed(InputHelpers.Button.Trigger, out leftPress);
        righty.IsPressed(InputHelpers.Button.Trigger, out rightPress);
        if (rightPress) rightHandRay.SendHapticImpulse(1f, 0.2f);
        if (leftPress) leftHandRay.SendHapticImpulse(1f, 0.2f);
    }

    public void HoverOn()
    {
        transform.position = transform.position + transform.up * 0.2f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        myTitle.color = Color.red;
        myStory.gameObject.SetActive(false);
        myStats.gameObject.SetActive(true);
    }
    public void HoverOff()
    {
        transform.position = transform.position + transform.up * -0.2f;
        resetItem();
    }
    public void resetItem()
    {
        transform.localScale = new Vector3(1, 1f, 1f);
        if (bought) myTitle.color = Color.yellow;
        else myTitle.color = Color.white;
        myStory.gameObject.SetActive(true);
        myStats.gameObject.SetActive(false);
    }
    public void clickedThis()
    {
        if (!mySettings) return;
        if ( bought && !activated )
        { // select existing
            myBank.playSound(myBank.sfxSelect);
            mySettings.currentHammerSkin = hammerSkinPrefab;
            myBank.activeSkin.activated = false;
            myBank.activeSkin.resetItem();
            myBank.activeSkin = this;
            activated = true;
            hapticFeedback();
        }
        if (!bought)
        { // buy it
            if (mySettings.storedPoints >= shopPrice)
            { // rich
                myBank.playSound(myBank.sfxBuy);
                bought = true;
                mySettings.boughtSkins.Add(hammerSkinPrefab);
                mySettings.currentHammerSkin = hammerSkinPrefab;
                mySettings.storedPoints -= shopPrice;
                myPrice.gameObject.SetActive(false);
                myBank.activeSkin.activated = false;
                myBank.activeSkin.resetItem();
                myBank.activeSkin = this;
                activated = true;
                hapticFeedback();
            } else { // broke ass bitch
                myBank.playSound(myBank.sfxDeny);
                hapticFeedback();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        if ( mySettings )
        {
            if (mySettings.currentHammerSkin == hammerSkinPrefab)
            {
                activated = true;
                myBank.activeSkin = this;
            }
            foreach ( GameObject skinBought in mySettings.boughtSkins)
            {
                if (skinBought == hammerSkinPrefab)
                {
                    bought = true;
                    myTitle.color = Color.yellow;
                    myPrice.gameObject.SetActive(false);
                }
            }
        }
        HoverOff();
        leftHandRay = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHandRay = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }
    private void Update()
    {
        updateController();
        if (!productDisplay) return;
        if (activated)
        {
            productDisplay.transform.Rotate(Vector3.up, Time.deltaTime*50f);
            localScale = 5F + (Mathf.Sin(Time.realtimeSinceStartup*5f)*0.25f);
            productDisplay.transform.localScale = new Vector3(localScale, localScale, localScale);
        } else {
            productDisplay.transform.Rotate(Vector3.up, Time.deltaTime * 40f);
            productDisplay.transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }
}
