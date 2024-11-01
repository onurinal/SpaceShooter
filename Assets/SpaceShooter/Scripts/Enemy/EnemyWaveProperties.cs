using UnityEngine;

namespace SpaceShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Wave", menuName = "SpaceShooter/Enemy/Create New Enemy Wave")]
    public class EnemyWaveProperties : ScriptableObject
    {
        public GameObject enemyPrefab;
        public Transform enemyPath;
        public float enemyMoveSpeed = 5f;
    }
}