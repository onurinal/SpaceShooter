using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private Rigidbody2D myRigidbody2D;
        [SerializeField] private Transform playerShipModel;

        private Camera mainCamera;

        // to not go offscreen
        private Vector3 topRightBorder;
        private Vector3 bottomLeftBorder;
        private float minMaxMovementX;
        private float minMaxMovementY;

        // FOR MOVEMENT
        private Vector3 previousTouchPosition;
        private Vector3 currentTouchPosition;
        private Vector3 direction;
        private Vector3 targetPosition;

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
            MoveShipWithTouch();
        }

        private void FixedUpdate()
        {
            MoveShipWithKeyboard();
        }

        private void MoveShipWithTouch()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                previousTouchPosition = currentTouchPosition;
                currentTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                currentTouchPosition.z = 0f;
                direction = currentTouchPosition - previousTouchPosition;
                direction.Normalize();
                targetPosition = transform.position + direction;
                targetPosition = ClampShipToScreen(targetPosition);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerProperties.playerTouchMoveSpeed);
            }

#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                if (touch.phase == TouchPhase.Moved)
                {
                    previousTouchPosition = currentTouchPosition;
                    currentTouchPosition = touchPosition;
                    direction = currentTouchPosition - previousTouchPosition;
                    direction.Normalize();
                    targetPosition = transform.position + direction;
                    targetPosition = ClampShipToScreen(targetPosition);
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerProperties.playerTouchMoveSpeed);
                }
            }
#endif
        }

        private void MoveShipWithKeyboard()
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            if (direction != Vector3.zero)
            {
                myRigidbody2D.velocity = direction * playerProperties.playerKeyboardMoveSpeed;
                transform.position = ClampShipToScreen(transform.position);
            }
            else
            {
                myRigidbody2D.velocity = Vector3.zero;
            }
        }

        private Vector3 ClampShipToScreen(Vector3 position)
        {
            minMaxMovementX = Mathf.Clamp(position.x, bottomLeftBorder.x + playerShipModel.localScale.x / 2, topRightBorder.x - playerShipModel.localScale.x / 2);
            minMaxMovementY = Mathf.Clamp(position.y, bottomLeftBorder.y + playerShipModel.localScale.y / 2, topRightBorder.y - playerShipModel.localScale.y / 2);
            return new Vector3(minMaxMovementX, minMaxMovementY, 0f);
        }
    }
}