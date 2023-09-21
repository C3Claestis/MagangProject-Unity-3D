namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using TMPro;
    using UnityEngine;

    public class UnitTurnSystemUI : MonoBehaviour
    {
        [SerializeField] Transform UnitIconPrefab;
        [SerializeField] Transform waitingListContainer;
        [SerializeField] Transform nowTurnContainer;

        private List<Transform> unitTurnSystemList;

        private void Start()
        {
            unitTurnSystemList = new List<Transform>();

            foreach (Transform iconTransform in waitingListContainer)
            {
                Destroy(iconTransform.gameObject);
            }

            foreach (Transform iconTransform in nowTurnContainer)
            {
                Destroy(iconTransform.gameObject);
            }

            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
        }

        public void UpdateTurnSystemVisual()
        {
            Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();
            List<Unit> waitingUnitList = UnitTurnSystem.Instance.GetWaitingUnitList();
            List<Unit> movedUnitList = UnitTurnSystem.Instance.GetMovedUnitList();

            foreach (Transform iconTransform in unitTurnSystemList)
            {
                Destroy(iconTransform.gameObject);
            }

            unitTurnSystemList.Clear();

            if (selectedUnit == null) return;

            Transform nowUnitTurnIconTransform = Instantiate(UnitIconPrefab, nowTurnContainer);
            UnitIconUI selectedUnitIcon = nowUnitTurnIconTransform.GetComponent<UnitIconUI>();
            unitTurnSystemList.Add(nowUnitTurnIconTransform);
            selectedUnitIcon.SetIconName(selectedUnit.GetCharacterName());
            selectedUnitIcon.SetIcon(selectedUnit.GetUnitIcon());
            selectedUnitIcon.ShadeStatus(false);

            foreach (Unit unit in waitingUnitList)
            {
                Transform waitingUnitTurnIconTransform = Instantiate(UnitIconPrefab, waitingListContainer);
                UnitIconUI waitingUnitIcon = waitingUnitTurnIconTransform.GetComponent<UnitIconUI>();
                unitTurnSystemList.Add(waitingUnitTurnIconTransform);
                waitingUnitIcon.SetIconName(unit.GetCharacterName());
                waitingUnitIcon.SetIcon(unit.GetUnitIcon());
                waitingUnitIcon.ShadeStatus(false);
            }

            foreach (Unit unit in movedUnitList)
            {
                Transform movedUnitTurnIconTransform = Instantiate(UnitIconPrefab, waitingListContainer);
                UnitIconUI movedUnitIcon = movedUnitTurnIconTransform.GetComponent<UnitIconUI>();
                unitTurnSystemList.Add(movedUnitTurnIconTransform);
                movedUnitIcon.SetIconName(unit.GetCharacterName());
                movedUnitIcon.SetIcon(unit.GetUnitIcon());
                movedUnitIcon.ShadeStatus(true);
            }

            for (int i = 1; i < unitTurnSystemList.Count; i++)
            {
                unitTurnSystemList[i].SetAsFirstSibling();
            }
        }

        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            UpdateTurnSystemVisual();
        }

    }

}