namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Nivandria.Battle.Grid;

    public class MoveAction : BaseAction
    {
        protected override string actionName { get { return "Move"; } }
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 15f;
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private Animator unitAnimator;

        private float moveStoppingDistance = .1f;

        private Vector3 moveTargetPosition;

        protected override void Awake()
        {
            base.Awake();
            moveTargetPosition = transform.position;
            unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (!isActive) return;
            HandleMoving();
        }

        /// <summary>Initiates a move action to the specified grid position and triggers a callback when the movement is finished.</summary>
        /// <param name="gridPosition">The target grid position to move to.</param>
        /// <param name="onActionComplete">Callback function to call upon completing the move action.</param>
        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            this.onActionComplete = onActionComplete;
            isActive = true;
            this.moveTargetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                    validGridPositionList.Add(testGridPosition);
                }
            }
            return validGridPositionList;
        }

        /// <summary>Moves unit towards a target position.</summary>
        /// <remarks> Adjusting its position, orientation, and animation. </remarks>
        private void HandleMoving()
        {
            if (Vector3.Distance(transform.position, moveTargetPosition) > moveStoppingDistance)
            {
                Vector3 moveDirection = (moveTargetPosition - transform.position).normalized;
                transform.position += moveDirection * Time.deltaTime * moveSpeed;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

                unitAnimator.SetBool("isWalking", true);
            }
            else
            {
                unitAnimator.SetBool("isWalking", false);
                isActive = false;
                onActionComplete();
            }

        }
    }
}