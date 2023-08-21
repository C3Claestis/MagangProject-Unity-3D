using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;

    [SerializeField] private int defaultSpeed = 3;
    [SerializeField] private int currentSpeed;
     
    [SerializeField] private int CT;

    private float stoppingDistance = .1f;
    private Vector3 targetPosition;

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Start() {
        currentSpeed = defaultSpeed;
    }
    
    private void Update()
    {

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("isWalking",true);
        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
        }


    }
    
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; 
    }

    public int GetCT(){
        return CT;
    }
}
