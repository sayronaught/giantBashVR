using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MenuHandler : MonoBehaviour
{

    public GameObject village;
    public GameObject loadScreen;

    public void startLoadScreen()
    {
        village.SetActive(false);
        loadScreen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
