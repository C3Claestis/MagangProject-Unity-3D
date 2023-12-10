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
        [SerializeField] private Material arrowMaterial;

        private Vector3 startPosition;
        private Quaternion startRotation;
        private Quaternion targetRotation;
        private GridPosition targetPosition;

        private List<Vector3> positionList;
        private int currentPositionIndex;
        private float moveStoppingDistance = 0.1f;
        private float moveSpeed = 4f;

        private float jumpHeight = 4f;
        private float interpolateAmount;
        private Vector3 middlePoint;
        private float middlePointPercentage = 0.5f;

        private Vector3 jumpTargetPosition;
        private bool startJumping = false;
        private bool isJumping = false;

        private bool destinationReached;

        private bool doneRotating = true;
        private float rotateSpeed = 30f;

        private bool generatePathfindingPath = false;
        private GridPosition pathTarget;
        List<GridPosition> pathToTarget;
        List<GridPosition> currentValidGrid;
        LineRenderer lineRenderer;

        private void Update()
        {
            if (generatePathfindingPath) ShowPath();
            if (!doneRotating) IsRotating();
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
                JumpingInitialization();
                return;
            }

            generatePathfindingPath = false;
            WalkingInitialization();
        }

        private void JumpingInitialization()
        {
            jumpTargetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
            isJumping = true;
            startJumping = true;

            interpolateAmount = 0;
            middlePoint = Vector3.Lerp(startPosition, jumpTargetPosition, middlePointPercentage);
            middlePoint += Vector3.up * jumpHeight;

            Vector3 directionToTarget = (jumpTargetPosition - transform.position).normalized;
            transform.forward = directionToTarget;
        }


        private void WalkingInitialization()
        {
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
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            MoveType moveType = unit.GetMoveType();

            Pathfinding.Instance.SetupPath(unit.GetUnitType());

            switch (moveType)
            {
                case MoveType.Normal:
                    validGridPositionList = MovePatern.NormalMoveValidGrids(unit, maxMoveDistance);
                    break;

                case MoveType.King:
                    validGridPositionList = MovePatern.KingMoveValidGrids(unit);
                    break;

                case MoveType.Tiger:
                    validGridPositionList = MovePatern.TigerMoveValidGrids(unit, obstacleLayer);
                    break;

                case MoveType.Bull:
                    validGridPositionList = MovePatern.BullMoveValidGrids(unit);
                    break;

                case MoveType.Snake:
                    validGridPositionList = MovePatern.SnakeMoveValidGrids(unit);
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

            if (Pathfinding.Instance.IsObstacleOnGrid(targetPosition, out Transform objectTransform) && objectTransform.CompareTag("Tier2_Obstacles"))
            {
                if (!objectTransform.GetComponent<Obstacle>().IsBroken()) targetPosition.y = 0.75f;
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
            float jumpSpeed = 0.9f;

            if (startJumping)
            {
                OnJump?.Invoke(this, EventArgs.Empty);
                startJumping = false;
            }

            if (Vector3.Distance(transform.position, jumpTargetPosition) > moveStoppingDistance)
            {
                interpolateAmount += Time.deltaTime * jumpSpeed;

                Vector3 pointAB = Vector3.Lerp(startPosition, middlePoint, interpolateAmount);
                Vector3 pointBC = Vector3.Lerp(middlePoint, jumpTargetPosition, interpolateAmount);
                transform.position = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
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

            if (!unit.IsEnemy()) UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
            else YesButtonAction();

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
                case FacingDirection.NORTH:
                    targetRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case FacingDirection.EAST:
                    targetRotation = Quaternion.Euler(0, 90, 0);
                    break;
                case FacingDirection.SOUTH:
                    targetRotation = Quaternion.Euler(0, 180, 0);
                    break;
                case FacingDirection.WEST:
                    targetRotation = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }

        protected override void NoButtonAction()
        {
            generatePathfindingPath = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            destinationReached = false;
            doneRotating = true;

            unit.UpdateUnitGridPosition();
            interpolateAmount = 0;

            base.NoButtonAction();
        }

        protected override void YesButtonAction()
        {
            base.YesButtonAction();
            destinationReached = false;
            GridSystemVisual.Instance.HideAllGridPosition();
            HidePath();
            onActionComplete();
        }

        protected override void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            if (destinationReached) return;
            generatePathfindingPath = false;
            HidePath();
            base.PlayerInputController_OnCancelPressed(sender, e);
        }

        public void InitPathToTarget()
        {
            if (unit.GetMoveType() == MoveType.Tiger) return;
            generatePathfindingPath = true;
            pathToTarget = null;
            currentValidGrid = GetValidActionGridPosition();

            if (lineRenderer != null) Destroy(lineRenderer);
            lineRenderer = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            lineRenderer.positionCount = 0;
            lineRenderer.material = arrowMaterial;
            lineRenderer.generateLightingData = true;
            lineRenderer.useWorldSpace = true;
            lineRenderer.numCornerVertices = 5;
            lineRenderer.textureScale = new Vector2(1, 0.3f);
            lineRenderer.sortingLayerName = "Below";
        }

        private void ShowPath()
        {
            GridPosition newPathTarget = Pointer.Instance.GetCurrentGrid();

            if (newPathTarget == pathTarget || !currentValidGrid.Contains(newPathTarget)) return;
            if (pathToTarget != null) GridSystemVisual.Instance.ShowGridPositionList(pathToTarget, GridVisualType.White);

            pathToTarget = Pathfinding.Instance.FindPath(unit.GetGridPosition(), newPathTarget, out int pathLength);
            lineRenderer.positionCount = pathToTarget.Count;
            for (int i = 0; i < pathToTarget.Count; i++)
            {
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(pathToTarget[i]);
                worldPosition.y = 0.3f;
                lineRenderer.SetPosition(i, worldPosition);
            }

            pathTarget = newPathTarget;
        }

        public void HidePath()
        {
            Destroy(lineRenderer);
        }

        public Material GetArrowMaterial() => arrowMaterial;

    }
}