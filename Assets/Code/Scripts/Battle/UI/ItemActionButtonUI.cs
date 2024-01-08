namespace Nivandria.Battle.UI
{
    using System;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;

    public class ItemActionButtonUI : BaseActionButtonUI
    {
        protected override ActionCategory actionCategory => ActionCategory.Item;
        private ItemActionButtonContainerUI itemActionButtonContainerUI;
        private ItemAction itemAction;


        public void InitializeActionButton(ItemAction itemAction, ItemActionButtonContainerUI itemActionButtonContainerUI)
        {
            this.itemAction = itemAction;
            this.itemActionButtonContainerUI = itemActionButtonContainerUI;
            string actionName = itemAction.GetName();

            gameObject.name = actionName;
            buttonText.text = actionName;
        }

        public void UpdateButtonText(string buttonName)
        {
            buttonText.text = buttonName;
        }

        public void AddItemAction(ItemAction itemAction)
        {
            this.itemAction = itemAction;
        }

        public override void ButtonOnClick()
        {
            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(itemAction.GetActionCategory())) return;

            UnitActionSystem.Instance.SetSelectedAction(itemAction);
            GridSystemVisual.Instance.UpdateGridVisual();

            UnitActionSystem.Instance.HideActionUI();
            Pointer.Instance.SetActive(true);

            itemAction.InitializeCancel();
            itemActionButtonContainerUI.LinkCancel(false);

            PlayerInputController.Instance.SetActionMap("Gridmap");

            UnitActionSystemUI.Instance.SetSelectedGameObject(null);

        }


        public ItemAction GetItemAction() => itemAction;
        public ItemActionButtonContainerUI GetActionContainer() => itemActionButtonContainerUI;

        protected override void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            base.UnitTurnSystem_OnSelectedUnitChanged(null, e);
            itemAction.SetUnit(UnitTurnSystem.Instance.GetSelectedUnit());
        }
    }

}