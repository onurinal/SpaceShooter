using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Wave", menuName = "SpaceShooter/Enemy/Create New Enemy Wave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private TheEnemy enemyPrefab;
        [SerializeField] private GameObject pathPrefab;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private int numberOfEnemies = 5;
        [SerializeField] private float enemyMoveSpeed = 4f;
        [SerializeField] private float enemySpawnRate = 0.4f;
        [SerializeField] private int enemyLife;
        [SerializeField] private int enemyPoint;

        [Header("Enemy Shoot Parameters")] [SerializeField]
        private EnemyLaser enemyLaserPrefab;

        [SerializeField] private float enemyLaserSpeed;
        [SerializeField] private int enemyLaserDamage;
        [SerializeField] private float minEnemyFireRate;
        [SerializeField] private float maxEnemyFireRate;

        public TheEnemy EnemyPrefab => enemyPrefab;
        public int NumberOfEnemies => numberOfEnemies;
        public float EnemyMoveSpeed => enemyMoveSpeed;
        public float EnemySpawnRate => enemySpawnRate;
        public int EnemyLife => enemyLife;
        public EnemyLaser EnemyLaserPrefab => enemyLaserPrefab;
        public float EnemyLaserSpeed => enemyLaserSpeed;
        public int EnemyLaserDamage => enemyLaserDamage;
        public float MinEnemyFireRate => minEnemyFireRate;
        public float MaxEnemyFireRate => maxEnemyFireRate;

        public int EnemyPoint => enemyPoint;

        public GameObject ExplosionPrefab => explosionPrefab;


        public List<Transform> InitializeWaypoints()
        {
            var waypoints = new List<Transform>();
            var waypointsCount = pathPrefab.transform.childCount;

            for (int i = 0; i < waypointsCount; i++)
            {
                waypoints.Add(pathPrefab.transform.GetChild(i));
            }

            return waypoints;
        }
    }
}