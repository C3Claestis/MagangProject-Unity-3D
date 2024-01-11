namespace Nivandria.Battle
{
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class InitializeBattlefield : MonoBehaviour
    {
        private List<UnitSetup> friendlyUnitList;
        private List<UnitSetup> enemyUnitList;

        List<Transform> unitList;

        public void SetupUnits()
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
                LevelGrid.Instance.AddUnitAtGridPosition(LevelGrid.Instance.GetGridPosition(position), newUnit);

                unitList.Add(newUnitTransform);

                if (newUnit.IsEnemy()) newUnitTransform.rotation = Quaternion.Euler(0, 270, 0);
                else newUnitTransform.rotation = Quaternion.Euler(0, 90, 0);

                newUnit.UpdateUnitDirection();
            }
        }

        public void SetupObjects(List<ObjectSetup> objectList, Transform container)
        {
            foreach (var objectSetup in objectList)
            {
                int chances = objectSetup.GetChances();
                int objectCount = objectSetup.GetObjectNumber();
                List<GridPosition> gridPositionList = new List<GridPosition>(objectSetup.GetPositionList());

                for (int i = 0; i < objectCount; i++)
                {
                    if (gridPositionList.Count == 0) break;

                    int randomChance = Random.Range(0, 100);
                    if (randomChance > chances) continue;

                    int randomPosition = Random.Range(0, gridPositionList.Count);
                    GridPosition gridPosition = gridPositionList[randomPosition];
                    Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

                    if (!LevelGrid.Instance.IsValidGridPosition(gridPosition) ||
                        Pathfinding.Instance.IsObstacleOnGrid(worldPosition, out string objectName) ||
                        LevelGrid.Instance.GetUnitListAtGridPosition(gridPosition).Count > 0
                    )
                    {
                        gridPositionList.Remove(gridPosition);
                        continue;
                    }

                    Instantiate(objectSetup.GetPrefab(), worldPosition, Quaternion.identity, container);
                    gridPositionList.Remove(gridPosition);
                }
            }
        }


        public void SetFriendlyUnitList(List<UnitSetup> unitList) => friendlyUnitList = unitList;
        public void SetEnemyUnitList(List<UnitSetup> unitList) => enemyUnitList = unitList;


        public List<Transform> GetUnitList() => unitList;
    }

}