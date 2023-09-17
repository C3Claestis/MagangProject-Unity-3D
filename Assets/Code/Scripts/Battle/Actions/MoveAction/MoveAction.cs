namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;

    public class MoveAction : BaseAction
    {
        protected override string actionName { get { return "Move"; } }
        protected override ActionType actionType {get {return ActionType.Move;}}
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 15f;
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private Animator unitAnimator;

        private float moveStoppingDistance = .1f;
        private int currentPositionIndex;
        private List<Vector3> positionList;
        [SerializeField] private MoveType moveType; // ! Temporary [serializefield]

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
            List<GridPosition> pathGridPositionList = Pathfinding.Instance.FindPath(unit.GetGridPosition(), gridPosition, out int pathLength);
            positionList = new List<Vector3>();
            currentPositionIndex = 0;

            foreach (GridPosition pathGridPosition in pathGridPositionList)
            {
                positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
            }

            base.TakeAction(gridPosition, onActionComplete);
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            MoveLibrary moveLibrary = new MoveLibrary(unit, maxMoveDistance);

            switch (moveType)
            {
                case MoveType.Normal:
                    validGridPositionList = moveLibrary.NormalMoveValidGrids();
                    break;

                case MoveType.King:
                    validGridPositionList = moveLibrary.KingMoveValidGrids();
                    break;

                case MoveType.Tiger:
                    validGridPositionList = moveLibrary.TigerMoveValidGrids();
                    break;

                case MoveType.Bull:
                    validGridPositionList = moveLibrary.BullMoveValidGrids();
                    break;

                case MoveType.Snake:
                    validGridPositionList = moveLibrary.SnakeMoveValidGrids();
                    break;

                default:
                    validGridPositionList = null;
                    break;
            }

            return validGridPositionList;
        }

        /// <summary>Moves unit towards a target position.</summary>
        /// <remarks> Adjusting its position, orientation, and animation. </remarks>
        private void HandleMoving()
        {
            Vector3 targetPosition = positionList[currentPositionIndex];
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            if (Vector3.Distance(transform.position, targetPosition) > moveStoppingDistance)
            {
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

                transform.position += moveDirection * Time.deltaTime * moveSpeed;

                unitAnimator.SetBool("isWalking", true);
            }
            else
            {
                currentPositionIndex++;

                if (currentPositionIndex >= positionList.Count)
                {
                    SetActive(false);
                    unitAnimator.SetBool("isWalking", false);
                    GridSystemVisual.Instance.HideAllGridPosition();
                    unit.GetRotateAction().StartRotating(unit, onActionComplete);
                }
            }

        }

        public void SetMoveType(MoveType moveType) => this.moveType = moveType;
    }
}