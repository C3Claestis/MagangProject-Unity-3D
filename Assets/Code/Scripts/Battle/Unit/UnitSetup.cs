namespace Nivandria.Battle
{
    using System;
    using Nivandria.Battle.Grid;
    using UnityEngine;

    [Serializable]
    public class UnitSetup
    {
        [SerializeField] private Transform unitPrefab;
        [SerializeField] private GridPosition position;

        public Transform GetPrefab() => unitPrefab;
        public GridPosition GetPosition() => position;
    }
}