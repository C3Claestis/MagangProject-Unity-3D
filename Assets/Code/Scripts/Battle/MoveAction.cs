using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private Animator unitAnimator;
        
    [SerializeField] private int maxMoveDistance = 4;

    private float stoppingDistance = .1f;
    private bool justStopMoving = false;
    private Vector3 targetPosition;
    private Unit unit;

    private void Awake() {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();            
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance){
            HandleMoving();
            justStopMoving = true;
        }
        else if(justStopMoving){
            unitAnimator.SetBool("isWalking", false);
        }
    }

    /// <summary>Moves unit towards a target position. 
    /// </summary>
    /// <remarks> Adjusting its position, orientation, and animation. </remarks>
    private void HandleMoving(){
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        unitAnimator.SetBool("isWalking",true);
    }

    /// <summary>Set the target position for movement.
    /// </summary>
    /// <param name="targetPosition">The target position to move to.</param>
    public void MoveTo(GridPosition gridPosition) {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }
    
    /// <summary>Retrieves a list of valid grid positions that the unit can move to.
    /// </summary>
    /// <returns>A list of valid grid positions for the unit's movement.</returns>
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

    /// <summary>Checks whether the given grid position is a valid action grid position for the unit.
    /// </summary>
    /// <param name="gridPosition">The grid position to be checked.</param>
    /// <returns>True if the grid position is a valid action grid position, otherwise false.</returns>
    public bool IsValidActionGridPosition(GridPosition gridPosition){
        List<GridPosition> validGridPositionList = GetValidActionGridPosition();
        return validGridPositionList.Contains(gridPosition);
    }
}
