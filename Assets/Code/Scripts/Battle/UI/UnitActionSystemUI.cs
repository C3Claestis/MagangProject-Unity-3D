namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using UnityEngine;
    using UnityEngine.UI;

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
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
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
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

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
        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
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