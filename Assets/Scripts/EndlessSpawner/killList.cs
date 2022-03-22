using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killList
{
    public int bigBossGnot = 0;
    public int smurf = 0;
    public int jotunnSmall = 0;
    public int jotunnShaman = 0;
    public int jotunnBulky = 0;
    public void addKills(monsterList type)
    {
        if (type.bigBossGnot) bigBossGnot++;
        if (type.smurf) smurf++;
        if (type.jotunnSmall) jotunnSmall++;
        if (type.jotunnShaman) jotunnShaman++;
        if (type.jotunnBulky) jotunnBulky++;
    }
}
