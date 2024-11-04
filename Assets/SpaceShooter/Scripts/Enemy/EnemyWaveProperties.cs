using UnityEngine;

namespace SpaceShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Wave", menuName = "SpaceShooter/Enemy/Create New Enemy Wave")]
    public class EnemyWaveProperties : ScriptableObject
    {
        public float enemyMoveSpeed = 4f;

        public int minEnemyWaypoints = 5;
        public int maxEnemyWaypoints = 8;
        public float enemySpawnInterval = 0.4f;
    }
}