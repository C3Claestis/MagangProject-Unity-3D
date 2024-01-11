namespace Nivandria.Battle
{
    using System;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    [Serializable]
    public class UnitSetup
    {
        [SerializeField] private Transform unitPrefab;
        [SerializeField] private GridPosition position;

        public UnitSetup(Transform unitPrefab, GridPosition position)
        {
            this.unitPrefab = unitPrefab;
            this.position = position;
        }

        public void SetPosition(GridPosition newPosition) => position = newPosition;

        public Transform GetPrefab() => unitPrefab;
        public GridPosition GetPosition() => position;
    }
}