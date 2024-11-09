using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemyPathing
    {
        private TheEnemy enemy;
        private EnemyWave enemyWave;
        private List<Transform> waypoints = new List<Transform>();
        private IEnumerator enemyPathingCoroutine;
        private int currentWaypointIndex = 0;

        public void Initialize(TheEnemy enemy, EnemyWave enemyWave, List<Transform> waypoints)
        {
            this.enemy = enemy;
            this.enemyWave = enemyWave;
            this.waypoints = waypoints;
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
            var targetPosition = waypoints[currentWaypointIndex].position;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, Time.deltaTime * enemyWave.EnemyMoveSpeed);
            if (enemy.transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }

        private void StartEnemyPathing()
        {
            if (enemyPathingCoroutine == null)
            {
                enemyPathingCoroutine = MoveEnemyPathing();
                enemy.StartCoroutine(enemyPathingCoroutine);
            }
        }

        private void StopEnemyPathing()
        {
            if (enemyPathingCoroutine != null)
            {
                enemy.StopCoroutine(enemyPathingCoroutine);
                enemyPathingCoroutine = null;
                enemy.DestroyEnemy();
            }
        }
    }
}