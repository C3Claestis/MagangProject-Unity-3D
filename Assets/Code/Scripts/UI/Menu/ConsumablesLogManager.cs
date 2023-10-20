namespace Nivandria.UI.Consumable
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ConsumablesLogManager : MonoBehaviour
    {
        public static ConsumablesLogManager Instance{get; private set;}
        [SerializeField] Transform contentLeft;
        [SerializeField] Transform contentRight;
        [SerializeField] Image iconConsumable;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI status;


        [SerializeField] List<Consumables> consumablesList = new List<Consumables>();
        [SerializeField] GameObject consumableLog;

        private List<ConsumablesLog> consumableLogList;
        private ConsumablesLog selectedConsumableLog;

        public ConsumablesLog GetSelectedConsumableLog() => selectedConsumableLog;
        
        void Awake()
        {
            if(Instance != null && Instance != this)
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
            IntializeConsumableLogs();
        }

        void Update()
        {

        }

        public void IntializeConsumableLogs()
        {
            GameObject newConsumable;
            consumableLogList = new List<ConsumablesLog>();

            for (int i = 0; i < consumablesList.Count; i++)
            {
                if (i/2 == 1)
                {
                    newConsumable = Instantiate(consumableLog, contentRight);
                    newConsumable.GetComponent<ConsumablesLog>().SetConsumableDetail(consumablesList[i]);
                }
                else
                {
                    newConsumable = Instantiate(consumableLog, contentLeft);
                    newConsumable.GetComponent<ConsumablesLog>().SetConsumableDetail(consumablesList[i]);
                }

                consumableLogList.Add(newConsumable.GetComponent<ConsumablesLog>());

                //Button keyButton = newKey.GetComponent<Button>();
                
            }

            SetSelectedConsumableLog(consumableLogList[0]);
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