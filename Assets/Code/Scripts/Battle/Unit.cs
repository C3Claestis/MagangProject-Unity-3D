using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private string characterName = "Base Unit";
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

    private GridPosition gridPosition;
    private MoveAction moveAction;

    private void Awake() {
        moveAction = GetComponent<MoveAction>();            
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
        
        ChangeUnitShade();
    }
    
    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition){ // if unit change grid position
            LevelGrid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

   

    /// <summary>
    /// Change unit material shading based on the selection status.
    /// </summary>
    public void ChangeUnitShade(){
        Material newMaterial = skinnedMeshRenderer.material;
        newMaterial.color = isSelected ?  new Color(0.9f, 0.9f, 0.9f, 1f) :  new Color(0.4f, 0.4f, 0.4f, 1f);
        skinnedMeshRenderer.material = newMaterial;
    }

    public int GetAgility() => currentAgility;
    public bool GetMoveStatus() => hasMove;
    public string GetCharacterName() => characterName;
    public MoveAction GetMoveAction() => moveAction;

    public void SetMoveStatus(bool hasMove) => this.hasMove = hasMove;
    public void SetSelectedStatus(bool isSelected) => this.isSelected = isSelected;

    
}
