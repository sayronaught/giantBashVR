using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItem : MonoBehaviour
{

    public bool bought = false;
    public bool activated = false;
    public int price = 0;

    public GameObject hammerSkinPrefab;

    public TextMesh myTitle;
    public TextMesh myStory;
    public TextMesh myStats;
    public TextMesh myPrice;
    public GameObject productDisplay;

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
        transform.localScale = new Vector3(1, 1f, 1f);
        if (bought) myTitle.color = Color.yellow;
        else myTitle.color = Color.white;
        myStory.gameObject.SetActive(true);
        myStats.gameObject.SetActive(false);
    }
    public void clickedThis()
    {
        if (mySettings) mySettings.currentHammerSkin = hammerSkinPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        if ( mySettings )
        {
            if (mySettings.currentHammerSkin == hammerSkinPrefab) activated = true;
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
            productDisplay.transform.Rotate(Vector3.up, Time.deltaTime*20f);
            localScale = 1F + (Mathf.Sin(Time.realtimeSinceStartup*5f)*0.25f);
            productDisplay.transform.localScale = new Vector3(localScale, localScale, localScale);
        } else {
            productDisplay.transform.rotation = Quaternion.identity;
            productDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
