using SpaceShooter.Enemy;
using UnityEngine;

namespace SpaceShooter.Lasers
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D laserRigidbody2D;
        private LaserProperties laserProperties;

        public void Initialize(LaserProperties laserProperties)
        {
            this.laserProperties = laserProperties;
            InitializeLaserSpeed();
        }

        private void InitializeLaserSpeed()
        {
            laserRigidbody2D.velocity = laserProperties.speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var enemy = collision.gameObject.GetComponentInParent<TheEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(laserProperties.laserDamage);
                Destroy(gameObject);
            }
        }
    }
}