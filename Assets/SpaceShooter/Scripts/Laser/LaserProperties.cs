using UnityEngine;

namespace SpaceShooter.Lasers
{
    [CreateAssetMenu(fileName = "Laser 1", menuName = "SpaceShooter/Laser/Create New Laser Properties")]
    public class LaserProperties : ScriptableObject
    {
        public Laser laserPrefab;
        public Vector2 speed = new Vector2(0, 8f);
        public float fireInterval = 0.2f;
        public int laserDamage = 1;
    }
}