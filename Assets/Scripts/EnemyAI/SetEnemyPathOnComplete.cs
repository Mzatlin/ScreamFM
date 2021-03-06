﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyPathOnComplete : MonoBehaviour
{
    public GameObject enemy;

    public EnemyTarget newTargetToChange;
    EnemyTarget previousTarget;

    EnemyAI enemyAI => enemy?.GetComponent<EnemyAI>();
    // Start is called before the first frame update
    ICompleteGame Complete => GetComponent<ICompleteGame>();

    void Start()
    {
        if (Complete != null)
        {
            Complete.OnComplete += HandleComplete;
        }
    }

    void OnDestroy()
    {
        if (Complete != null)
        {
            Complete.OnComplete -= HandleComplete;
        }
    }

    private void HandleComplete()
    {
        SetTarget(); //ToDo, make possible extension method for this
    }
    void SetTarget()
    {

        if (enemyAI.target.typeOfTarget != newTargetToChange.typeOfTarget)
        {
            enemyAI.SetCanPath();
            previousTarget = enemyAI.target;
            enemyAI.SetTarget(newTargetToChange);
        }
    }

}
