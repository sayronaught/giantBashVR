using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageText : MonoBehaviour
{
    public TextMesh myText;
    public float visibility = 255f;

    public void setText(string text)
    {
        myText = GetComponent<TextMesh>();
        myText.text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
       // myText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * 0.5f), transform.position.z);
        visibility -= Time.deltaTime * 50f;
        if (visibility <= 0f) Destroy(gameObject);
    }
}
