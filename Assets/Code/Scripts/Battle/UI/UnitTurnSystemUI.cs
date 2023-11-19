namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using TMPro;
    using UnityEngine;

    public class UnitTurnSystemUI : MonoBehaviour
    {
        public static UnitTurnSystemUI Instance { get; private set; }

        [SerializeField] Transform UnitIconPrefab;
        [SerializeField] Transform waitingListContainer;
        [SerializeField] Transform nowTurnContainer;
        [SerializeField] TextMeshProUGUI turnText;

        [Header("Screen Card")]
        [SerializeField] private CanvasGroup openingCard;
        [SerializeField] private CanvasGroup roundCard;
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private CanvasGroup winCard;
        [SerializeField] private CanvasGroup gameoverCard;

        private List<Transform> unitTurnSystemList;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitTurnSystemUI! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

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

            UnitTurnSystem.Instance.OnUnitListChanged += UnitTurnSystem_OnUnitListChanged;

            UpdateTurnSystemVisual();
        }

        public void UpdateTurnSystemVisual()
        {
            List<Unit> waitingUnitList = UnitTurnSystem.Instance.GetWaitingUnitList();
            turnText.enabled = true;

            foreach (Transform iconTransform in unitTurnSystemList)
            {
                Destroy(iconTransform.gameObject);
            }

            unitTurnSystemList.Clear();

            if (waitingUnitList.Count == 0)
            {
                turnText.enabled = false;
                return;
            }

            Unit nextUnit = waitingUnitList[0];

            if (nextUnit == null) return;
            Transform nowUnitTurnIconTransform = Instantiate(UnitIconPrefab, nowTurnContainer);
            UnitIconUI selectedUnitIcon = nowUnitTurnIconTransform.GetComponent<UnitIconUI>();
            unitTurnSystemList.Add(nowUnitTurnIconTransform);
            selectedUnitIcon.SetIconName(nextUnit.GetCharacterName());
            selectedUnitIcon.SetIcon(nextUnit.GetUnitIcon());

            for (int i = 1; i < waitingUnitList.Count; i++)
            {
                Unit unit = waitingUnitList[i];

                if (unit == null) break;

                Transform waitingUnitTurnIconTransform = Instantiate(UnitIconPrefab, waitingListContainer);
                UnitIconUI waitingUnitIcon = waitingUnitTurnIconTransform.GetComponent<UnitIconUI>();

                unitTurnSystemList.Add(waitingUnitTurnIconTransform);
                waitingUnitIcon.SetIconName(unit.GetCharacterName());
                waitingUnitIcon.SetIcon(unit.GetUnitIcon());
            }


            for (int i = 1; i < unitTurnSystemList.Count; i++)
            {
                unitTurnSystemList[i].SetAsFirstSibling();
            }
        }

        public void ShowOpeningCard(bool show)
        {
            openingCard.alpha = show ? 1 : 0;
            openingCard.interactable = show;
            openingCard.blocksRaycasts = show;
        }

        public void ShowRoundCard(bool show)
        {
            roundCard.alpha = show ? 1 : 0;
            roundCard.interactable = show;
            roundCard.blocksRaycasts = show;

            roundText.text = "ROUNDS " + UnitTurnSystem.Instance.GetTurnNumber().ToString();
        }

        public void ShowWinCard(bool show)
        {
            winCard.alpha = show ? 1 : 0;
            winCard.interactable = show;
            winCard.blocksRaycasts = show;
        }

        public void ShowEndingCard(bool show)
        {
            gameoverCard.alpha = show ? 1 : 0;
            gameoverCard.interactable = show;
            gameoverCard.blocksRaycasts = show;
        }

        private void UnitTurnSystem_OnUnitListChanged(object sender, EventArgs e)
        {
            UpdateTurnSystemVisual();
        }

    }

}