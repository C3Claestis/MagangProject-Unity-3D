namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.UnitSystem;

    public class MoveAction : BaseAction
    {
        public event EventHandler OnStartMoving;
        public event EventHandler OnJump;
        public event EventHandler OnStopMoving;

        protected override string actionName => "Move";
        protected override ActionCategory actionCategory => ActionCategory.Move;
        protected override ActionType actionType => ActionType.NULL;
        protected override string actionDescription =>
        "Lets a unit relocate to a chosen grid, enhancing tactical positioning and adaptability in combat.";

        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private LayerMask obstacleLayer;

        private Vector3 startPosition;
        private Quaternion startRotation;
        private Quaternion targetRotation;

        private GridPosition targetPosition;

        private List<Vector3> positionList;
        private int currentPositionIndex;
        private float moveStoppingDistance = 0.1f;
        private float rotateSpeed = 20f;
        private float moveSpeed = 4f;

        private bool destinationReached;

        private Vector3 jumpTargetPosition;
        private bool startJumping = false;
        private bool isJumping = false;
        private bool doneRotating;

        private void Update()
        {
            if (!doneRotating)
            {
                IsRotating();
            }

            if (!isActive) return;

            if (isJumping) HandleJumping();
            else HandleMoving();

        }

        /// <summary>Initiates a move action to the specified grid position and triggers a callback when the movement is finished.</summary>
        /// <param name="gridPosition">The target grid position to move to.</param>
        /// <param name="onActionComplete">Callback function to call upon completing the move action.</param>
        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            targetPosition = gridPosition;
            doneRotating = true;

            base.TakeAction(targetPosition, onActionComplete);

            if (unit.GetMoveType() == MoveType.Tiger)
            {
                jumpTargetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
                isJumping = true;
                startJumping = true;
                return;
            }

            List<GridPosition> pathGridPositionList = Pathfinding.Instance.FindPath(unit.GetGridPosition(), targetPosition, out int pathLength);
            positionList = new List<Vector3>();
            currentPositionIndex = 0;

            foreach (GridPosition pathGridPosition in pathGridPositionList)
            {
                positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
            }

        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            MoveType moveType = unit.GetMoveType();
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
                    validGridPositionList = moveLibrary.TigerMoveValidGrids(obstacleLayer);
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

            if (Pathfinding.Instance.IsObstacleOnGrid(targetPosition, out string objectTag) && objectTag == "Obstacle")
            {
                targetPosition.y = 0.75f;
            }

            Vector3 moveDirection = (targetPosition - transform.position).normalized;


            if (Vector3.Distance(transform.position, targetPosition) > moveStoppingDistance)
            {
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

                transform.position += moveDirection * Time.deltaTime * moveSpeed;

                OnStartMoving?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                currentPositionIndex++;

                if (currentPositionIndex >= positionList.Count)
                {
                    OnStopMoving?.Invoke(this, EventArgs.Empty);
                    DoneMoving();
                }
            }

        }

        private void HandleJumping()
        {
            float jumpSpeed = 3.7f;

            if (startJumping)
            {
                OnJump?.Invoke(this, EventArgs.Empty);
                startJumping = false;
            }

            if (Vector3.Distance(transform.position, jumpTargetPosition) > moveStoppingDistance)
            {
                Vector3 moveDirection = (jumpTargetPosition - transform.position).normalized;
                transform.position += moveDirection * Time.deltaTime * jumpSpeed;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 50f);
            }
            else
            {
                DoneMoving();
            }
        }


        private void DoneMoving()
        {
            destinationReached = true;
            isJumping = false;
            SetActive(false);

            unit.UpdateUnitGridPosition();
            unit.UpdateUnitDirection();
            RotateCharacter(unit.GetFacingDirection());
            Pointer.Instance.SetPointerOnGrid(targetPosition);

            UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
            doneRotating = false;
        }

        private void IsRotating()
        {

            if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
            {
                doneRotating = true;
                unit.UpdateUnitDirection();
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                doneRotating = false;
            }
        }

        private void RotateCharacter(FacingDirection state)
        {
            switch (state)
            {
                case FacingDirection.UP:
                    targetRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case FacingDirection.RIGHT:
                    targetRotation = Quaternion.Euler(0, 90, 0);
                    break;
                case FacingDirection.DOWN:
                    targetRotation = Quaternion.Euler(0, 180, 0);
                    break;
                case FacingDirection.LEFT:
                    targetRotation = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }

        protected override void NoButtonAction()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            destinationReached = false;
            doneRotating = true;

            unit.UpdateUnitGridPosition();

            base.NoButtonAction();
        }

        protected override void YesButtonAction()
        {
            base.YesButtonAction();
            GridSystemVisual.Instance.HideAllGridPosition();

            onActionComplete();
        }

        protected override void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            if (destinationReached) return;
            base.PlayerInputController_OnCancelPressed(sender, e);
        }

    }
}