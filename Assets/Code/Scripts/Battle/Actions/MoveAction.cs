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
        private MoveType moveType;

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

        public override List<GridPosition> GetRangeActionGridPosition()
        {
            List<GridPosition> rangeActionGridPosition = new List<GridPosition>();
            MoveLibrary moveLibrary = new MoveLibrary(unit, maxMoveDistance);

            switch (moveType)
            {
                case MoveType.Normal:
                    rangeActionGridPosition = moveLibrary.NormalMoveRangeGrids();
                    break;

                case MoveType.King:
                    rangeActionGridPosition = moveLibrary.KingMoveRangeGrids();
                    break;

                case MoveType.Tiger:
                    rangeActionGridPosition = moveLibrary.TigerMoveRangeGrids();
                    break;

                case MoveType.Bull:
                    rangeActionGridPosition = moveLibrary.BullMoveRangeGrids();
                    break;

                case MoveType.Snake:
                    rangeActionGridPosition = moveLibrary.SnakeMoveRangeGrids();
                    break;

                default:
                    rangeActionGridPosition = null;
                    break;
            }

            return rangeActionGridPosition;
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

        public void SetMoveType(MoveType moveType) => this.moveType = moveType;
    }
}