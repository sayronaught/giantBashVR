using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessPlayerScript : MonoBehaviour
{
    public EndlessSpawner Spawner;
    public TMPro.TextMeshPro uiPoints;
    public GameObject PlayerBloodEffect;
    
    public float maxHit = 100f;
    public float hit = 100f;
    public int points = 0;

    private Vignette bloodVignette;
    private _Settings mySettings;

    public void damagePlayer(float dam)
    {
        hit -= dam;
    }
    public void addPoints(int newPoints)
    {
        points += newPoints;
        uiPoints.text = "Points: "+points.ToString();
        if (mySettings) mySettings.storedPoints += newPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        var volume = PlayerBloodEffect.GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out bloodVignette);
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        hit += Time.deltaTime*0.2f;
        float blood = 1f- (hit / maxHit);
        if (blood < 0f) blood = 0f;
        bloodVignette.intensity.value = blood;
        if (hit > maxHit) hit = maxHit;
        if (hit < 0f ) SceneManager.LoadScene(0);
    }
}
