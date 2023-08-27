using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor.Search;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private Animator unitAnimator;
        
    [SerializeField] private int maxMoveDistance = 4;

    private float stoppingDistance = .1f;
    private Vector3 targetPosition;
    private Unit unit;

    private void Awake() {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();            
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
    public void MoveTo(GridPosition gridPosition) {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }
    
    public List<GridPosition> GetValidActionGridPosition(){
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++){
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++){
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (unitGridPosition == testGridPosition) continue; 
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition){
        List<GridPosition> validGridPositionList = GetValidActionGridPosition();
        return validGridPositionList.Contains(gridPosition);
    }
}
