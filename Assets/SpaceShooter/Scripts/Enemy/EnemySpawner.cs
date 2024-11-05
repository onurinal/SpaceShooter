using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemySpawner : MonoBehaviour

    {
        [SerializeField] private List<EnemyWave> enemyWave;
        private IEnumerator enemySpawnCoroutine;


        private IEnumerator enemyWaveCoroutine; // to manage wave coroutines
        private bool isLastEnemyOfWavePathing;

        public static EnemySpawner Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            StartSpawnWaves();
        }

        private IEnumerator SpawnAllEnemies(EnemyWave enemyWave)
        {
            var enemySpawnPosition = enemyWave.InitializeWaypoints()[0];
            for (var i = 0; i < enemyWave.GetNumberOfEnemies(); i++)
            {
                var enemy = Instantiate(enemyWave.GetEnemyPrefab(), enemySpawnPosition.position, Quaternion.identity);
                enemy.InitializeEnemyWave(enemyWave);
                yield return new WaitForSeconds(enemyWave.GetEnemySpawnRate());

                // Until last enemy's pathing is over, do not spawn other waves
                while (i == (enemyWave.GetNumberOfEnemies() - 1) && enemy.GetIsEnemyPathing() == true)
                {
                    yield return null;
                }
            }
        }

        private void StopSpawnAllEnemies()
        {
            if (enemySpawnCoroutine != null)
            {
                StopCoroutine(enemySpawnCoroutine);
                enemySpawnCoroutine = null;
            }
        }

        private IEnumerator SpawnAllWaves()
        {
            foreach (var currentWave in enemyWave)
            {
                yield return StartCoroutine(SpawnAllEnemies(currentWave));
            }
        }

        private void StartSpawnWaves()
        {
            StopSpawnWaves();
            enemyWaveCoroutine = SpawnAllWaves();
            StartCoroutine(enemyWaveCoroutine);
        }

        private void StopSpawnWaves()
        {
            if (enemyWaveCoroutine != null)
            {
                StopCoroutine(enemyWaveCoroutine);
                enemyWaveCoroutine = null;
            }
        }
    }
}