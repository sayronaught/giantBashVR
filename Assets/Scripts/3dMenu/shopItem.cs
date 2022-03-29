using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItem : MonoBehaviour
{

    public bool bought = false;
    public bool activated = false;
    public int shopPrice = 0;
    public string shopTitle = "name of the skin";
    public string shopStory = "funky story coming soon";
    public string shopStats = "funky stats coming soon";

    public GameObject hammerSkinPrefab;

    public TextMesh myTitle;
    public TextMesh myStory;
    public TextMesh myStats;
    public TextMesh myPrice;
    public GameObject productDisplay;
    public pointBank myBank;

    private _Settings mySettings;
    private float localScale;

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
            } else { // broke ass bitch
                myBank.playSound(myBank.sfxDeny);
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
    }
    private void Update()
    {
        if (activated)
        {
            productDisplay.transform.Rotate(Vector3.up, Time.deltaTime*50f);
            localScale = 1F + (Mathf.Sin(Time.realtimeSinceStartup*5f)*0.25f);
            productDisplay.transform.localScale = new Vector3(localScale, localScale, localScale);
        } else {
            productDisplay.transform.Rotate(Vector3.up, Time.deltaTime * 40f);
            productDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
