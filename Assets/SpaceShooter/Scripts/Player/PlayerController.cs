using SpaceShooter.Manager;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private Rigidbody2D playerRigidbody2D;
        [SerializeField] private Transform playerShipModel;

        [SerializeField] private Transform leftLaserSpawnTransform, rightLaserSpawnTransform;

        private Camera mainCamera;
        private Vector3 topRightBorder, bottomLeftBorder;

        // for movement 
        private Vector3 currentTouchPosition, previousTouchPosition;

        public void Initialize()
        {
            SetUpMovementBoundaries();
            InitializeLaser();
        }

        private void Update()
        {
            MoveShip();
        }

        // -------------------- MOVEMENTS -------------------
        private void MoveShip()
        {
            // use touch or mouse movement if no keyboard input detected
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                MoveShipWithTouch();
            }
            else
            {
                MoveShipWithKeyboard();
            }
        }

        private void MoveShipWithTouch()
        {
#if UNITY_EDITOR
            currentTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentTouchPosition.z = 0f;
            if (Input.GetMouseButton(0)) // Check if the left mouse button is held down
            {
                var direction = currentTouchPosition - previousTouchPosition;
                var targetPosition = transform.position + direction * (playerProperties.mouseMoveSpeed * Time.deltaTime);
                targetPosition = ClampShipToScreen(targetPosition);
                transform.position = targetPosition;
            }

            previousTouchPosition = currentTouchPosition;

#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                if (touch.phase == TouchPhase.Began)
                {
                    previousTouchPosition = touchPosition;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    var direction = touchPosition - previousTouchPosition;
                    var targetPosition = transform.position + direction * (Time.deltaTime * playerProperties.touchMoveSpeed);
                    targetPosition = ClampShipToScreen(targetPosition);
                    transform.position = targetPosition;
                    previousTouchPosition = touchPosition;
                }
            }
#endif
        }

        private void MoveShipWithKeyboard()
        {
            var direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
            var targetPosition = transform.position + direction * (Time.deltaTime * playerProperties.keyboardMoveSpeed);
            targetPosition = ClampShipToScreen(targetPosition);
            transform.position = targetPosition;
        }

        // -------------------- BOUNDARIES -------------------
        private void SetUpMovementBoundaries()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                topRightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));
                bottomLeftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
            }
        }

        private Vector3 ClampShipToScreen(Vector3 position)
        {
            var clampedX = Mathf.Clamp(position.x, bottomLeftBorder.x + playerShipModel.localScale.x / 2, topRightBorder.x - playerShipModel.localScale.x / 2);
            var clampedY = Mathf.Clamp(position.y, bottomLeftBorder.y + playerShipModel.localScale.y / 2, topRightBorder.y - playerShipModel.localScale.y / 2);
            return new Vector3(clampedX, clampedY, 0f);
        }

        // -------------------- SET UP LASER -------------------
        private void InitializeLaser()
        {
            LaserManager.Instance.Initialize(leftLaserSpawnTransform, rightLaserSpawnTransform);
            LaserManager.Instance.StartFireLaser();
        }
    }
}