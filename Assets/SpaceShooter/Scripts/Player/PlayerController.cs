using System;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private Rigidbody2D myRigidbody2D;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            // MoveShipWithKeyboard();
            MoveShipWithTouch();
        }

        private void MoveShipWithTouch()
        {
            var touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(touchPosition.x, touchPosition.y + 0.3f, transform.position.z);
        }

        private void MoveShipWithKeyboard()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (direction != Vector2.zero)
            {
                myRigidbody2D.velocity = new Vector2(direction.x * playerProperties.playerMoveSpeed,
                    direction.y * playerProperties.playerMoveSpeed);
            }
            else
            {
                myRigidbody2D.velocity = Vector2.zero;
            }
        }
    }
}