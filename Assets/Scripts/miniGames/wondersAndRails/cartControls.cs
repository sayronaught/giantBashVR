using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartControls : MonoBehaviour
{
    public float speed;
    public GameObject void1;
    public GameObject void2;
    public GameObject void3;
    public GameObject void4;
    public Vector3 target;
    
    // Start is called before the first frame update
    void Start()
    {
        target =  void1.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target , speed * Time.deltaTime );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == (void1))
        {
            transform.position = void2.transform.position;
            target = void3.transform.position;

        }
        if (other.gameObject == (void3))
        {
            transform.position = void4.transform.position;
            target = void1.transform.position;

        }
    }
}
