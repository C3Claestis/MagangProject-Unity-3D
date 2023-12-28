namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class BasicAttack : BaseSkillAction
    {
        protected override string actionName => "Basic Attack";
        protected override float powerPercentage => 100;
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        protected override ActionType actionType => ActionType.Physyical;
        protected override string actionDescription =>
        "Attacking unit in front of them.";


        /// <summary>Performs a spin action at the specified grid position and invokes a callback upon completion.</summary>
        /// <param name="gridPosition">The target grid position for the spin action.</param>
        /// <param name="onActionComplete">Callback function to execute when the spin action is complete.</param>
        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            base.TakeAction(gridPosition, onActionComplete);
            SetActive(false);

            if (unit.IsEnemy()) YesButtonAction();
            else UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
        }

        private void Attacking()
        {
            var targetPosition = LevelGrid.Instance.GetWorldPosition(targetGrid);
            IDamageable damageable;
            float facingBonus = 1f;

            if (Pathfinding.Instance.IsObstacleOnGrid(targetPosition, out Transform objectTransform))
            {
                damageable = objectTransform.GetComponent<IDamageable>();
            }
            else
            {
                Unit targetUnit = LevelGrid.Instance.GetUnitListAtGridPosition(targetGrid)[0];
                damageable = targetUnit.GetComponent<IDamageable>();

                facingBonus = LevelGrid.Instance.RelativeFacingChecker(targetUnit, unit) / 100f;
            }

            int damageValue = (int)(unit.GetCurrentPhysicalAttack() * facingBonus * (powerPercentage / 100f));
            //! Fix Later
            bool critical = false;

            damageable.Damage(damageValue, critical);
            isActive = false;
            onActionComplete();
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();
            return GetValidActionGridPosition(unitGridPosition);
        }

        public List<GridPosition> GetValidActionGridPosition(GridPosition unitGridPosition)
        {
            List<GridPosition> validGridPosition = GetRangeActionGridPosition(unitGridPosition);
            List<GridPosition> newValidGridPosition = new List<GridPosition>();

            foreach (GridPosition gridPosition in validGridPosition)
            {
                Unit targetUnit = CheckForUnitTarget(gridPosition);
                IDamageable targetObstacle = CheckForObstacleTarget(gridPosition);

                if (targetUnit == null && targetObstacle == null) continue;

                newValidGridPosition.Add(gridPosition);
            }
            return newValidGridPosition;
        }

        private Unit CheckForUnitTarget(GridPosition gridPosition)
        {
            if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition)) return null;
            Unit targetUnit = LevelGrid.Instance.GetUnitListAtGridPosition(gridPosition)[0];
            if (!targetUnit.IsAlive()) return null;
            if (unit.IsEnemy() == targetUnit.IsEnemy()) return null;
            if (unit.IsEnemy() && targetUnit.IsEnemy()) return null;
            return targetUnit;
        }

        private IDamageable CheckForObstacleTarget(GridPosition gridPosition)
        {
            var targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            if (Pathfinding.Instance.IsObstacleOnGrid(targetPosition, out Transform objectTransform))
            {
                if (objectTransform.GetComponent<Obstacle>().IsBroken()) return null;
                return objectTransform.GetComponent<IDamageable>();
            }

            return null;
        }

        private bool IsValidGrid(GridPosition testGridPosition)
        {
            if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) return false;

            Vector3 testWorldPosition = LevelGrid.Instance.GetWorldPosition(testGridPosition);
            if (Pathfinding.Instance.IsObstacleOnGrid(testWorldPosition, out string objectTag) && objectTag != "Tier2_Obstacles") return false;
            if (unit.IsEnemy() && objectTag != "") return false;

            return true;
        }

        public override List<GridPosition> GetRangeActionGridPosition()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();
            return GetRangeActionGridPosition(unitGridPosition);
        }

        public List<GridPosition> GetRangeActionGridPosition(GridPosition unitGridPosition)
        {
            List<GridPosition> validGridPosition = new List<GridPosition>();
            int offset = 1;

            GridPosition testRightGrid = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z);
            if (IsValidGrid(testRightGrid)) validGridPosition.Add(testRightGrid);

            GridPosition testLeftGrid = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z);
            if (IsValidGrid(testLeftGrid)) validGridPosition.Add(testLeftGrid);

            GridPosition testUpperGrid = new GridPosition(unitGridPosition.x, unitGridPosition.z + offset);
            if (IsValidGrid(testUpperGrid)) validGridPosition.Add(testUpperGrid);

            GridPosition testBottomGrid = new GridPosition(unitGridPosition.x, unitGridPosition.z - offset);
            if (IsValidGrid(testBottomGrid)) validGridPosition.Add(testBottomGrid);

            return validGridPosition;
        }

        protected override void YesButtonAction()
        {
            base.YesButtonAction();
            SetActive(true);
            FacingTarget(LevelGrid.Instance.GetWorldPosition(targetGrid));

            int waitingInSecond = 1;
            StartCoroutine(DelayAndAction(Attacking, waitingInSecond));
        }

        private IEnumerator DelayAndAction(Action action, int waitForSeconds)
        {
            yield return new WaitForSeconds(waitForSeconds);
            action();
        }
    }
}