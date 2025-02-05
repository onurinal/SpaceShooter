﻿using SpaceShooter.Enemy;
using SpaceShooter.Lasers;
using UnityEngine;

public class DestroyWhenHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var laser = collision.GetComponentInParent<Laser>();
        if (laser != null)
        {
            Destroy(laser.gameObject);
        }

        var enemyLaser = collision.GetComponentInParent<EnemyLaser>();
        if (enemyLaser != null)
        {
            Destroy(enemyLaser.gameObject);
        }
    }
}