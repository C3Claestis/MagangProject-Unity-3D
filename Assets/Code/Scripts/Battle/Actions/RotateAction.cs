namespace Nivandria.Battle.Action
{
    using System;
    using UnityEngine;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.UI;

    public class RotateAction : MonoBehaviour
    {
        private FacingDirection currentDirection = FacingDirection.UP;
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

        /// <summary>Initiates the rotation of the object.</summary>
        /// <param name="unit">The unit to be rotated.</param>
        /// <param name="onActionComplete">Callback action to be executed when the rotation is complete.</param>
        public void StartRotating()
        {
            PlayerInputController.Instance.OnCancelActionPressed += PlayerInputController_OnCancelPressed;

            unit = UnitTurnSystem.Instance.GetSelectedUnit();
            PlayerInputController.Instance.SetActionMap("RotateUnit");

            currentDirection = unit.GetFacingDirection();

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
            UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
        }

        private void YesButtonAction()
        {
            SetRotateVisualActive(false);
            IsActive(false);
            UnitTurnSystem.Instance.HandleUnitSelection();
            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;

        }

        private void NoButtonAction()
        {
            PlayerInputController.Instance.SetActionMap("RotateUnit");
        }

        private void CancelAction()
        {
            SetRotateVisualActive(false);
            IsActive(false);
            UnitActionSystemUI.Instance.SetSelectedGameObject(gameObject);
            PlayerInputController.Instance.SetActionMap("BattleUI");
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

        private void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            CancelAction();
        }

        private void SetRotateVisualActive(bool status) => Pointer.Instance.GetRotateVisual().SetActive(status);
        private void IsActive(bool status) => isActive = status;
    }

}