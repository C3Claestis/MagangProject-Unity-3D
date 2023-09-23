namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using UnityEngine;

    public class UnitActionSystemUI : MonoBehaviour
    {
        public static UnitActionSystemUI Instance { get; private set; }

        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        [SerializeField] private Transform actionButtonBackground;
        [SerializeField] private Transform turnSystemButton;

        private List<ActionButtonUI> actionButtonUIList;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitActionSystemUI! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;

            CreateUnitActionButtons();
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

            int numberArray = 1;
            foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonTransform.gameObject.name = numberArray.ToString();
                numberArray++;
                actionButtonUI.SetBaseAction(baseAction);

                actionButtonUIList.Add(actionButtonUI);
            }

            Button turnButton = turnSystemButton.GetComponent<Button>();
            Navigation turnButtonNavigation = turnButton.navigation;
            turnButtonNavigation.mode = Navigation.Mode.Explicit;

            for (int i = 0; i < actionButtonUIList.Count; i++)
            {

                Button button = actionButtonUIList[i].GetComponent<Button>();

                Navigation newNavigation = button.navigation;
                newNavigation.mode = Navigation.Mode.Explicit;

                if (actionButtonUIList.Count - 1 == 0)
                {
                    newNavigation.selectOnLeft = turnButton;
                    newNavigation.selectOnRight = turnButton;
                    turnButtonNavigation.selectOnLeft = button;
                    turnButtonNavigation.selectOnRight = button;
                }
                else if (i == 0)
                {
                    newNavigation.selectOnLeft = turnButton;
                    newNavigation.selectOnRight = actionButtonUIList[i + 1].GetComponent<Button>();
                    turnButtonNavigation.selectOnRight = button;

                }
                else if (i == actionButtonUIList.Count - 1)
                {
                    newNavigation.selectOnLeft = actionButtonUIList[i - 1].GetComponent<Button>();
                    newNavigation.selectOnRight = turnButton;
                    turnButtonNavigation.selectOnLeft = button;
                }
                else
                {
                    newNavigation.selectOnRight = actionButtonUIList[i + 1].GetComponent<Button>();
                    newNavigation.selectOnLeft = actionButtonUIList[i - 1].GetComponent<Button>();
                }

                button.navigation = newNavigation;
            }
            turnButton.navigation = turnButtonNavigation;

            SelectUIBaseOnSelectedAction();
            actionButtonBackground.gameObject.SetActive(true);
        }


        public void SelectUIBaseOnSelectedAction()
        {
            var eventSystem = EventSystem.current;
            var selectedAction = UnitActionSystem.Instance.GetSelectedAction();

            foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                BaseAction baseAction = actionButtonUI.GetBaseAction();

                if (selectedAction == baseAction)
                {
                    var selectButton = actionButtonUI;
                    eventSystem.SetSelectedGameObject(selectButton.gameObject, new BaseEventData(eventSystem));
                    return;
                }
            }

            Debug.LogError("Action button not found!");

        }


        //EVENT FUNCTION
        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
            SelectUIBaseOnSelectedAction();
        }
    }
}