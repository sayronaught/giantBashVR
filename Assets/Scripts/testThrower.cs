using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testThrower : MonoBehaviour
{
    public GameObject prefabMissile;
    public Transform hand;
    public Transform target;

    private GameObject heldMissile;

    private Vector3 targetRandomizer(Vector3 thisTarget,float variation)
    {
        thisTarget += new Vector3(Random.Range(-variation,variation), Random.Range(-variation, variation), Random.Range(-variation, variation));
        return thisTarget;
    }
    public void eventReeling()
    { }
    public void eventReelingRecover()
    { }
    public void eventPickupProjectile()
    {
        Debug.Log(".: Event -> Pickup :.");
        var missile = Instantiate(prefabMissile);
        missile.transform.position = hand.position;
        missile.transform.SetParent(hand);
        heldMissile = missile;
    }

    public void eventReleaseProjectile()
    {
        Debug.Log(".: Another Event -> Throw :.");
        heldMissile.transform.SetParent(null);
        //heldMissile.transform.rotation = Quaternion.identity;
        var rb = heldMissile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(((targetRandomizer(target.position, 1f) + (Vector3.up*5f)) - heldMissile.transform.position)*50f);
        rb.maxAngularVelocity = Mathf.Infinity;
        rb.AddRelativeTorque(Vector3.up * 4f);
        heldMissile.GetComponent<enemyThrowingAxe>().isThrown = true;
        heldMissile.GetComponent<AudioSource>().Play();
        //Destroy(heldMissile, 15f);
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
