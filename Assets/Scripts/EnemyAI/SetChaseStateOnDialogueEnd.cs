﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChaseStateOnDialogueEnd : DisableOnDialogueEndBase
{
    public GameObject enemy;
    public EnemyTarget chaseTarget;
    EnemyAI enemyAI;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if(enemy != null)
        {
            enemyAI = enemy.GetComponent<EnemyAI>();
        }
    }

    protected override void HandleEnd()
    {
        if (enemyAI != null)
        {
            enemyAI.SetTarget(chaseTarget.targetTransform);
            enemyAI.SetTarget(chaseTarget);
        }
    }

}
