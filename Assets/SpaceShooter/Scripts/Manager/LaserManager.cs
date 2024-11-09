using System.Collections;
using System.Collections.Generic;
using SpaceShooter.Lasers;
using UnityEngine;

namespace SpaceShooter.Manager
{
    public class LaserManager : MonoBehaviour
    {
        [SerializeField] private List<LaserProperties> laserProperties;
        private Transform leftLaserSpawnTransform, rightLaserSpawnTransform;
        private IEnumerator laserCoroutine;
        private bool isFiring;

        [SerializeField] private AudioClip laserSound;
        [SerializeField] [Range(0f, 1f)] private float laserVolume = 0.5f;

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

            // START SHOOTING
            StartFireLaser();
        }

        private IEnumerator FireLaser()
        {
            isFiring = true;
            while (isFiring)
            {
                InstantiateLasers();
                PlayLaserSound();
                yield return new WaitForSeconds(laserProperties[0].FireInterval);
            }
        }

        private void InstantiateLasers()
        {
            // For now didn't have many laser, that's why after some new lasers you should create a new method to select the specific laser
            var leftLaser = Instantiate(laserProperties[0].LaserPrefab, leftLaserSpawnTransform.position, Quaternion.identity);
            leftLaser.Initialize(laserProperties[0]);
            var rightLaser = Instantiate(laserProperties[0].LaserPrefab, rightLaserSpawnTransform.position, Quaternion.identity);
            rightLaser.Initialize(laserProperties[0]);
        }

        public void StartFireLaser()
        {
            StopFireLaser();
            laserCoroutine = FireLaser();
            StartCoroutine(laserCoroutine);
        }

        public void StopFireLaser()
        {
            if (laserCoroutine != null)
            {
                StopCoroutine(laserCoroutine);
                laserCoroutine = null;
            }

            isFiring = false;
        }

        private void PlayLaserSound()
        {
            AudioSource.PlayClipAtPoint(laserSound, transform.position, laserVolume);
        }
    }
}