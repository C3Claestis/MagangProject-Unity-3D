namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine;
    using System;
    using TMPro;

    public abstract class BaseActionButtonUI : MonoBehaviour
    {
        protected Unit unit;
        protected abstract ActionCategory actionCategory { get; }
        [SerializeField] protected TextMeshProUGUI buttonText;

        private void Start()
        {
            UnitTurnSystem.Instance.OnUnitListChanged += UnitTurnSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnActionCompleted += UnitActionSystem_OnActionCompleted;
        }

        private void OnDestroy()
        {
            UnitTurnSystem.Instance.OnUnitListChanged -= UnitTurnSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnActionCompleted -= UnitActionSystem_OnActionCompleted;
        }

        public abstract void ButtonOnClick();

        public void UpdateButtonTextColor()
        {
            if (unit == null) unit = UnitTurnSystem.Instance.GetSelectedUnit();
            bool actionStatus = unit.GetActionStatus(actionCategory);
            float color = 0.2235294f;

            Color normalColor = new Color(color, color, color, 1f);
            Color disableColor = new Color(color, color, color, 0.5f);
            Color selectedColor = actionStatus ? disableColor : normalColor;

            buttonText.color = selectedColor;
        }

        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            unit = UnitTurnSystem.Instance.GetSelectedUnit();
            UpdateButtonTextColor();
        }

        private void UnitActionSystem_OnActionCompleted(object sender, EventArgs e)
        {
            UpdateButtonTextColor();
        }

        public ActionCategory GetActionCategory() => actionCategory;
        public GameObject GetButtonGameObject() => gameObject;
    }
}