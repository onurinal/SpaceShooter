using UnityEngine;

namespace SpaceShooter.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "SpaceShooter/Player/Create New PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        [Header("Movement parameters")] [SerializeField]
        private float mouseMoveSpeed = 90f;

        [SerializeField] private float keyboardMoveSpeed = 14f;
        [SerializeField] private float touchMoveSpeed = 60f;

        [SerializeField] private Vector3 playerSpawnPosition;
        [SerializeField] private int playerMaxHealth = 3;
        [SerializeField] private int playerLife = 3;

        public float MouseMoveSpeed => mouseMoveSpeed;
        public float KeyboardMoveSpeed => keyboardMoveSpeed;
        public float TouchMoveSpeed => touchMoveSpeed;
        public Vector3 PlayerSpawnPosition => playerSpawnPosition;
        public int PlayerMaxHealth => playerMaxHealth;
        public int PlayerLife => playerLife;
    }
}