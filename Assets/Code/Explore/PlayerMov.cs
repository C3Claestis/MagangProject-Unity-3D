using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rotateSpeed = 30f;
    
    private InputSystem inputSystem;    
    
    private bool onGround;
    
    private void Awake()
    {        
        inputSystem = GetComponent<InputSystem>();
    }    

    private void Update()
    {
        if(onGround){
            Move();
        }
    }

    //Akan terpanggil otomatis jika player bersentuhan dengan Ground
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer != 3) return;

        onGround = true;
        Debug.Log("Player is on the ground");
    }

    //Akan terpanggil otomatis jika player tidak bersentuhan dengan ground
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.layer != 3) return;
        
        onGround = false;   
        Debug.Log("Player exit the ground");    
    }
    
    private void Move()
    {
        Vector2 input = inputSystem.GetMovementValue();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        float moveSpeed = walkSpeed;

        if (input == Vector2.zero){
            return;
        }

        if(moveDirection != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }


        if(inputSystem.CanRunning()){
            moveSpeed = runSpeed;
        }

        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }

}
