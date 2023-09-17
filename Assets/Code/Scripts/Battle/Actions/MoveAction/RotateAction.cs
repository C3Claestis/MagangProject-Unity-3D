namespace Nivandria.Battle.Action
{
    using System;
    using UnityEngine;

    public class RotateAction : MonoBehaviour
    {
        [SerializeField] Transform rotateVisual;
        private FacingDirection currentDirection = FacingDirection.UP;
        private Action onActionComplete;
        private Quaternion target;
        private Unit unit;
        private float rotationTolerance = 5f;
        private float smooth = 5.0f;
        private bool doneRotating;
        private bool isActive;

        private void Update()
        {
            if(doneRotating && !isActive) return;
            IsRotating();

            if (!isActive) return;
            HandleRotate();
        }

        /// <summary>Initiates the rotation of the object.</summary>
        /// <param name="unit">The unit to be rotated.</param>
        /// <param name="onActionComplete">Callback action to be executed when the rotation is complete.</param>
        public void StartRotating(Unit unit, Action onActionComplete)
        {
            this.unit = unit;
            this.onActionComplete = onActionComplete;
            
            unit.UpdateUnitGridPosition();
            unit.UpdateUnitDirection();

            currentDirection = unit.GetFacingDirection();
            Pointer.Instance.SetPointerOnGrid(unit.GetGridPosition());

            RotateCharacter(currentDirection);
            SetRotateVisualActive(true);
            IsActive(true);
        }

        /// <summary>Handles the rotation of the character based on user input.</summary>
        private void HandleRotate()
        {
            // Rotate the character counterclockwise
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentDirection = (FacingDirection)(((int)currentDirection + 1) % 4);
                RotateCharacter(currentDirection);
            }

            // Rotate the character clockwise
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentDirection = (FacingDirection)(((int)currentDirection + 3) % 4);
                RotateCharacter(currentDirection);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SetRotateVisualActive(false);
                IsActive(false);
                onActionComplete();
            }

        }

        /// <summary>Checks if the object is currently rotating and manages the rotation process. </summary>
        private void IsRotating()
        {
            if (Quaternion.Angle(transform.rotation, target) < rotationTolerance)
            {
                doneRotating = true;
                unit.UpdateUnitDirection();
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                doneRotating = false;
            }
        }

        /// <summary>Rotates the character to the specified facing direction.</summary>
        /// <param name="state">The desired facing direction.</param>
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
        private void IsActive(bool status) => isActive = status;
    }

}