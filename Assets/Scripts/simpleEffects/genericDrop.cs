using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericDrop : MonoBehaviour
{
    public bool spinChild = false;
    public bool bounceChild = false;

    private Transform child;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spinChild && child) child.Rotate(Vector3.up * Time.deltaTime * 50f);
        if (bounceChild && child) child.localPosition = new Vector3(0f, Mathf.Abs(Mathf.Sin(Time.fixedUnscaledTime)), 0f);
    }
}
