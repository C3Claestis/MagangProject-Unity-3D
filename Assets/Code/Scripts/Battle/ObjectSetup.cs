namespace Nivandria.Battle
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using UnityEngine;

    [Serializable]
    public class ObjectSetup
    {
        [SerializeField] private Transform objectPrefab;
        [SerializeField] private int objectNumber;
        [SerializeField][Range(0, 100)] private int chances;
        [SerializeField] private List<GridPosition> spawnPositionList;

        public Transform GetPrefab() => objectPrefab;
        public List<GridPosition> GetPositionList() => spawnPositionList;
        public int GetChances() => chances;
        public int GetObjectNumber() => objectNumber;
        public void RemovePosition(GridPosition gridPosition) => spawnPositionList.Remove(gridPosition);
    }
}