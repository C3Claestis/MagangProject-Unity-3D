namespace Nivandria.Battle
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class InitializeBattlefield : MonoBehaviour
    {
        [SerializeField] private List<UnitSetup> unitSetupList;

        private void Start()
        {
            foreach (var unit in unitSetupList)
            {
                Transform prefab = unit.GetPrefab();
                Vector3 position = unit.GetPosition();

                Transform newUnitTransform = Instantiate(prefab, position, Quaternion.identity, transform);
                Unit newUnit = newUnitTransform.GetComponent<Unit>();

                if(newUnit.IsEnemy()) newUnitTransform.rotation = Quaternion.Euler(0, 270, 0);
                else newUnitTransform.rotation = Quaternion.Euler(0, 90, 0);

                newUnit.UpdateUnitDirection();
            }
        }
    }

}