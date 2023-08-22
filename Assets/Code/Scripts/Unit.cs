using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private int baseHealth=100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int basePhysicalAttack=10;
    [SerializeField] private int currentPhysicalAttack;
    [SerializeField] private float baseMagicalAttack=10;
    [SerializeField] private float currentMagicalAttack;
    [SerializeField] private float basePhysicalDefense=10;
    [SerializeField] private float currentPhysicalDefense;
    [SerializeField] private float baseMagicalDefense=0;
    [SerializeField] private float currentMagicalDefense=0;
    [SerializeField] private int baseAgility = 0;
    [SerializeField] private int currentAgility = 0;
    [SerializeField] private bool alreadyMove = false;   

    private float stoppingDistance = .1f;
    private Vector3 targetPosition;

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Start() {
        currentHealth = baseHealth;
        currentPhysicalAttack = basePhysicalAttack;
        currentMagicalAttack = baseMagicalAttack;
        currentPhysicalDefense = basePhysicalDefense;
        currentMagicalDefense = baseMagicalDefense;
        currentAgility = baseAgility;
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

    public int GetAgi() => currentAgility;
    public bool GetMoveStatus() => alreadyMove;
}
