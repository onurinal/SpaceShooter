using System.Collections;
using System.Collections.Generic;
using SpaceShooter.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter.Enemy
{
    public class EnemySpawner : MonoBehaviour

    {
        [SerializeField] private EnemyWaveProperties enemyWaveProperties;
        [SerializeField] private List<GameObject> enemyList;
        private readonly List<Vector3> enemyPathWaypoints = new List<Vector3>();
        [SerializeField] private int minEnemies = 4;
        [SerializeField] private int maxEnemies = 7;

        private Vector3 topRightBorder, bottomLeftBorder;
        private IEnumerator enemySpawnCoroutine;

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
            InitializeBorders();
            StartEnemySpawn();
        }

        private void InitializeBorders()
        {
            var spaceManager = SpaceManager.Instance;
            spaceManager.InitializeBorders();
            bottomLeftBorder = spaceManager.bottomLeftBorder;
            topRightBorder = spaceManager.topRightBorder;
        }

        private IEnumerator EnemySpawn()
        {
            InitializeWaypoints();
            var enemyCount = Random.Range(minEnemies, maxEnemies + 1);

            for (var i = 0; i < enemyCount; i++)
            {
                var enemy = Instantiate(enemyList[0], enemyPathWaypoints[0], Quaternion.identity);
                yield return new WaitForSeconds(enemyWaveProperties.enemySpawnInterval);
            }
        }

        private void StartEnemySpawn()
        {
            StopEnemySpawn();
            enemySpawnCoroutine = EnemySpawn();
            StartCoroutine(enemySpawnCoroutine);
        }

        private void StopEnemySpawn()
        {
            if (enemySpawnCoroutine != null)
            {
                StopCoroutine(enemySpawnCoroutine);
                enemySpawnCoroutine = null;
            }
        }

        private void InitializeWaypoints()
        {
            var waypointCount = Random.Range(enemyWaveProperties.minEnemyWaypoints, enemyWaveProperties.maxEnemyWaypoints + 1);

            for (int i = 0; i < waypointCount; i++)
            {
                var waypointPosition = GenerateWaypointPosition(i, waypointCount);
                enemyPathWaypoints.Add(waypointPosition);
            }
        }

        private Vector3 GenerateWaypointPosition(int index, int totalWaypoints)
        {
            // Starting position: Spawn enemy on either left or right side of the screen
            if (index == 0)
            {
                return (totalWaypoints % 2 == 0)
                    ? new Vector3(topRightBorder.x + 1f, Random.Range(0f, topRightBorder.y))
                    : new Vector3(bottomLeftBorder.x - 1f, Random.Range(0f, topRightBorder.y));
            }

            // Middle point: Random positions within screen bounds
            if (index < totalWaypoints - 1)
            {
                return new Vector3(Random.Range(bottomLeftBorder.x / 1.5f, topRightBorder.x / 1.5f), Random.Range(0f, topRightBorder.y / 1.5f));
            }

            // Final position: Top of the screen
            return new Vector3(Random.Range(bottomLeftBorder.x / 1.5f, topRightBorder.x / 1.5f), topRightBorder.y + 1f);
        }

        public List<Vector3> TakeWaypoints()
        {
            return enemyPathWaypoints;
        }
    }
}