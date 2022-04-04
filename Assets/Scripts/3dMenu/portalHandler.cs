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

    private _Settings mySettings;

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        if ( mySettings )
        {
            travelTo.doLoadScene = mySettings.worlds[mySettings.currentWorld].loadOrder;
            story.text = mySettings.worlds[mySettings.currentWorld].story;
            travelToText.text = "Travel to\n" + mySettings.worlds[mySettings.currentWorld].title;
            video.clip = mySettings.worlds[mySettings.currentWorld].videoclip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
