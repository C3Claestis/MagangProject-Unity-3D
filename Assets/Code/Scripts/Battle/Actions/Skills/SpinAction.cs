namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UI;
    using UnityEngine;

    public class SpinAction : BaseSkillAction
    {
        protected override string actionName => "Spin";
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        protected override ActionType actionType => ActionType.Magical;
        protected override string actionDescription => 
        "Showcases a unit's agility with a 360-degree rotation, adding unpredictability to battlefield maneuvers.";

        private float totalSpinAmount;

        private void Update()
        {
            HandleSpin();
        }

        /// <summary>Handles the spinning animation of the object.</summary>
        private void HandleSpin()
        {
            if (!isActive) return;

            float spinAddAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
            totalSpinAmount += spinAddAmount;
            if (totalSpinAmount >= 360f)
            {
                isActive = false;
                onActionComplete();
            }
        }

        /// <summary>Performs a spin action at the specified grid position and invokes a callback upon completion.</summary>
        /// <param name="gridPosition">The target grid position for the spin action.</param>
        /// <param name="onActionComplete">Callback function to execute when the spin action is complete.</param>
        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            base.TakeAction(gridPosition, onActionComplete);
            totalSpinAmount = 0f;
            SetActive(false);

            UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();
            return new List<GridPosition> { unitGridPosition };
        }

        protected override void YesButtonAction()
        {
            SetActive(true);
            base.YesButtonAction();
        }

        public override List<GridPosition> GetRangeActionGridPosition()
        {
            return null;
        }
    }
}