using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        private EnemyWave enemyWave;
        private List<Transform> waypoints = new List<Transform>();
        private IEnumerator enemyPathingCoroutine;
        private int currentWaypointIndex = 0;
        private bool isEnemyPathing = false;

        private void Start()
        {
            InitializeWaypoints();
            StartEnemyPathing();
        }

        public void InitializeEnemyWave(EnemyWave enemyWave)
        {
            this.enemyWave = enemyWave;
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
            var targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * enemyWave.GetEnemyMoveSpeed());
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
                isEnemyPathing = true;
            }
        }

        private void StopEnemyPathing()
        {
            if (enemyPathingCoroutine != null)
            {
                StopCoroutine(enemyPathingCoroutine);
                enemyPathingCoroutine = null;
                isEnemyPathing = false;
                Destroy(gameObject);
            }
        }

        private void InitializeWaypoints()
        {
            waypoints = enemyWave.InitializeWaypoints();
        }

        public bool GetIsEnemyPathing()
        {
            return isEnemyPathing;
        }
    }
}