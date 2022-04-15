using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopShelf : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public Vector3[] Positions;
    public pointBank myBank;

    public int currentID = 0;
    private _Settings mySettings;
    private List<GameObject> onShelvesNow;

    private void fillShelfOneItem(int itemNr,Vector3 pos)
    {
        if (itemNr >= mySettings.allSkinsForStore.Count) return;
        var item = Instantiate(shopItemPrefab,transform);
        item.transform.localPosition = pos;
        item.transform.localRotation = Quaternion.identity;
        var SI = item.GetComponent<shopItem>();
        SI.myBank = myBank;
        SI.hammerSkinPrefab = mySettings.allSkinsForStore[itemNr];
        var display = Instantiate(mySettings.allSkinsForStore[itemNr], item.transform.Find("SkinHolder").transform);
        display.transform.localPosition = Vector3.zero;
        display.transform.localRotation = Quaternion.identity;
        SI.productDisplay = display;
        var SIS = display.GetComponent<shopItemStats>();
        item.transform.name = SIS.name;
        SI.myTitle.text = SIS.shopTitle;
        SI.myPrice.text = SIS.shopPrice.ToString() + " Points";
        SI.shopPrice = SIS.shopPrice;
        SI.myStats.text = SIS.shopStats;
        SI.myStory.text = SIS.shopStory;
        onShelvesNow.Add(item);
    }

    public void fillShelf()
    {
        for ( int i = 0; i<4; i++)
        {
            fillShelfOneItem(i+currentID,Positions[i]);
        }
    }

    public void Next()
    {
        currentID += 4;
        if (currentID >= mySettings.allSkinsForStore.Count) currentID = 0;
        foreach(GameObject item in onShelvesNow) Destroy(item);
        onShelvesNow.Clear();
        fillShelf();
    }
    public void Close()
    {

    }
    public void Open()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        onShelvesNow = new List<GameObject>();
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
        if ( mySettings ) fillShelf();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
