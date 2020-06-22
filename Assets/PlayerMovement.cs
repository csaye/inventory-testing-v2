using UnityEngine;

namespace InventoryTesting.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        public float speed = 1;

        [Header("References")]
        public Rigidbody2D rb;
        public Animator animator;

        private Vector2 movementDirection;
        private float movementSpeed;

        // From variables to fix animation inconsistencies
        private bool fromX, fromY;

        void Update()
        {
            ProcessInputs();
            Move();
            Animate();
        }

        void ProcessInputs()
        {
            // Set the movement direction to the player input
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Normalize the movement direction
            movementDirection.Normalize();
        }

        void Move()
        {
            // Move the player by changing the velocity of the rigidbody
            rb.velocity = movementDirection * speed;
        }

        void Animate()
        {
            // Reset from paramaters when player not moving
            if (movementDirection.x == 0) fromX = false;
            if (movementDirection.y == 0) fromY = false;

            // Set animator values if player moving
            if (movementDirection != Vector2.zero) {

                // If moving horizontally and not previously moving vertically
                if (movementDirection.x != 0 && !fromY)
                {
                    fromX = true;
                    animator.SetFloat("Horizontal", movementDirection.x);
                }
                else
                {
                    animator.SetFloat("Horizontal", 0);
                }

                // If moving vertically and not previously moving horizontally
                if (movementDirection.y != 0 && !fromX)
                {
                    fromY = true;
                    animator.SetFloat("Vertical", movementDirection.y);
                }
                else
                {
                    animator.SetFloat("Vertical", 0);
                }

                // Set speed of animator to movement speed
                animator.SetFloat("Speed", 1);
            
            }
            else
            {
                // Set speed of animator to movement speed
                animator.SetFloat("Speed", 0);
            }
        }

    }
}
