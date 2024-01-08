

namespace Nivandria.Battle
{
    using System;
    using UnityEngine;
    using Nivandria.Battle.UnitSystem;

    public class RotatePlaceUnit : MonoBehaviour
    {

        private FacingDirection currentDirection = FacingDirection.EAST;
        private Quaternion target;
        private Unit unit;
        private float rotationTolerance = 5f;
        private float smooth = 5.0f;
        private bool doneRotating = true;
        private bool isActive = false;
        private Transform rotateVisualTransform;

        private void Update()
        {
            if (doneRotating && !isActive) return;
            IsRotating();
        }

        public void StartRotating(Unit unit)
        {
            PlayerInputController.Instance.OnCancelActionPressed += PlayerInputController_OnCancelPressed;

            this.unit = unit;
            PlayerInputController.Instance.SetActionMap("RotateUnit");

            currentDirection = FacingDirection.EAST;

            Pointer.Instance.GetRotateVisual().ResetPosition();
            rotateVisualTransform = Pointer.Instance.GetRotateVisual().GetTransform();

            doneRotating = true;
            RotateCharacter(currentDirection);
            SetRotateVisualActive(true);
            IsActive(true);
        }

        public void RotateLeft()
        {
            currentDirection = (FacingDirection)(((int)currentDirection + 1) % 4);
            RotateCharacter(currentDirection);
        }

        public void RotateRight()
        {
            currentDirection = (FacingDirection)(((int)currentDirection + 3) % 4);
            RotateCharacter(currentDirection);
        }

        public void ConfirmRotation()
        {
            PlacingSystem.Instance.ConfirmPlace(unit.GetGridPosition());

            SetRotateVisualActive(false);
            IsActive(false);
            PlayerInputController.Instance.SetActionMap("PlacingUnits");
            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        private void CancelAction()
        {
            PlacingSystem.Instance.Cancel_Action();
            PlacingSystem.Instance.CancelRotate();

            SetRotateVisualActive(false);
            IsActive(false);
            PlayerInputController.Instance.SetActionMap("PlacingUnits");
            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        /// <summary>Checks if the object is currently rotating and manages the rotation process. </summary>
        private void IsRotating()
        {
            Transform unitTransfrom = unit.GetUnitTransform();

            if (Quaternion.Angle(unitTransfrom.rotation, target) < rotationTolerance)
            {
                doneRotating = true;
                unit.UpdateUnitDirection();
            }
            else
            {

                unitTransfrom.rotation = Quaternion.Slerp(unitTransfrom.rotation, target, Time.deltaTime * smooth);

                Quaternion xzRotation = Quaternion.Euler(rotateVisualTransform.rotation.eulerAngles.x, unitTransfrom.rotation.eulerAngles.y, rotateVisualTransform.rotation.eulerAngles.z);
                rotateVisualTransform.rotation = xzRotation;

                doneRotating = false;
            }
        }

        /// <summary>Rotates the character to the specified facing direction.</summary>
        /// <param name="state">The desired facing direction.</param>
        private void RotateCharacter(FacingDirection state)
        {
            switch (state)
            {
                case FacingDirection.NORTH:
                    target = Quaternion.Euler(0, 0, 0);
                    break;
                case FacingDirection.EAST:
                    target = Quaternion.Euler(0, 90, 0);
                    break;
                case FacingDirection.SOUTH:
                    target = Quaternion.Euler(0, 180, 0);
                    break;
                case FacingDirection.WEST:
                    target = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }

        private void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            CancelAction();
        }

        private void SetRotateVisualActive(bool status) => Pointer.Instance.GetRotateVisual().SetActive(status);
        private void IsActive(bool status) => isActive = status;

    }
}