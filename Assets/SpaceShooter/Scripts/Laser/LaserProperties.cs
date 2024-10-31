using UnityEngine;

namespace SpaceShooter.Lasers
{
    [CreateAssetMenu(fileName = "Laser Properties", menuName = "SpaceShooter/Laser/Create New Laser Properties")]
    public class LaserProperties : ScriptableObject
    {
        public Vector2 speed = new Vector2(0, 8f);
        public float fireInterval = 0.2f;
    }
}