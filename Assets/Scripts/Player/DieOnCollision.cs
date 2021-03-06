﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnCollision : MonoBehaviour, IDie
{
    IPlayerState player => GetComponent<IPlayerState>();
    Transform enemy;
    LayerMask enemyLayer;
    [SerializeField] GameObject mainCamera;
    bool isDead;
    public event Action OnDie = delegate { };

    // Start is called before the first frame update
    void Awake()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        isDead = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & enemyLayer) != 0)
        {

            if (!isDead)
            {
                isDead = true;
                player.PlayerState.isDead = true;
                collision.gameObject.SetActive(false);
                StartCoroutine(DeathDelay());
            }

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & enemyLayer) != 0)
        {
            if (!isDead)
            {
                isDead = true;
                player.PlayerState.isDead = true;
                collision.gameObject.SetActive(false);
                StartCoroutine(DeathDelay());
            }
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.1f);
        OnDie();
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

}
