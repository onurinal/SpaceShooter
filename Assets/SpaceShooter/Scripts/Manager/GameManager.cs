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
            ResetPlayerLifeAndMaxHealth();
        }

        private void ResetPlayerLifeAndMaxHealth()
        {
            currentPlayerLife = playerProperties.PlayerLife;
            currentPlayerHealth = playerProperties.PlayerMaxHealth;
        }

        private void CreateNewShip()
        {
            player = Instantiate(playerPrefab, playerProperties.PlayerSpawnPosition, Quaternion.identity);
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
            RemoveLifeIcon();
            currentPlayerLife--;
            if (currentPlayerLife <= 0)
            {
                Debug.Log("Game Over");
                return;
            }

            currentPlayerHealth = playerProperties.PlayerMaxHealth; // reset player health after losing life
            CreateNewShip();
        }

        private void RemoveLifeIcon()
        {
            UIManager.Instance.RemoveLifeIcon(currentPlayerLife);
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