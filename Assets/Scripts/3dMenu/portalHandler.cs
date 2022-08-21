using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class portalHandler : MonoBehaviour
{
    public simpleInteractableOption travelTo;
    public simpleInteractableOption next;
    public TextMesh story;
    public TextMesh travelToText;
    public VideoPlayer video;
    public Transform portalSpinPositionEnd;
    public Transform portalSpin;
    public GameObject videoMesh;
    public GameObject behindVideoMesh;
    public GameObject nextButton;

    private _Settings mySettings;
    private bool spinning = false;
    private float targetTimer = 0f;
    private Vector3 targetScale = new Vector3(1f, 1f, 1f);

    public void updatePortal()
    {
        if (!mySettings) return;
        travelTo.doLoadScene = mySettings.worlds[mySettings.currentWorld].loadOrder;
        story.text = mySettings.worlds[mySettings.currentWorld].story;
        travelToText.text = "Travel to\n" + mySettings.worlds[mySettings.currentWorld].title;
        video.clip = mySettings.worlds[mySettings.currentWorld].videoclip;
        spinning = true;
        targetTimer = 1f;
        videoMesh.SetActive(false);
        travelToText.transform.localScale = Vector3.zero;
        story.transform.localScale = Vector3.zero;
        nextButton.transform.localScale = Vector3.zero;
        behindVideoMesh.SetActive(false);
        //portalSpin.transform.Rotate(Vector3.forward,180);
    }
    public void nextPortal()
    {
        mySettings.currentWorld++;
        if (mySettings.currentWorld >= mySettings.worlds.Count) mySettings.currentWorld = 0;
        updatePortal();
    }
    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        updatePortal();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spinning) return;
        targetTimer -= Time.deltaTime;
        //portalSpin.transform.rotation = Quaternion.Lerp(portalSpin.transform.rotation, portalSpinPositionEnd.rotation, Time.deltaTime * 5f);
        travelToText.transform.localScale = Vector3.Lerp(travelToText.transform.localScale,targetScale , Time.deltaTime * 5f);
        story.transform.localScale = Vector3.Lerp(story.transform.localScale, targetScale, Time.deltaTime * 5f);
        nextButton.transform.localScale = Vector3.Lerp(nextButton.transform.localScale, targetScale, Time.deltaTime * 5f);
        if (targetTimer < 0f )
        {
            spinning = false;
            videoMesh.SetActive(true);
            travelToText.transform.localScale = targetScale;
            nextButton.transform.localScale = targetScale;
            story.transform.localScale = targetScale;
            behindVideoMesh.SetActive(true);
            //portalSpin.transform.rotation = portalSpinPositionEnd.rotation;
        }
    }
}
