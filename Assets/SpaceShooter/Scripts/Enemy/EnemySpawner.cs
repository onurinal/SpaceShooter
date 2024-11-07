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
            var waypoints = enemyWave.InitializeWaypoints();
            var enemySpawnPosition = waypoints[0].position;
            for (var i = 0; i < enemyWave.NumberOfEnemies; i++)
            {
                var enemy = Instantiate(enemyWave.EnemyPrefab, enemySpawnPosition, Quaternion.identity);
                enemy.Initialize(enemyWave, waypoints);
                yield return new WaitForSeconds(enemyWave.EnemySpawnRate);
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

                // wait 3 second to spawn another wave
                // change it later after some tests
                yield return new WaitForSeconds(3f);
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