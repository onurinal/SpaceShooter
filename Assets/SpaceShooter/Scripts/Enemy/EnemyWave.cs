using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Wave", menuName = "SpaceShooter/Enemy/Create New Enemy Wave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private TheEnemy enemyPrefab;
        [SerializeField] private GameObject pathPrefab;
        [SerializeField] private int numberOfEnemies = 5;
        [SerializeField] private float enemyMoveSpeed = 4f;
        [SerializeField] private float enemySpawnRate = 0.4f;
        [SerializeField] private int enemyLife;

        [Header("Enemy Shoot Parameters")] [SerializeField]
        private EnemyLaser enemyLaserPrefab;

        [SerializeField] private float enemyLaserSpeed;
        [SerializeField] private int enemyLaserDamage;
        [SerializeField] private float minEnemyFireRate;
        [SerializeField] private float maxEnemyFireRate;

        public TheEnemy GetEnemyPrefab()
        {
            return enemyPrefab;
        }

        public EnemyLaser GetEnemyLaserPrefab()
        {
            return enemyLaserPrefab;
        }

        public int GetNumberOfEnemies()
        {
            return numberOfEnemies;
        }

        public float GetEnemyMoveSpeed()
        {
            return enemyMoveSpeed;
        }

        public float GetEnemySpawnRate()
        {
            return enemySpawnRate;
        }

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

        public int GetEnemyLife()
        {
            return enemyLife;
        }

        public float GetEnemyLaserSpeed()
        {
            return enemyLaserSpeed;
        }

        public float GetMinEnemyFireRate()
        {
            return minEnemyFireRate;
        }

        public float GetMaxEnemyFireRate()
        {
            return maxEnemyFireRate;
        }

        public int GetEnemyLaserDamage()
        {
            return enemyLaserDamage;
        }
    }
}