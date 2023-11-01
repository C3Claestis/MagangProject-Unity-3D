namespace Nivandria.UI.Consumable
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ConsumablesLogManager : MonoBehaviour
    {
        public static ConsumablesLogManager Instance { get; private set; }

        [Header("Content Container")]
        [SerializeField] Transform contentLeft;
        [SerializeField] Transform contentRight;

        [Header("Description")]
        [SerializeField] Image iconConsumable;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI status;

        [Header("Consumable Type")]
        [SerializeField] public ConsumableType consumableType;
        [SerializeField] ConsumableType currentConsumableType;


        [SerializeField] List<Consumables> consumablesList = new List<Consumables>();
        [SerializeField] GameObject consumableLog;

        private List<ConsumablesLog> consumableLogList;
        private ConsumablesLog selectedConsumableLog;

        public ConsumablesLog GetSelectedConsumableLog() => selectedConsumableLog;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            InitializeConsumableLogs();
        }

        void Update()
        {
            if (consumableType == currentConsumableType) return;
            currentConsumableType = consumableType;
            RemoveConsumableLog();
            InitializeConsumableLogs();
        }

        public void InitializeConsumableLogs()
        {
            GameObject newConsumable;
            consumableLogList = new List<ConsumablesLog>();

            int i = 0;
            foreach (Consumables consumable in consumablesList)
            {
                if (!(consumable.GetConsumableType() == consumableType)) continue;
                newConsumable = Instantiate(consumableLog, i % 2 == 1 ? contentRight : contentLeft);
                newConsumable.GetComponent<ConsumablesLog>().SetConsumableDetail(consumable);
                consumableLogList.Add(newConsumable.GetComponent<ConsumablesLog>());
                i++;
            }

            SetSelectedConsumableLog(consumableLogList[0]);
        }

        void RemoveConsumableLog()
        {
            foreach (Transform child in contentLeft)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in contentRight)
            {
                Destroy(child.gameObject);
            }
        }

        public void UpdateVisualConsumableLog()
        {
            foreach (ConsumablesLog consumables in consumableLogList)
            {
                consumables.UpdateVisual();
            }
        }

        public void SetSelectedConsumableLog(ConsumablesLog consumableLog)
        {
            selectedConsumableLog = consumableLog;
            title.text = selectedConsumableLog.GetConsumables().GetTitle();
            description.text = selectedConsumableLog.GetConsumables().GetDescription();
            status.text = selectedConsumableLog.GetConsumables().GetStatus();
            UpdateVisualConsumableLog();
        }
    }

}