namespace Nivandria.Battle
{
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class InitializeBattlefield : MonoBehaviour
    {
        private List<UnitSetup> friendlyUnitList;
        private List<UnitSetup> enemyUnitList;

        List<Transform> unitList;

        public void SetupUnit()
        {
            List<UnitSetup> unitSetupList = new List<UnitSetup>();
            unitList = new List<Transform>();

            if (friendlyUnitList != null) unitSetupList.AddRange(friendlyUnitList);
            unitSetupList.AddRange(enemyUnitList);
            int cellSize = (int)LevelGrid.Instance.GetCellSize();

            foreach (var unit in unitSetupList)
            {
                Transform prefab = unit.GetPrefab();
                Vector3 position = new Vector3(unit.GetPosition().x, 0, unit.GetPosition().z) * cellSize;

                Transform newUnitTransform = Instantiate(prefab, position, Quaternion.identity, transform);
                Unit newUnit = newUnitTransform.GetComponent<Unit>();

                unitList.Add(newUnitTransform);

                if (newUnit.IsEnemy()) newUnitTransform.rotation = Quaternion.Euler(0, 270, 0);
                else newUnitTransform.rotation = Quaternion.Euler(0, 90, 0);

                newUnit.UpdateUnitDirection();
            }
        }


        public void SetupObject()
        {

        }

        public void SetFriendlyUnitList(List<UnitSetup> unitList) => friendlyUnitList = unitList;
        public void SetEnemyUnitList(List<UnitSetup> unitList) => enemyUnitList = unitList;


        public List<Transform> GetUnitList() => unitList;
    }

}