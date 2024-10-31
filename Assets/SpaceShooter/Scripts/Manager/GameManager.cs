﻿using SpaceShooter.Player;
using UnityEngine;

namespace SpaceShooter.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerShipPrefab;
        [SerializeField] private PlayerProperties playerProperties;

        private PlayerController playerController;

        public static GameManager Instance;

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

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            CreateNewShip();
            playerController.Initialize();
        }

        private void CreateNewShip()
        {
            playerController = Instantiate(playerShipPrefab, playerProperties.playerSpawnPosition, Quaternion.identity);
        }
    }
}