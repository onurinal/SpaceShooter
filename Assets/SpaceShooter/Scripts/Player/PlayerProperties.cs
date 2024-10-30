using UnityEngine;

namespace SpaceShooter.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "SpaceShooter/Player/Create New PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        public float playerMouseMoveSpeed = 90f;
        public float playerKeyboardMoveSpeed = 14f;
        public float playerTouchMoveSpeed = 60f;
        public Vector3 playerSpawnPosition;
    }
}