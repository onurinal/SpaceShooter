using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        [SerializeField] private EnemyWaveProperties enemyWave;

        // waypoints for pathing
        private readonly List<Transform> enemyWaypoints = new List<Transform>();
        private int currentWaypointIndex;

        private void Start()
        {
            InitializeWaypoints();
            SpawnEnemy();
        }

        // will do another script to instantiate enemies
        private void SpawnEnemy()
        {
            transform.position = enemyWaypoints[currentWaypointIndex].position;
        }

        private void Update()
        {
            MoveAlongPath();
        }

        private void MoveAlongPath()
        {
            if (currentWaypointIndex < enemyWaypoints.Count)
            {
                var targetPosition = enemyWaypoints[currentWaypointIndex].position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * enemyWave.enemyMoveSpeed);
                if (transform.position == targetPosition)
                {
                    currentWaypointIndex++;
                }
            }

            if (currentWaypointIndex == enemyWaypoints.Count)
            {
                Destroy(gameObject);
            }
        }

        private void InitializeWaypoints()
        {
            var waypointCount = enemyWave.enemyPath.childCount;
            for (var i = 0; i < waypointCount; i++)
            {
                enemyWaypoints.Add(enemyWave.enemyPath.GetChild(i).transform);
            }
        }
    }
}