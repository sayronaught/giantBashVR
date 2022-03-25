using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItemStats : MonoBehaviour
{
    public float playerMaxHit = 100f;
    public float playerRegeneration = 0.2f;

    public float weaponThrowForce = 3f;
    public float weaponThrowCharge = 2f;

    private _Settings mySettings;
    private EndlessPlayerScript myPlayerScript;
    private hammerControllerEndlessMode myHammerEndless;
    private hammerController myHammerController;

    private void setPlayerStats()
    {
        myPlayerScript.maxHit = playerMaxHit;
        myPlayerScript.hit = playerMaxHit;
    }
    private void setHammerController()
    {
        myHammerController.statsThrowForce = weaponThrowForce;
        myHammerController.statsThrowCharge = weaponThrowCharge;
    }
    private void setHammerEndless()
    {
        myHammerEndless.statsThrowForce = weaponThrowForce;
        myHammerEndless.statsThrowCharge = weaponThrowCharge;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        myPlayerScript = GameObject.Find("XR Origin").GetComponent<EndlessPlayerScript>();
        if (myPlayerScript) setPlayerStats();
        myHammerController = GameObject.Find("Hammer").GetComponent<hammerController>();
        if (myHammerController) setHammerController();
        myHammerEndless = GameObject.Find("Hammer").GetComponent<hammerControllerEndlessMode>();
        if (myHammerEndless) setHammerEndless();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
