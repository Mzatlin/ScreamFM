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
        playerCollider = enemy.PlayerTarget.gameObject.GetComponent<Collider2D>();
    }

    public override Type Tick()
    {
        if(playerCollider != null && !playerCollider.enabled)
        {
            return typeof(PatrolState);
        }

        return null;
    }

}