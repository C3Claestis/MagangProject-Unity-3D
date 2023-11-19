namespace Nivandria.Battle
{
    using System;
    using UnityEngine;

    [Serializable]
    public class UnitSetup
    {
        [SerializeField] private Transform unitPrefab;
        [SerializeField] private Vector3 position;

        public Transform GetPrefab() => unitPrefab;
        public Vector3 GetPosition() => position;
    }
}