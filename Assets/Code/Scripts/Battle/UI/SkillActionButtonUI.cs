namespace Nivandria.Battle.UI
{
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;

    public class SkillActionButtonUI : BaseActionButtonUI
    {
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        private BaseSkillAction skillAction;
        private SkillActionButtonContainerUI skillActionButtonContainerUI;

        public override void ButtonOnClick()
        {
            if (skillAction is WordAction)
            {
                WordActionSkill();
                return;
            }

            NormalSkill();

        }

        private void NormalSkill()
        {
            UnitActionSystem.Instance.SetSelectedAction(skillAction);
            GridSystemVisual.Instance.UpdateGridVisual();

            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(skillAction.GetActionCategory())) return;

            UnitActionSystem.Instance.HideActionUI();
            Pointer.Instance.SetActive(true);

            skillAction.InitializeCancel();
            skillActionButtonContainerUI.LinkCancel(false);

            PlayerInputController.Instance.SetActionMap("Gridmap");

            UnitActionSystemUI.Instance.SetSelectedGameObject(null);
        }

        private void WordActionSkill()
        {
            UnitActionSystem.Instance.SetSelectedAction(skillAction);
            GridSystemVisual.Instance.HideAllGridPosition();

            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(skillAction.GetActionCategory())) return;

            List<RandomWordButtonUI> buttonPressedList = WordActionUI.Instance.GetButtonPressedList();

            foreach (var button in buttonPressedList)
            {
                button.SetPressedStatus(false);
                button.ChangeButtonColor();
            }

            WordActionUI.Instance.ResetButtonPressedList();
            WordActionUI.Instance.UpdateInputText();
            WordActionUI.Instance.CheckWord();

            skillActionButtonContainerUI.LinkCancel(false);
            WordActionUI.Instance.LinkCancel(true);
            UnitActionSystem.Instance.HideActionUI();
            WordActionUI.Instance.HideUI(false);
            WordActionUI.Instance.SetSelectedUIToFirstButton();
        }

        public void InitializeActionButton(BaseSkillAction skillAction, SkillActionButtonContainerUI skillActionButtonContainerUI)
        {
            this.skillAction = skillAction;
            this.skillActionButtonContainerUI = skillActionButtonContainerUI;
            string actionName = skillAction.GetName();

            gameObject.name = actionName;
            buttonText.text = actionName;
        }

        public SkillActionButtonContainerUI GetActionContainer() => skillActionButtonContainerUI;
        public BaseSkillAction GetSkillAction() => skillAction;
    }

}