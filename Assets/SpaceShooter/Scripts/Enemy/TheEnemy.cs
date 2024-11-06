using System.Collections;
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

        public void Initialize(EnemyWave enemyWave)
        {
            this.enemyWave = enemyWave;
            enemyPathing = new EnemyPathing();
            enemyPathing.Initialize(this, enemyWave);
            currentLife = this.enemyWave.GetEnemyLife();

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
            var minFireRate = enemyWave.GetMinEnemyFireRate();
            var maxFireRate = enemyWave.GetMaxEnemyFireRate();
            while (isFiring)
            {
                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
                var laser = Instantiate(enemyWave.GetEnemyLaserPrefab(), transform.position, Quaternion.identity);
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