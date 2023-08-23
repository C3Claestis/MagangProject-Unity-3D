using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody playerRigidbody;
    [SerializeField] Animator animator;    
    bool _iswalk = false;    
    private PlayerInputActions inputActions;
    private bool isGrounded;
    private void Awake()
    {        
        playerRigidbody = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Interaksi.performed += Interaksi;
    }    

    private void Update()
    {
        if(!_iswalk)
            Move();

        // Mengecek apakah karakter di tanah sebelum mengatur animasi lompat atau idle
        isGrounded = CheckGrounded();
        
        if (playerRigidbody.velocity.y != 0)
        {
            //Animasi Lompat
        }
        else 
        {
            //Animasi Idle
        }
    }
    private bool CheckGrounded()
    {
        RaycastHit hit;
        float rayDistance = 0.1f; // Ubah ini sesuai dengan ukuran karakter
        Vector3 rayOrigin = transform.position + Vector3.up * rayDistance * 0.5f;

        // Raycast ke bawah untuk mendeteksi permukaan di bawah karakter
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
        {
            // Jika permukaan yang terdeteksi ada, karakter dianggap berada di tanah
            return true;
        }

        // Tidak ada permukaan yang terdeteksi di bawah karakter
        return false;
    }
    private void Move()
    {
        //Mengambil Input System
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(inputVector.x, 0f, inputVector.y) * moveSpeed * Time.deltaTime;
        movement.Normalize();

        //Untuk bergerak
        Vector3 targetPosition = transform.position + movement;
        playerRigidbody.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f));

        //Agar karakter tidak berotasi seperti awal
        if (inputVector.x != 0 || inputVector.y != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        //Animasi
        if (inputVector.x == 0 && inputVector.y == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {        
        if(context.performed)
        {
            if (isGrounded)
            {
                playerRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }            
    }

    private void Interaksi(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interaksi");
        }
    }
}
