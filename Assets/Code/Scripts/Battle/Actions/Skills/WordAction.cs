namespace Nivandria.Battle.Action
{
    using System.Collections.Generic;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.Grid;

    public class WordAction : BaseSkillAction
    {
        protected override string actionName => "Wording Skill";
        protected override string actionDescription => "Sacra Unique Skill";
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        protected override ActionType actionType => ActionType.Magical;

        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            LevelGrid levelGrid = LevelGrid.Instance;

            for (int xOffset = 1; xOffset < levelGrid.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!levelGrid.IsValidGridPosition(testGridPosition)) break;
                if (!levelGrid.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                validGridList.Add(testGridPosition);

            }

            return validGridList;
        }


        public override List<GridPosition> GetRangeActionGridPosition()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            LevelGrid levelGrid = LevelGrid.Instance;

            for (int xOffset = 1; xOffset < levelGrid.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!levelGrid.IsValidGridPosition(testGridPosition)) break;
                if (levelGrid.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                validGridList.Add(testGridPosition);
            }

            return validGridList;
        }

    }

}