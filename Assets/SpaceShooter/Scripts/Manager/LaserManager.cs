using System.Collections;
using SpaceShooter.Lasers;
using UnityEngine;

namespace SpaceShooter.Manager
{
    public class LaserManager : MonoBehaviour
    {
        [SerializeField] private LaserProperties laserProperties;
        [SerializeField] private Laser initialLaserPrefab;
        private Transform leftLaserSpawnTransform, rightLaserSpawnTransform;
        private IEnumerator laserCoroutine;
        private bool isFiring;


        public static LaserManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void Initialize(Transform leftLaserSpawnTransform, Transform rightLaserSpawnTransform)
        {
            if (leftLaserSpawnTransform == null || rightLaserSpawnTransform == null)
            {
                Debug.LogError("Laser spawn positions cannot be null before initializing!");
            }

            this.leftLaserSpawnTransform = leftLaserSpawnTransform;
            this.rightLaserSpawnTransform = rightLaserSpawnTransform;
        }

        private IEnumerator FireLaser()
        {
            isFiring = true;
            while (isFiring)
            {
                var leftLaser = Instantiate(initialLaserPrefab, leftLaserSpawnTransform.position, Quaternion.identity);
                var rightLaser = Instantiate(initialLaserPrefab, rightLaserSpawnTransform.position, Quaternion.identity);
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