namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class SkillActionButtonUI : BaseActionButtonUI
    {
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        private BaseSkillAction skillAction;
        private SkillActionButtonContainerUI skillActionButtonContainerUI;

        public override void ButtonOnClick()
        {

            UnitActionSystem.Instance.SetSelectedAction(skillAction);
            GridSystemVisual.Instance.UpdateGridVisual();

            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(skillAction.GetActionCategory())) return;
            
            EventSystem eventSystem = EventSystem.current;

            UnitActionSystem.Instance.SetBusyUI();
            Pointer.Instance.SetActive(true);

            skillAction.InitializeCancel();
            skillActionButtonContainerUI.LinkCancel(false);

            PlayerInputController.Instance.SetActionMap("Gridmap");
            eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
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