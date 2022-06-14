using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerEmissionMaterial : MonoBehaviour
{
    public Material materialToFlicker;
    public Color minColor;
    public Color maxColor;
    public float minIntensity;
    public float maxIntensity;
    public float delay = 0.1f;

    private float minH;
    private float maxH;
    private float minS;
    private float maxS;
    private float minV;
    private float maxV;


    private float currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        Color.RGBToHSV(minColor, out minH, out minS, out minV);
        Color.RGBToHSV(maxColor, out maxH, out maxS, out maxV);
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay > 0f) return;
        materialToFlicker.SetColor("_EmissionColor", Random.ColorHSV(minH,maxH,minS,maxS,minV,maxV)*Random.Range(minIntensity,maxIntensity));
        currentDelay = delay;
    }
}
