using SpaceShooter.Player;
using UnityEngine;

namespace SpaceShooter.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private PlayerProperties playerProperties;

        private int currentPlayerLife;
        private int currentPlayerHealth;

        private PlayerController player;

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

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            CreateNewShip();
            currentPlayerLife = playerProperties.playerLife;
            currentPlayerHealth = playerProperties.playerMaxHealth;
        }

        private void CreateNewShip()
        {
            player = Instantiate(playerPrefab, playerProperties.playerSpawnPosition, Quaternion.identity);
            player.Initialize();
            LaserManager.Instance.StartFireLaser();
        }

        private void DestroyShip()
        {
            LaserManager.Instance.StopFireLaser();
            Destroy(player.gameObject);
        }

        private void RemoveLife()
        {
            currentPlayerLife--;
            if (currentPlayerLife <= 0)
            {
                Debug.Log("Game Over");
                return;
            }

            currentPlayerHealth = playerProperties.playerMaxHealth; // reset player health after losing life
            CreateNewShip();
        }

        public void PlayerTakeDamage(int damage)
        {
            currentPlayerHealth -= damage;
            if (currentPlayerHealth <= 0)
            {
                DestroyShip();
                RemoveLife();
            }
        }
    }
}