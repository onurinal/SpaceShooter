using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        [SerializeField] private EnemyWaveProperties enemyWave;

        public List<Vector3> waypoints = new List<Vector3>();
        private IEnumerator enemyPathingCoroutine;
        private int currentWaypointIndex = 0;

        private void Start()
        {
            InitializeWaypoints();
            StartEnemyPathing();
        }

        private IEnumerator MoveEnemyPathing()
        {
            while (currentWaypointIndex < waypoints.Count)
            {
                MoveTowardsNextWaypoint();
                yield return null;
            }

            StopEnemyPathing();
        }

        private void MoveTowardsNextWaypoint()
        {
            var targetPosition = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * enemyWave.enemyMoveSpeed);
            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }

        private void StartEnemyPathing()
        {
            if (enemyPathingCoroutine == null)
            {
                enemyPathingCoroutine = MoveEnemyPathing();
                StartCoroutine(enemyPathingCoroutine);
            }
        }

        private void StopEnemyPathing()
        {
            if (enemyPathingCoroutine != null)
            {
                StopCoroutine(enemyPathingCoroutine);
                enemyPathingCoroutine = null;
                Destroy(gameObject);
            }
        }

        private void InitializeWaypoints()
        {
            var enemySpawner = EnemySpawner.Instance;
            waypoints = enemySpawner.TakeWaypoints();
        }
    }
}