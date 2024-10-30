using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private Rigidbody2D myRigidbody2D;
        [SerializeField] private Transform playerShipModel;
        [SerializeField] private Transform leftLaserSpawnPosition;
        [SerializeField] private Transform rightLaserSpawnPosition;


        private Camera mainCamera;

        private Vector3 topRightBorder;
        private Vector3 bottomLeftBorder;

        // for movement
        private Vector3 currentTouchPosition;
        private Vector3 previousTouchPosition;

        public void Initialize()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                topRightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));
                bottomLeftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
            }
        }

        private void Update()
        {
            MoveShip();
        }

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
                var targetPosition = transform.position + direction * (playerProperties.playerMouseMoveSpeed * Time.deltaTime);
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
                    var targetPosition = transform.position + direction * (Time.deltaTime * playerProperties.playerTouchMoveSpeed);
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
            var targetPosition = transform.position + direction * (Time.deltaTime * playerProperties.playerKeyboardMoveSpeed);
            targetPosition = ClampShipToScreen(targetPosition);
            transform.position = targetPosition;
        }

        private Vector3 ClampShipToScreen(Vector3 position)
        {
            var clampedX = Mathf.Clamp(position.x, bottomLeftBorder.x + playerShipModel.localScale.x / 2, topRightBorder.x - playerShipModel.localScale.x / 2);
            var clampedY = Mathf.Clamp(position.y, bottomLeftBorder.y + playerShipModel.localScale.y / 2, topRightBorder.y - playerShipModel.localScale.y / 2);
            return new Vector3(clampedX, clampedY, 0f);
        }
    }
}