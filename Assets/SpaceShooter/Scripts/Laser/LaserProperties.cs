using UnityEngine;

namespace SpaceShooter.Lasers
{
    [CreateAssetMenu(fileName = "Laser 1", menuName = "SpaceShooter/Laser/Create New Laser Properties")]
    public class LaserProperties : ScriptableObject
    {
        [SerializeField] private Laser laserPrefab;
        [SerializeField] private Vector2 laserSpeed = new Vector2(0, 8f);
        [SerializeField] private float fireInterval = 0.2f;
        [SerializeField] private int laserDamage = 1;


        public Laser LaserPrefab => laserPrefab;
        public Vector2 LaserSpeed => laserSpeed;
        public float FireInterval => fireInterval;
        public int LaserDamage => laserDamage;
    }
}