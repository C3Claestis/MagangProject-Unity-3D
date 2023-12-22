namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.UnitSystem;
    using TMPro;
    using UnityEngine;

    public class ItemActionButtonContainerUI : MonoBehaviour
    {
        [SerializeField] private Transform itemButtonPrefab;
        [SerializeField] private Transform itemButtonContainer;
        [SerializeField] private Transform itemButtonPanel;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Transform selectedDummyImage;
        [SerializeField] private CanvasGroup scrollCanvas;

        private List<Transform> itemButtonList = new List<Transform>();

        private void Start()
        {
            UnitActionSystem.Instance.OnActionCompleted += UnitActionSystem_OnActionCompleted;

            HideUI(true);
            ShowSelectedDummy(false);

            SpawnItemButtons();
        }


        private void SpawnItemButtons()
        {
            for (int i = 0; i < itemButtonPanel.childCount; i++)
            {
                Transform button = itemButtonPanel.GetChild(i);
                Destroy(button.gameObject);
            }

            for (int j = 0; j < 5; j++)
            {
                Transform itemButton = Instantiate(itemButtonPrefab, itemButtonPanel);
                itemButtonList.Add(itemButton);
            }
        }

        public void ButtonClick()
        {
            if (itemButtonList.Count > 0)
            {
                int firstButton = 0;
                UnitActionSystemUI.Instance.SetSelectedGameObject(itemButtonList[firstButton].gameObject);
            }

            UnitActionSystemUI.Instance.ShowActionBlocker(true);

            LinkCancel(true);
            HideUI(false);
            ShowSelectedDummy(true);

            HideScroller(!(itemButtonList.Count > 3));
        }

        private void HideUI(bool status)
        {
            CanvasGroup canvasGroup = itemButtonContainer.GetComponent<CanvasGroup>();
            canvasGroup.alpha = !status ? 1 : 0;
            canvasGroup.interactable = !status;
            canvasGroup.blocksRaycasts = !status;
        }

        private void HideScroller(bool hide)
        {
            scrollCanvas.alpha = !hide ? 1 : 0;
            scrollCanvas.interactable = !hide;
            scrollCanvas.blocksRaycasts = !hide;
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

        private void ShowSelectedDummy(bool status) => selectedDummyImage.gameObject.SetActive(status);

        private void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            CancelAction();
        }

        private void UnitActionSystem_OnActionCompleted(object sender, EventArgs e)
        {
            ShowSelectedDummy(false);
            HideUI(true);
        }

    }

}