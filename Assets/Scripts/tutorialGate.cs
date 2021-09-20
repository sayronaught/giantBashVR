using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialGate : MonoBehaviour
{
    public gameController mainGC;
    public GameObject RamPorten;

    private AudioSource myAS;
    private float deleteTimer = 10f;
    private bool smashed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Hammer")) return;
        foreach (Transform child in transform)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        mainGC.smashedGate();
        RamPorten.SetActive(false);
        smashed = true;
        GetComponent<BoxCollider>().enabled = false;
        myAS.Play();
    }

    // Start is called before the first frame update
    private void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!smashed) return;
        deleteTimer -= Time.deltaTime;
        if (deleteTimer < 0f)
            Destroy(gameObject);
    }
}
