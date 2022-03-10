using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicEnemyAnimationEvents : MonoBehaviour
{
    private dynamicEnemy myDE;

    // Start is called before the first frame update
    void Start()
    {
        myDE=transform.parent.gameObject.GetComponent<dynamicEnemy>();
    }

    public void animEventReleaseProjectile()
    {
        myDE.animEventReleaseProjectile();
    }
}
