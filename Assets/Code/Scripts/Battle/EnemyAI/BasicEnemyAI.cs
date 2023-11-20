
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
                if (unit.GetActionStatus(ActionCategory.Skill)) StartCoroutine(WaitAndNext(UnitTurnSystem.Instance.HandleUnitSelection, 2));
                else if (!TryMoveAction()) StartCoroutine(WaitAndNext(UnitTurnSystem.Instance.HandleUnitSelection, 2));
            }
        }

        private bool TryAttackAction()
        {
            if (unit.GetActionStatus(ActionCategory.Skill))
            {
                Debug.Log("Can't Attack because already spent skill.");
                return false;
            }

            selectedAction = unit.GetAction<BasicAttack>();

            List<GridPosition> validGrid = selectedAction.GetValidActionGridPosition();
            Debug.Log("Valid Attack grid : " + validGrid.Count);
            if (validGrid.Count != 0)
            {
                StartCoroutine(WaitAndNext(AttackAction, 2));
                return true;
            }
            Debug.Log("Can't Attack because no enemy.");
            return false;
        }

        private void AttackAction()
        {
            List<GridPosition> attackGrid = selectedAction.GetValidActionGridPosition();
            int randomNumber = Random.Range(0, attackGrid.Count);
            StartCoroutine(WaitAndAction(selectedAction.TakeAction, attackGrid[randomNumber], 2));
            Debug.Log("Attacking.");
        }

        private bool TryMoveAction()
        {
            if (unit.GetActionStatus(ActionCategory.Move))
            {
                Debug.Log("Can't Move because already spent skill.");
                return false;
            }

            selectedAction = unit.GetAction<MoveAction>();

            List<GridPosition> validGrid = selectedAction.GetValidActionGridPosition();
            if (validGrid.Count != 0)
            {
                StartCoroutine(WaitAndNext(MoveAction, 2));
                return true;
            }
            Debug.Log("Can't move because no valid grid.");
            return false;
        }

        private void MoveAction()
        {
            GridPosition moveposition = BestMovePosition();
            StartCoroutine(WaitAndAction(selectedAction.TakeAction, moveposition, 2));
            Debug.Log("Moving.");
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

            if (mostDamage == 0)
            {
                validGrid = BestNearestUnitGrid();
            }

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
            HandleEnemyTurn();
        }

    }

}