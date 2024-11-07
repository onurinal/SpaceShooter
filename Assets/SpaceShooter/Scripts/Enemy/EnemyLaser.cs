using SpaceShooter.Manager;
using SpaceShooter.Player;
using UnityEngine;

namespace SpaceShooter.Enemy
{
    public class EnemyLaser : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D myRigidbody2D;
        private EnemyWave enemyWave;

        public void Initialize(EnemyWave enemyWave)
        {
            this.enemyWave = enemyWave;
            myRigidbody2D.velocity = new Vector2(0f, -enemyWave.EnemyLaserSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                GameManager.Instance.PlayerTakeDamage(enemyWave.EnemyLaserDamage);
                Destroy(gameObject);
            }
        }
    }
}