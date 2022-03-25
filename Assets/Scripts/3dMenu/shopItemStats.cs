using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItemStats : MonoBehaviour
{
    public float playerMaxHit = 100f;
    public float playerRegeneration = 0.2f;

    private _Settings mySettings;
    private EndlessPlayerScript myPlayerScript;

    private void setPlayerStats()
    {
        myPlayerScript.maxHit = playerMaxHit;
        myPlayerScript.hit = playerMaxHit;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        myPlayerScript = GameObject.Find("XR Origin").GetComponent<EndlessPlayerScript>();
        if (myPlayerScript) setPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
