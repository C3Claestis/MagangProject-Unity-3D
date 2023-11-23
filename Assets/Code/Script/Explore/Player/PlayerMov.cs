namespace Nivandria.Explore
{
    using UnityEngine;

    /// <summary>
    /// Controls the movement of the player character, including walking and running.
    /// </summary>
    public class PlayerMov : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 4f; // Walking speed
        [SerializeField] float runSpeed = 8f; // Running speed
        [SerializeField] float rotateSpeed = 30f; // Rotation speed
        [SerializeField] InteraksiNPC interaksiNPC; // Reference to the NPC interaction system
        private InputSystem inputSystem; // Input system for controlling movement
        private Rigidbody rb; // Reference to the Rigidbody component

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputSystem = GetComponent<InputSystem>();
        }
        
        private void FixedUpdate()
        {
            if (interaksiNPC.GetIsTalk() == false)
            {
                Move();
            }
        }
        private void Move()
        {
            Vector2 input = inputSystem.GetMovementValue();

            // Dapatkan rotasi kamera saat ini
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Ubah input pemain menjadi arah dunia menggunakan rotasi kamera
            Vector3 moveDirection = cameraRotation * new Vector3(input.x, 0, input.y);
            moveDirection.y = 0; // Pastikan tidak ada perubahan di sumbu y

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