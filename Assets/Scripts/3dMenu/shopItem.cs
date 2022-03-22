using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItem : MonoBehaviour
{

    public bool bought = false;
    public bool activated = false;
    public int price = 0;

    public GameObject hammerSkinPrefab;

    public TextMesh myTxt;

    private _Settings mySettings;

    public void HoverOn()
    {
        transform.position = transform.position + transform.up * 0.2f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        myTxt.color = Color.red;
    }
    public void HoverOff()
    {
        transform.position = transform.position + transform.up * -0.2f;
        transform.localScale = new Vector3(1, 1f, 1f);
        myTxt.color = Color.white;
    }
    public void clickedThis()
    {
        if (mySettings) mySettings.currentHammerSkin = hammerSkinPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
    }

}
