using SpaceShooter.Player;
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
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 90;
            CreateNewShip();
            playerController.Initialize();
        }

        private void CreateNewShip()
        {
            playerController = Instantiate(playerShipPrefab, playerProperties.playerSpawnPosition, Quaternion.identity);
        }
    }
}