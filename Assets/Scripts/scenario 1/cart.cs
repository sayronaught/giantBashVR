using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : MonoBehaviour
{
    public Transform cartgoal;
    public float cartSpeed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, cartgoal.position, cartSpeed * Time.deltaTime);

    }
}
