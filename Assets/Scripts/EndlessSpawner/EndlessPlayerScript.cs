using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EndlessPlayerScript : MonoBehaviour
{
    public EndlessSpawner Spawner;
    public GameObject PlayerBloodEffect;
    


    public float maxHit = 100f;
    public float hit = 100f;


    public void damagePlayer(float dam)
    {
        hit -= dam;
    }
    // Start is called before the first frame update
    void Start()
    {
        var volume = PlayerBloodEffect.GetComponent<Volume>();

        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0.5f;
        }
        //hitpointVignette.
        // var h3 = hitpointVignette;
    }

    // Update is called once per frame
    void Update()
    {
        hit += Time.deltaTime;
        if (hit > maxHit) hit = maxHit;
        if (hit < 0f ) SceneManager.LoadScene(0);
    }
}
