using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItemStats : MonoBehaviour
{
    public int shopPrice = 0;
    public string shopTitle = "name of the skin";
    [TextArea]
    public string shopStory = "funky story coming soon";
    [TextArea]
    public string shopStats = "funky stats coming soon";

    public float playerMaxHit = 100f;
    public float playerRegeneration = 0.2f;

    public float weaponThrowForce = 3f;
    public float weaponThrowCharge = 2f;
    public float weaponChargeMax = 9f;
    public float weaponChargeSpeed = 3f;
    public float weaponMagnetMultiplier = 1.1f;
    public float weaponMagnetMinimum = 2f;

    public float weaponMass = 1f;

    public float aoeDamage = 5f;
    public float aoeChargeKnockback = 25f;
    public float aoeBaseKnockback = 2500f;
    public float aoeRange = 5f;

    private _Settings mySettings;
    private EndlessPlayerScript myPlayerScript;
    private hammerControllerEndlessMode myHammerEndless;
    private hammerController myHammerController;
    private hammerFX myHammerFx;
    private Rigidbody myHammerRb;

    private void setPlayerStats()
    {
        myPlayerScript.maxHit = playerMaxHit;
        myPlayerScript.hit = playerMaxHit;
    }
    private void setHammerController()
    {
        myHammerController.statsThrowForce = weaponThrowForce;
        myHammerController.statsThrowCharge = weaponThrowCharge;
        myHammerController.statsMaxCharge = weaponChargeMax;
        myHammerController.statsChargeSpeed = weaponChargeSpeed;
    }
    private void setHammerEndless()
    {
        myHammerEndless.statsThrowForce = weaponThrowForce;
        myHammerEndless.statsThrowCharge = weaponThrowCharge;
        myHammerEndless.statsMaxCharge = weaponChargeMax;
        myHammerEndless.statsChargeSpeed = weaponChargeSpeed;
    }
    private void setHammerFx()
    {
        myHammerFx.statsAoeDamage = aoeDamage;
        myHammerFx.statsAoeChargeKnockback = aoeChargeKnockback;
        myHammerFx.statsAoeBaseKnockback = aoeBaseKnockback;
        myHammerFx.statsAoeRange = aoeRange;
    }
    private void setHammerRb()
    {
        myHammerRb.mass = weaponMass;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        var playerGo = GameObject.Find("XR Origin");
        if ( playerGo ) myPlayerScript = playerGo.GetComponent<EndlessPlayerScript>();
        if (myPlayerScript) setPlayerStats();
        var hammerGo = GameObject.Find("Hammer");
        if (!hammerGo) return;
        myHammerController = hammerGo.GetComponent<hammerController>();
        if (myHammerController) setHammerController();
        myHammerEndless = hammerGo.GetComponent<hammerControllerEndlessMode>();
        if (myHammerEndless) setHammerEndless();
        myHammerFx = hammerGo.GetComponent<hammerFX>();
        if (myHammerFx) setHammerFx();
        myHammerRb = hammerGo.GetComponent<Rigidbody>();
        if (myHammerRb) setHammerRb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
