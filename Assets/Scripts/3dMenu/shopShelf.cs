using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class shopShelf : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public Vector3[] Positions;
    public pointBank myBank;

    public Transform shelfPositionOpen;
    public Transform shelfPositionClosed;
    public GameObject openShopButton;

    public int currentID = 0;
    private _Settings mySettings;
    private List<GameObject> onShelvesNow;

    private Transform targetPosition;
    private float targetTimer = 0f;
    private bool closing = false;
    private bool opening = false;

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
    public void emptyShelves()
    {
        if (onShelvesNow.Count < 1) return;
        foreach (GameObject item in onShelvesNow) Destroy(item);
        onShelvesNow.Clear();
    }

    public void Next()
    {
        currentID += 4;
        if (currentID >= mySettings.allSkinsForStore.Count) currentID = 0;
        emptyShelves();
        fillShelf();
    }
    public void Close()
    {
        transform.position = shelfPositionOpen.position;
        transform.rotation = shelfPositionOpen.rotation;
        targetPosition = shelfPositionClosed;
        targetTimer = 1f;
        closing = true;
    }
    public void Open()
    {
        transform.position = shelfPositionClosed.position;
        transform.rotation = shelfPositionClosed.rotation;
        transform.localScale = new Vector3(1f, 1f, 1f);
        targetPosition = shelfPositionOpen;
        emptyShelves();
        fillShelf();
        openShopButton.SetActive(false);
        targetTimer = 1f;
        opening = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        onShelvesNow = new List<GameObject>();
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetPosition) return;
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime*5f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPosition.rotation, Time.deltaTime*5f);
        targetTimer -= Time.deltaTime;
        if (targetTimer > 0f) return;
        transform.position = targetPosition.position;
        transform.rotation = targetPosition.rotation;
        targetPosition = null;
        if ( opening )
        {
            openShopButton.SetActive(false);
            opening = false;
        }
        if ( closing )
        {
            openShopButton.SetActive(true);
            transform.localScale = new Vector3(0f, 0f, 0f);
            closing = false;
        }
    }
}
