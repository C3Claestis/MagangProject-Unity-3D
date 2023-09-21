namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine;

    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        [SerializeField] private Transform actionButtonBackground;

        private List<ActionButtonUI> actionButtonUIList;

        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;

            CreateUnitActionButtons();
            UpdateUISelectedVisual();
        }

        /// <summary> Creates unit action buttons based on the selected unit's available actions. </summary>
        private void CreateUnitActionButtons()
        {
            foreach (Transform buttonTransform in actionButtonContainerTransform)
            {
                Destroy(buttonTransform.gameObject);
            }

            actionButtonUIList.Clear();
            actionButtonBackground.gameObject.SetActive(false);
            Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();

            if (selectedUnit == null) return;

            foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);

                actionButtonUIList.Add(actionButtonUI);
            }

            actionButtonBackground.gameObject.SetActive(true);
        }

        /// <summary> Updates the UI selected visual state for all action buttons. </summary>
        private void UpdateUISelectedVisual()
        {
            foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                actionButtonUI.UpdateUISelectedVisual();
            }
        }


        //EVENT FUNCTION
        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
            UpdateUISelectedVisual();
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
        {
            UpdateUISelectedVisual();
        }

    }
}