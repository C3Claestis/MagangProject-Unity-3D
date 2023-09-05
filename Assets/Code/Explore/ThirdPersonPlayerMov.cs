using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerMov : MonoBehaviour
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] InteraksiNPC interaksiNPC;
    private InputSystem inputSystem;
    private Rigidbody rb;
    //private bool onGround;

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
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        float moveSpeed = inputSystem.CanRunning() ? runSpeed : walkSpeed;

        if (input == Vector2.zero)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        // Mengubah vektor gerakan ke ruang lokal kamera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 velocity = (cameraForward * moveDirection.z + cameraRight * moveDirection.x) * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Mengubah rotasi pemain berdasarkan arah pandangan kamera
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward.normalized, Vector3.up);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotateSpeed * Time.deltaTime));
        }
    }
}
