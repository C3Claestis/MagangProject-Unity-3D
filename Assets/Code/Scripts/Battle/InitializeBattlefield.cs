namespace Nivandria.Battle
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class InitializeBattlefield : MonoBehaviour
    {
        [SerializeField] private List<UnitSetup> friendlyUnitList;
        [SerializeField] private List<UnitSetup> enemyUnitList;

        private void Start()
        {
            SetupUnit();
        }

        private void SetupUnit()
        {
            List<UnitSetup> unitSetupList = new List<UnitSetup>();
            unitSetupList.AddRange(friendlyUnitList);
            unitSetupList.AddRange(enemyUnitList);

            foreach (var unit in unitSetupList)
            {
                Transform prefab = unit.GetPrefab();
                Vector3 position = new Vector3(unit.GetPosition().x, 0, unit.GetPosition().z);

                Transform newUnitTransform = Instantiate(prefab, position, Quaternion.identity, transform);
                Unit newUnit = newUnitTransform.GetComponent<Unit>();

                if (newUnit.IsEnemy()) newUnitTransform.rotation = Quaternion.Euler(0, 270, 0);
                else newUnitTransform.rotation = Quaternion.Euler(0, 90, 0);

                newUnit.UpdateUnitDirection();
            }
        }

        private void SetupObject()
        {

        }
    }

}