namespace Nivandria.Battle.UI
{
    using System;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine.EventSystems;
    using UnityEngine;
    using System.Collections.Generic;

    public class UnitActionSystemUI : MonoBehaviour
    {
        public static UnitActionSystemUI Instance { get; private set; }

        [SerializeField] private Transform turnSystemButton;
        [SerializeField] private Transform dialogueConfirmationPrefab;
        [SerializeField] private Transform skillActionButtonContainer;
        [SerializeField] private Transform itemActionButton;
        [SerializeField] private Transform moveActionButton;
        [SerializeField] private Transform escapeActionButton;
        [SerializeField] private Transform actionBlocker;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitActionSystemUI! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnActionCompleted += UnitActionSystem_OnActionCompleted;
            ShowActionBlocker(false);
            UnitActionSystem.Instance.ClearBusyUI();
        }

        public void SelectUIBaseOnSelectedAction()
        {
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            ActionCategory actionCategory = selectedAction.GetActionCategory();

            switch (actionCategory)
            {
                case ActionCategory.Skill:
                    SetSelectedGameObject(skillActionButtonContainer.gameObject);
                    return;

                case ActionCategory.Item:
                    SetSelectedGameObject(itemActionButton.gameObject);
                    return;

                case ActionCategory.Move:
                    SetSelectedGameObject(moveActionButton.gameObject);
                    return;
            }

            Debug.LogError("Action button not found!");
        }

        public virtual void InitializeConfirmationButton(Action onYesButtonSelected, Action onNoButtonSelected)
        {
            Transform uiTransform = GameObject.FindGameObjectWithTag("UI").transform;
            Transform confirmationTranform = Instantiate(dialogueConfirmationPrefab, uiTransform);

            PlayerInputController.Instance.SetActionMap("ConfirmationUI");
            confirmationTranform.GetComponent<ConfirmationDialogUI>().InitializeConfirmationButton(onYesButtonSelected, onNoButtonSelected);
        }

        public Transform GetActionButton(BaseSkillAction skillAction)
        {
            var skillButtonContainer = skillActionButtonContainer.GetComponent<SkillActionButtonContainerUI>();
            List<Transform> buttonTransformList = skillButtonContainer.GetActionButtonList();

            foreach (Transform buttonTransform in buttonTransformList)
            {
                var skillButton = buttonTransform.GetComponent<SkillActionButtonUI>();
                bool check = skillButton.GetSkillAction() == skillAction;

                if (!check) continue;

                return buttonTransform;
            }

            Debug.LogError("Can't find button with this action : ");
            return null;
        }

        public void SetSelectedGameObject(GameObject selectedObject)
        {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(selectedObject, new BaseEventData(eventSystem));
        }

        public Transform GetTurnSystemButton() => turnSystemButton;

        public void ShowActionBlocker(bool status) => actionBlocker.gameObject.SetActive(status);

        //EVENT FUNCTION
        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            SetSelectedGameObject(skillActionButtonContainer.gameObject);
        }

        private void UnitActionSystem_OnActionCompleted(object sender, EventArgs e)
        {
            ShowActionBlocker(false);
        }

    }
}