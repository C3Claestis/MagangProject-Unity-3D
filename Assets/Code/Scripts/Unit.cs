using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private string characterName = "Base Unit";
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private int baseHealth=100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int basePhysicalAttack=12;
    [SerializeField] private int currentPhysicalAttack;
    [SerializeField] private float baseMagicalAttack=11;
    [SerializeField] private float currentMagicalAttack;
    [SerializeField] private float basePhysicalDefense=7;
    [SerializeField] private float currentPhysicalDefense;
    [SerializeField] private float baseMagicalDefense=6;
    [SerializeField] private float currentMagicalDefense;
    [SerializeField] private int baseAgility = 7;
    [SerializeField] private int currentAgility;
    [SerializeField] private bool hasMove = false;  
    [SerializeField] private bool isSelected = false;

    private float stoppingDistance = .1f;
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);


        currentHealth = baseHealth;
        currentPhysicalAttack = basePhysicalAttack;
        currentMagicalAttack = baseMagicalAttack;
        currentPhysicalDefense = basePhysicalDefense;
        currentMagicalDefense = baseMagicalDefense;
        currentAgility = baseAgility;
        
        ChangeMaterial();
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


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition){ // if unit change grid position
            LevelGrid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }
    
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; 
    }

    public void ChangeMaterial(){
        Material newMaterial = skinnedMeshRenderer.material;
        newMaterial.color = isSelected ?  new Color(0.9f, 0.9f, 0.9f, 1f) :  new Color(0.4f, 0.4f, 0.4f, 1f);
        skinnedMeshRenderer.material = newMaterial;
    }

    public int GetAgility() => currentAgility;
    public bool GetMoveStatus() => hasMove;
    public string GetCharacterName() => characterName;

    public void SetMoveStatus(bool hasMove) => this.hasMove = hasMove;
    public void SetSelectedStatus(bool isSelected) => this.isSelected = isSelected;
}
