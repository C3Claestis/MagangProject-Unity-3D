namespace Nivandria.UI.Consumable
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    public class ConsumablesLog : MonoBehaviour
    {
        [SerializeField] Image iconConsumable;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI totalConsumable;

        private Consumables consumables;

        public Consumables GetConsumables() => consumables;

        public void SetConsumableDetail(Consumables consumables)
        {
            this.consumables = consumables;
            //iconKey = key.GetIcon();
            title.text = consumables.GetTitle();
            // descriptionKey.text = key.GetDescriptionKey();
            if (consumables.GetTotal() > 1)
            {
                totalConsumable.text = consumables.GetTotal().ToString();
            }
            else
            {
                totalConsumable.text = "";
            }
        }

        public void UpdateVisual()
        {
            bool status = ConsumablesLogManager.Instance.GetSelectedConsumableLog() == this;
            if (status)
            {
                title.fontStyle = FontStyles.Bold;
                SetImageAlpha(255);
                GetComponent<Button>().Select();
            }
            else
            {
                title.fontStyle = FontStyles.Normal;
                SetImageAlpha(0);

            }
        }

        public void SetSelected()
        {
            ConsumablesLogManager.Instance.SetSelectedConsumableLog(this);
            ConsumablesLogManager.Instance.UpdateVisualConsumableLog();
        }

        public void SetImageAlpha(float alpha)
        {
            Image image = GetComponent<Image>();
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

}