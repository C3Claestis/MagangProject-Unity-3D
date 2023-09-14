namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Unity.VisualScripting;
    using UnityEngine;

    public class RotateAction : BaseAction
    {
        [SerializeField] Transform rotateVisual;
        protected override string actionName { get { return "Rotate"; } }
        private float smooth = 5.0f;
        private float rotationTolerance = 5f;
        private FacingDirection currentDirection = FacingDirection.UP;
        private Quaternion target;
        private bool doneRotating;


        private void Update()
        {
            if (!isActive) return;
            HandleRotate();

            if (Quaternion.Angle(transform.rotation, target) < rotationTolerance)
            {
                doneRotating = true;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                doneRotating = false;
            }


        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();
            return new List<GridPosition> { unitGridPosition };
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            base.TakeAction(gridPosition, onActionComplete);
            currentDirection = unit.GetFacingDirection();
            rotateVisual.gameObject.SetActive(true);
            RotateCharacter(currentDirection);
            SetRotateVisualActive(true);
        }

        private void HandleRotate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Rotate the character counterclockwise
                currentDirection = (FacingDirection)(((int)currentDirection + 1) % 4);
                RotateCharacter(currentDirection);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // Rotate the character clockwise
                currentDirection = (FacingDirection)(((int)currentDirection + 3) % 4);
                RotateCharacter(currentDirection);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                if (!doneRotating) return;
                unit.CalculateUnitDirection();
                SetRotateVisualActive(false);
                onActionComplete();
            }

        }

        private void RotateCharacter(FacingDirection state)
        {
            switch (state)
            {
                case FacingDirection.UP:
                    target = Quaternion.Euler(0, 0, 0);
                    break;
                case FacingDirection.RIGHT:
                    target = Quaternion.Euler(0, 90, 0);
                    break;
                case FacingDirection.DOWN:
                    target = Quaternion.Euler(0, 180, 0);
                    break;
                case FacingDirection.LEFT:
                    target = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }

        private void SetRotateVisualActive(bool status) => rotateVisual.gameObject.SetActive(status);
    }

}