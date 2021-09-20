using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteChildrenAfterTime : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private float timeBeforeDelete = 5f;

    // Update is called once per frame
    private void Update()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= timeBeforeDelete)) return;
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }
}
