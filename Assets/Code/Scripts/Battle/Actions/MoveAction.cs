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

            switch (moveType)
            {
                case MoveType.Normal:
                    validGridPositionList = NormalMoveGridPositions();
                    break;

                case MoveType.King:
                    validGridPositionList = KingMoveGridPositions();
                    break;

                case MoveType.Tiger:
                    validGridPositionList = TigerMoveGridPositions();
                    break;

                case MoveType.Bull:
                    validGridPositionList = BullMoveGridPositions();
                    break;

                default:
                    validGridPositionList = null;
                    break;
            }

            return validGridPositionList;
        }

        private List<GridPosition> NormalMoveGridPositions()
        {
            List<GridPosition> normalMoveList = new List<GridPosition>();
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

                    normalMoveList.Add(testGridPosition);
                }
            }

            return normalMoveList;
        }

        private List<GridPosition> KingMoveGridPositions()
        {
            List<GridPosition> kingMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int maxMoveDistance = 1;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                    kingMoveList.Add(testGridPosition);
                }
            }

            return kingMoveList;
        }

        private List<GridPosition> TigerMoveGridPositions()
        {
            List<GridPosition> tigerMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            int[][] tigerMoves = new int[][]
            {
                new int[] { 2, 1 },
                new int[] { 2, -1 },
                new int[] { -2, 1 },
                new int[] { -2, -1 },
                new int[] { 1, 2 },
                new int[] { 1, -2 },
                new int[] { -1, 2 },
                new int[] { -1, -2 }
            };

            foreach (var move in tigerMoves)
            {
                int xOffset = move[0];
                int zOffset = move[1];
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z + zOffset);

                if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && !LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    tigerMoveList.Add(testGridPosition);
                }
            }

            return tigerMoveList;
        }

        private List<GridPosition> BullMoveGridPositions()
        {
            List<GridPosition> towerMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            // Horizontal movement to the right
            for (int xOffset = 1; xOffset < LevelGrid.Instance.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                towerMoveList.Add(testGridPosition);
            }

            // Horizontal movement to the left
            for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                towerMoveList.Add(testGridPosition);
            }

            // Vertical movement upwards
            for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                towerMoveList.Add(testGridPosition);
            }

            // Vertical movement downwards
            for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                towerMoveList.Add(testGridPosition);
            }

            return towerMoveList;
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