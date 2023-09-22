namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UnitSystem;

    public class MoveAction : BaseAction
    {
        public event EventHandler OnStartMoving;
        public event EventHandler OnJump;
        public event EventHandler OnStopMoving;

        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private LayerMask obstacleLayer;

        private Vector3 startPosition;
        private Quaternion startRotation;
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

        protected override ActionType actionType { get { return ActionType.Move; } }
        protected override string actionName { get { return "Move"; } }

        private void Update()
        {
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
            float jumpSpeed = 3.8f;

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
            Pointer.Instance.SetPointerOnGrid(targetPosition);

            InitializeConfirmationButton(YesButtonAction, NoButtonAction);
        }

        protected override void NoButtonAction()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            destinationReached = false;

            unit.UpdateUnitGridPosition();
            Pointer.Instance.SetPointerOnGrid(LevelGrid.Instance.GetGridPosition(startPosition));

            base.NoButtonAction();
        }

        protected override void YesButtonAction()
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            unit.GetRotateAction().StartRotating(unit, onActionComplete);
            PlayerInputController.Instance.SetActionMap("RotateUnit");
        }

        protected override void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            if(destinationReached) return;
            base.PlayerInputController_OnCancelPressed(sender, e);
        }

    }
}