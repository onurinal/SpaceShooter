using UnityEngine;

namespace SpaceShooter.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "SpaceShooter/Player/Create New PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        public float playerTouchMoveSpeed;
        public float playerKeyboardMoveSpeed;
        public Vector3 playerSpawnPosition;
    }
}