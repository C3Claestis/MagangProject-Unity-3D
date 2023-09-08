namespace Nivandria.Battle.Action
{
    using System;
    using UnityEngine;

    public abstract class BaseAction : MonoBehaviour
    {
        protected bool isActive;
        protected Unit unit;
        protected Action onActionComplete;
        protected abstract string actionName { get; }

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        public string GetActionName() => actionName;
    }

}