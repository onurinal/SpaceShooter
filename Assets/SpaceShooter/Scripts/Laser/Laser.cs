using UnityEngine;

namespace SpaceShooter.Lasers
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LaserProperties laserProperties;
        [SerializeField] private Rigidbody2D laserRigidbody2D;

        private void Start()
        {
            laserRigidbody2D.velocity = laserProperties.speed;
            Destroy(gameObject, 2f);
        }
    }
}