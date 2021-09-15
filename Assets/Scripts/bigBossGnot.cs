using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossGnot : MonoBehaviour
{

    public float Hitpoints = 100f;

    private AudioSource myAS;

    public void Roar()
    {
        myAS.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
