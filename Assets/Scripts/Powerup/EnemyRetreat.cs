using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreat : PowerupBase
{
    public override void Activate()
    {
        FindObjectOfType<EnemyManager>().MakeEnemiesRetreat();
        base.Activate();
    }
}