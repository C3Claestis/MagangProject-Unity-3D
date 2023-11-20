
namespace Nivandria.Battle.AI
{
    using System;
    using System.Collections;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Random = UnityEngine.Random;

    public class BasicEnemyAI : UnitAI
    {
        public override void HandleEnemyTurn()
        {
            if (!TryAttackAction())
            {
                if (unit.GetActionStatus(ActionCategory.Skill)) StartCoroutine(WaitAndNext(UnitTurnSystem.Instance.HandleUnitSelection, 1));
                else if (!TryMoveAction()) StartCoroutine(WaitAndNext(UnitTurnSystem.Instance.HandleUnitSelection, 1));
            }
        }

        private bool TryAttackAction()
        {
            if (unit.GetActionStatus(ActionCategory.Skill)) return false;

            selectedAction = unit.GetAction<BasicAttack>();

            List<GridPosition> validGrid = selectedAction.GetValidActionGridPosition();
            if (validGrid.Count != 0)
            {
                StartCoroutine(WaitAndNext(AttackAction, 1));
                return true;
            }
            return false;
        }

        private void AttackAction()
        {
            List<GridPosition> attackGrid = selectedAction.GetValidActionGridPosition();
            int randomNumber = Random.Range(0, attackGrid.Count);
            StartCoroutine(WaitAndAction(selectedAction.TakeAction, attackGrid[randomNumber], 1));
        }

        private bool TryMoveAction()
        {
            if (unit.GetActionStatus(ActionCategory.Move)) return false;

            selectedAction = unit.GetAction<MoveAction>();

            List<GridPosition> validGrid = selectedAction.GetValidActionGridPosition();
            if (validGrid.Count != 0)
            {
                StartCoroutine(WaitAndNext(MoveAction, 1));
                return true;
            }
            return false;
        }

        private void MoveAction()
        {
            GridPosition moveposition = BestMovePosition();
            StartCoroutine(WaitAndAction(selectedAction.TakeAction, moveposition, 1));
        }

        private GridPosition BestMovePosition()
        {
            List<GridPosition> validGridList = selectedAction.GetValidActionGridPosition();
            GridPosition validGrid = new GridPosition();
            int mostDamage = 0;

            foreach (var grid in validGridList)
            {
                List<GridPosition> testValidGridList = unit.GetAction<BasicAttack>().GetValidActionGridPosition(grid);
                if (testValidGridList.Count == 0) continue;
                if (LevelGrid.Instance.GetUnitListAtGridPosition(grid).Count == 0) continue;

                Unit targetUnit = LevelGrid.Instance.GetUnitListAtGridPosition(grid)[0];
                int damage = LevelGrid.Instance.RelativeFacingChecker(targetUnit, unit);

                if (damage < mostDamage) continue;
                mostDamage = damage;
                validGrid = grid;
            }

            if (mostDamage == 0) validGrid = BestNearestUnitGrid();

            return validGrid;
        }

        private GridPosition BestNearestUnitGrid()
        {
            string unitTag = "Units";
            List<GridPosition> validGridList = selectedAction.GetValidActionGridPosition();
            GridPosition validGrid = new GridPosition();
            GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);
            List<Unit> unitList = new List<Unit>();

            foreach (var unitObject in unitObjects)
            {
                Unit unit = unitObject.GetComponent<Unit>();
                if (unit.IsEnemy()) continue;
                unitList.Add(unit);
            }

            float nearestDistance = 999f;
            foreach (var grid in validGridList)
            {
                foreach (var unit in unitList)
                {
                    float newDistance = unit.GetGridPosition().DistanceTo(grid);
                    if (newDistance < nearestDistance)
                    {
                        nearestDistance = newDistance;
                        validGrid = grid;
                    }
                }
            }

            return validGrid;
        }

        private IEnumerator WaitAndAction(Action<GridPosition, Action> action, GridPosition validPosition, int waitForSeconds)
        {
            UnitActionSystem.Instance.SetSelectedAction(selectedAction);
            GridSystemVisual.Instance.UpdateGridVisual();
            Pointer.Instance.SetPointerOnGrid(validPosition);

            yield return new WaitForSeconds(waitForSeconds);
            action(validPosition, OnActionComplete);
        }

        private IEnumerator WaitAndNext(Action action, int waitForSeconds)
        {
            yield return new WaitForSeconds(waitForSeconds);
            action();
        }

        void OnActionComplete()
        {
            unit.SetActionStatus(selectedAction.GetActionCategory(), true);
            GridSystemVisual.Instance.HideAllGridPosition();
            Pointer.Instance.SetPointerOnGrid(unit.GetGridPosition());

            if (!UnitTurnSystem.Instance.CheckGameOverCondition()) HandleEnemyTurn();
        }

    }

}