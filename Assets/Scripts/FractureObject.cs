using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private GameObject fracturedVersion;

    private AudioSource myAS;

    private void OnCollisionEnter(Collision other)
    {
        if(!other.transform.CompareTag("Hammer")){return;}
        doFracture();
    }

    public void forceFracture()
    {
        doFracture();
        myAS.Play();
    }

    private void doFracture()   
    {
        Instantiate(fracturedVersion, transform.position, transform.rotation);
        Destroy(this.gameObject);     
    }
    private void Start()
    {
        myAS = GetComponent<AudioSource>();
    }
}
