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