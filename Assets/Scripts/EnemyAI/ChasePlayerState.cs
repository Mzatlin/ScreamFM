﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : EnemyStateBase
{
    EnemyAI enemy;
    Collider2D playerCollider;
    public ChasePlayerState(EnemyAI _enemy) : base(_enemy.gameObject)
    {
        enemy = _enemy;
    }

    public override void BeginState()
    {
        enemy.SetTarget(enemy.PlayerTarget);
        enemy.SetTarget(new EnemyTarget(enemy.PlayerTarget.gameObject, TargetType.Player));
        playerCollider = enemy.PlayerTarget.gameObject.GetComponent<Collider2D>();
    }

    public override Type Tick()
    {
        if (!enemy.GetCanPath())
        {
            enemy.SetCanPath();
        }

        if (playerCollider != null && !playerCollider.enabled)
        {
            return typeof(InvestigateObjectState);
        }
        //If the enemy is determined to be stuck, it must be reset
        if (enemy.isStuck)
        {
            enemy.isStuck = false;
            enemy.SetTarget(new EnemyTarget(enemy.PatrolTargets[0].gameObject, TargetType.None));
            return typeof(PatrolState);
        }

        return null;
    }

}
