using UnityEngine;

namespace SpaceShooter.Manager
{
    public class SpaceManager : MonoBehaviour
    {
        [SerializeField] private Transform topCollider, bottomCollider, leftCollider, rightCollider;
        public Vector3 bottomLeftBorder, topRightBorder;
        private Camera mainCamera;

        public static SpaceManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            InitializeBorders();
            UpdateBorderColliders();
        }

        public void InitializeBorders()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                bottomLeftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
                topRightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            }
        }

        private void UpdateBorderColliders()
        {
            topCollider.position = new Vector3(topCollider.position.x, topRightBorder.y + topCollider.localScale.y, topCollider.position.z);
            bottomCollider.position = new Vector3(bottomCollider.position.x, bottomLeftBorder.y - bottomCollider.localScale.y, bottomCollider.position.z);
            leftCollider.position = new Vector3(bottomLeftBorder.x - leftCollider.localScale.x, leftCollider.position.y, leftCollider.position.z);
            rightCollider.position = new Vector3(topRightBorder.x + rightCollider.localScale.x, rightCollider.position.y, rightCollider.position.z);
        }
    }
}