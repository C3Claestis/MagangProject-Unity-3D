using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private Animator unitAnimator;

    private float stoppingDistance = .1f;
    private Vector3 targetPosition;

    private void Awake() {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance){
            Moving();
        }
        else{
            unitAnimator.SetBool("isWalking", false);
        }
    }

    /// <summary>
    /// Moves the object towards a target position. 
    /// </summary>
    /// <remarks> Adjusting its position, orientation, and animation. </remarks>
    private void Moving(){
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        unitAnimator.SetBool("isWalking",true);
    }

    /// <summary>
    /// Set the target position for movement.
    /// </summary>
    /// <param name="targetPosition">The target position to move to.</param>
    public void MoveTo(Vector3 targetPosition) => this.targetPosition = targetPosition; 
}
