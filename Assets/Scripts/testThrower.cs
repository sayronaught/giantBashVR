using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testThrower : MonoBehaviour
{
    public GameObject prefabMissile;
    public Transform hand;
    public Transform target;

    private GameObject heldMissile;

    public void AnimEventPickup()
    {
        Debug.Log(".: Event -> Pickup :.");
        var missile = Instantiate(prefabMissile);
        missile.transform.position = hand.position;
        missile.transform.SetParent(hand);
        heldMissile = missile;
    }

    public void AnimEventThrow()
    {
        Debug.Log(".: Another Event -> Throw :.");
        heldMissile.transform.SetParent(null);
        heldMissile.GetComponent<Rigidbody>().isKinematic = false;
        heldMissile.GetComponent<Rigidbody>().AddForce((target.position - heldMissile.transform.position)*500f);
        Destroy(heldMissile, 15f);
        heldMissile = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
