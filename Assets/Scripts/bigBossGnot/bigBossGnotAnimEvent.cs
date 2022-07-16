using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossGnotAnimEvent : MonoBehaviour
{

    public bigBossGnot bigBossScript;

    public void eventDoneRoar()
    {
        bigBossScript.eventDoneRoar();
    }

    public void eventReeling()
    {
        bigBossScript.eventReeling();
    }
    public void eventReelingRecover()
    {
        bigBossScript.eventReelingRecover();
    }
    public void eventPickupProjectile()
    {
        bigBossScript.eventPickupProjectile();
    }
    public void eventReleaseProjectile()
    {
        bigBossScript.eventReleaseProjectile();
    }
    public void animEventReleaseProjectile()
    {
        bigBossScript.eventReleaseProjectile();
    }

}
