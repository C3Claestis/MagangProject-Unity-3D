namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using UnityEngine;

    public class SpinAction : BaseAction
    {
        protected override string actionName { get { return "Spin"; } }
        private float totalSpinAmount;

        private void Update()
        {
            HandleSpin();
        }

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
            this.onActionComplete = onActionComplete;
            totalSpinAmount = 0f;
            isActive = true;
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();
            return new List<GridPosition> { unitGridPosition };
        }
    }
}