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
    public float regeneration = 0.2f;
    public int points = 0;

    private Vignette bloodVignette;
    private _Settings mySettings;
    private float updateStats = 0f;

    public void damagePlayer(float dam)
    {
        hit -= dam;
        updatePoints();
    }
    public void addPoints(int newPoints)
    {
        points += newPoints;
        if (mySettings) mySettings.storedPoints += newPoints;
        updatePoints();
    }
    private void updatePoints()
    {
        uiPoints.text = "Points: " + points.ToString() + "\nHP: " + ((int)hit).ToString() + "/" + ((int)maxHit).ToString();
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
        hit += Time.deltaTime* regeneration;
        float blood = 1f- (hit / maxHit);
        if (blood < 0f) blood = 0f;
        bloodVignette.intensity.value = blood;
        if (hit > maxHit) hit = maxHit;
        if (hit < 0f ) SceneManager.LoadScene(0);
        updateStats -= Time.deltaTime;
        if ( updateStats<0f)
        {
            updateStats = 1f;
            updatePoints();
        }
    }
}
