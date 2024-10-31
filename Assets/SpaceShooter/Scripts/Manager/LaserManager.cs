using System.Collections;
using SpaceShooter.Lasers;
using UnityEngine;

namespace SpaceShooter.Manager
{
    public class LaserManager : MonoBehaviour
    {
        [SerializeField] private LaserProperties laserProperties;
        [SerializeField] private Laser initialLaserPrefab;
        private Transform leftLaserSpawnPosition;
        private Transform rightLaserSpawnPosition;
        private IEnumerator laserCoroutine;
        private bool isFiring;


        public static LaserManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void Initialize(Transform leftLaserSpawnPosition, Transform rightLaserSpawnPosition)
        {
            if (leftLaserSpawnPosition == null || rightLaserSpawnPosition == null)
            {
                Debug.LogError("Laser spawn positions cannot be null before initializing!");
            }

            this.leftLaserSpawnPosition = leftLaserSpawnPosition;
            this.rightLaserSpawnPosition = rightLaserSpawnPosition;
        }

        private IEnumerator FireLaser()
        {
            isFiring = true;
            while (isFiring)
            {
                var leftLaser = Instantiate(initialLaserPrefab, leftLaserSpawnPosition.position, Quaternion.identity);
                var rightLaser = Instantiate(initialLaserPrefab, rightLaserSpawnPosition.position, Quaternion.identity);
                yield return new WaitForSeconds(laserProperties.fireInterval);
            }
        }

        public void StartFireLaser()
        {
            StopFireLaser();
            laserCoroutine = FireLaser();
            StartCoroutine(laserCoroutine);
        }

        private void StopFireLaser()
        {
            if (laserCoroutine != null)
            {
                StopCoroutine(laserCoroutine);
                laserCoroutine = null;
            }

            isFiring = false;
        }
    }
}