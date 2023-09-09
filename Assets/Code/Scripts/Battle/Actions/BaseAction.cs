namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using UnityEngine;

    public abstract class BaseAction : MonoBehaviour
    {
        protected bool isActive;
        protected Unit unit;
        protected Action onActionComplete;
        protected abstract string actionName { get; }

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        public string GetActionName() => actionName;

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        /// <summary>Checks whether the given grid position is a valid action grid position for the unit.
        /// </summary>
        /// <param name="gridPosition">The grid position to be checked.</param>
        /// <returns>True if the grid position is a valid action grid position, otherwise false.</returns>
        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPosition();
            return validGridPositionList.Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidActionGridPosition();


    }

}