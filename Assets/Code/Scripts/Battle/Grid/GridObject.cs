namespace Nivandria.Battle.Grid
{
    using System.Collections.Generic;

    public class GridObject
    {
        private GridSystem<GridObject> gridSystem;
        private GridPosition gridPosition;
        private List<Unit> unitList;

        public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
            unitList = new List<Unit>();
        }

        public override string ToString()
        {
            string unitString = "";

            foreach (Unit unit in unitList)
            {
                unitString += unit.GetCharacterName() + "\n";
            }

            return gridPosition.ToString() + "\n" + unitString;
        }

        
        #region Getter Setter
        public void AddUnit(Unit unit) => unitList.Add(unit);
        public void RemoveUnit(Unit unit) => unitList.Remove(unit);
        public List<Unit> GetUnitList() => unitList;
        public bool HasAnyUnit() => unitList.Count > 0;
        #endregion
    }
}