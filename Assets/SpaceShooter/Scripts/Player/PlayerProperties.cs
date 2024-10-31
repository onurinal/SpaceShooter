using UnityEngine;

namespace SpaceShooter.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "SpaceShooter/Player/Create New PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        public float mouseMoveSpeed = 90f;
        public float keyboardMoveSpeed = 14f;
        public float touchMoveSpeed = 60f;
        public Vector3 playerSpawnPosition;
    }
}