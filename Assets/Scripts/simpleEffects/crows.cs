using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crows : MonoBehaviour
{
    public AudioSource myAS;

    public AudioClip[] cawing;

    public float cawTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        cawTimer -= Time.deltaTime;
        if (cawTimer > 0f || myAS.isPlaying) return;
        myAS.clip = cawing[Random.Range(0, cawing.Length)];
        myAS.pitch = Random.Range(0.9f, 1.1f);
        myAS.Play();
        cawTimer = Random.Range(1.5f, 4f);
    }
}
