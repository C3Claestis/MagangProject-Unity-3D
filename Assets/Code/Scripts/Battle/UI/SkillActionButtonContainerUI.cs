namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.UnitSystem;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class SkillActionButtonContainerUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;
        [SerializeField] protected TextMeshProUGUI buttonText;
        [SerializeField] protected Transform selectedDummyImage;
        private List<Transform> actionButtonList;

        private void Start()
        {
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnActionCompleted += UnitActionSystem_OnActionCompleted;

            actionButtonList = new List<Transform>();
            HideUI(true);
            ShowSelectedDummy(false);
        }

        public void ButtonOnClick()
        {
            int firstButton = actionButtonList.Count - 1;
            UnitActionSystemUI.Instance.SetSelectedGameObject(actionButtonList[firstButton].gameObject);

            UnitActionSystemUI.Instance.ShowActionBlocker(true);

            LinkCancel(true);
            HideUI(false);
            ShowSelectedDummy(true);
            //Active the Conver and active 
        }

        private void CancelAction()
        {
            LinkCancel(false);
            UnitActionSystemUI.Instance.SetSelectedGameObject(gameObject);
            UnitActionSystemUI.Instance.ShowActionBlocker(false);
            HideUI(true);
            ShowSelectedDummy(false);
        }

        public void LinkCancel(bool status)
        {
            if (status == true)
                PlayerInputController.Instance.OnCancelActionPressed += PlayerInputController_OnCancelPressed;
            else
                PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        private void HideUI(bool status)
        {
            CanvasGroup canvasGroup = actionButtonContainer.GetComponent<CanvasGroup>();

            if (status)
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void UpdateContainerButtonTextColor()
        {
            Unit unit = UnitTurnSystem.Instance.GetSelectedUnit();
            bool actionStatus = unit.GetActionStatus(ActionCategory.Skill);
            float color = 0.2235294f;

            Color normalColor = new Color(color, color, color, 1f);
            Color disableColor = new Color(color, color, color, 0.5f);
            Color selectedColor = actionStatus ? disableColor : normalColor;

            buttonText.color = selectedColor;
        }

        private void CreateButtons(List<BaseSkillAction> skillActionList)
        {
            foreach (BaseSkillAction skillAction in skillActionList)
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
                var skillActionButtonUI = actionButtonTransform.GetComponent<SkillActionButtonUI>();

                skillActionButtonUI.InitializeActionButton(skillAction, this);
                actionButtonList.Add(actionButtonTransform);
            }
        }

        private void CreateButtonNavigation()
        {
            if (actionButtonList.Count - 1 == 0) return;

            for (int i = 0; i < actionButtonList.Count; i++)
            {
                Button button = actionButtonList[i].GetComponent<Button>();

                Navigation newNavigation = button.navigation;
                newNavigation.mode = Navigation.Mode.Explicit;

                if (i == 0)
                {
                    newNavigation.selectOnDown = actionButtonList[actionButtonList.Count - 1].GetComponent<Button>();
                    newNavigation.selectOnUp = actionButtonList[i + 1].GetComponent<Button>();

                }
                else if (i == actionButtonList.Count - 1)
                {
                    newNavigation.selectOnDown = actionButtonList[i - 1].GetComponent<Button>();
                    newNavigation.selectOnUp = actionButtonList[0].GetComponent<Button>();
                }
                else
                {
                    newNavigation.selectOnDown = actionButtonList[i - 1].GetComponent<Button>();
                    newNavigation.selectOnUp = actionButtonList[i + 1].GetComponent<Button>();
                }

                button.navigation = newNavigation;
            }
        }

        public List<Transform> GetActionButtonList() => actionButtonList;

        private void ShowSelectedDummy(bool status) => selectedDummyImage.gameObject.SetActive(status);

        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            List<BaseSkillAction> skillActionList = new List<BaseSkillAction>();
            Unit unit = UnitTurnSystem.Instance.GetSelectedUnit();
            actionButtonList.Clear();

            for (int i = 0; i < actionButtonContainer.childCount; i++)
            {
                Transform buttonTransform = actionButtonContainer.GetChild(i);
                Destroy(buttonTransform.gameObject);
            }

            foreach (BaseAction skillAction in unit.GetBaseActionArray())
            {
                if (!(skillAction is BaseSkillAction)) continue;

                skillActionList.Add((BaseSkillAction)skillAction);
            }

            CreateButtons(skillActionList);
            CreateButtonNavigation();
            UpdateContainerButtonTextColor();
        }

        private void UnitActionSystem_OnActionCompleted(object sender, EventArgs e)
        {
            UpdateContainerButtonTextColor();
            ShowSelectedDummy(false);
            HideUI(true);
        }

        private void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            CancelAction();
        }

    }
}