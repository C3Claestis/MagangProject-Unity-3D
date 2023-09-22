namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;

    public class PlayerMovePuzzle : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 4f;
        [SerializeField] float runSpeed = 8f;
        [SerializeField] float rotateSpeed = 30f;        
        private PuzzleInputSystem inputSystem;
        private Rigidbody rb;
        //private bool onGround;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputSystem = GetComponent<PuzzleInputSystem>();
        }

        private void FixedUpdate()
        {
            Move();            
        }

        private void Move()
        {
            Vector2 input = inputSystem.GetMovementValue();
            Vector3 moveDirection = new Vector3(input.x, 0, input.y);
            float moveSpeed = inputSystem.CanRunning() ? runSpeed : walkSpeed;

            if (input == Vector2.zero)
            {
                rb.velocity = Vector3.zero;
                return;
            }

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
                rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotateSpeed * Time.deltaTime));
            }

            Vector3 velocity = moveDirection * moveSpeed;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
    }
}
