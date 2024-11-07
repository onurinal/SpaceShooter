using System.Collections;
using System.Collections.Generic;
using SpaceShooter.Manager;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class TheEnemy : MonoBehaviour
    {
        private EnemyPathing enemyPathing;
        private EnemyWave enemyWave;
        private int currentLife;

        private IEnumerator laserCoroutine;
        private bool isFiring;

        public void Initialize(EnemyWave enemyWave, List<Transform> waypoints)
        {
            this.enemyWave = enemyWave;
            enemyPathing = new EnemyPathing();
            enemyPathing.Initialize(this, enemyWave, waypoints);
            currentLife = this.enemyWave.EnemyLife;

            StartFireLaser();
        }

        public void TakeDamage(int damage)
        {
            if (currentLife > 0)
            {
                currentLife -= damage;
                if (currentLife <= 0)
                {
                    DestroyEnemy();
                    UIManager.Instance.AddToScore(enemyWave.EnemyPoint);
                }
            }
        }

        public void DestroyEnemy()
        {
            StopFireLaser();
            Destroy(gameObject);
        }

        private IEnumerator FireLaser()
        {
            var minFireRate = enemyWave.MinEnemyFireRate;
            var maxFireRate = enemyWave.MaxEnemyFireRate;
            while (isFiring)
            {
                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
                var laser = Instantiate(enemyWave.EnemyLaserPrefab, transform.position, Quaternion.identity);
                laser.Initialize(enemyWave);
            }
        }

        private void StartFireLaser()
        {
            StopFireLaser();
            isFiring = true;
            laserCoroutine = FireLaser();
            StartCoroutine(laserCoroutine);
        }

        private void StopFireLaser()
        {
            if (laserCoroutine != null)
            {
                StopCoroutine(laserCoroutine);
                laserCoroutine = null;
                isFiring = false;
            }
        }
    }
}