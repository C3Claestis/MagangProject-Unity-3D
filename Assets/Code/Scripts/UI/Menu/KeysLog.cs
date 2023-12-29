namespace Nivandria.UI.Keys
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class KeysLog : MonoBehaviour
    {
        [SerializeField] Image iconKey;
        [SerializeField] TextMeshProUGUI nameKey;
        [SerializeField] TextMeshProUGUI totalKey;

        private Keys key;
        public Keys GetKeys() => key;

        public void SetKeyDetail(Keys key)
        {
            this.key = key;
            //iconKey = key.GetIcon();
            nameKey.text = key.GetNameKey();
            // descriptionKey.text = key.GetDescriptionKey();
            if (key.GetTotalKey() > 1)
            {
                totalKey.text = key.GetTotalKey().ToString();
            }
            else
            {
                totalKey.text = "";
            }
        }

        public void UpdateVisual()
        {
            bool status = KeysLogManager.Instance.GetSelectedKeyLog() == this;
            CanvasGroup canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
            if (status == true)
            {
                nameKey.fontStyle = FontStyles.Bold;
                canvasGroup.alpha = 1;
                GetComponent<Button>().Select();
            }
            else
            {
                nameKey.fontStyle = FontStyles.Normal;
                canvasGroup.alpha = 0;

            }
        }

        public void SetSelected()
        {
            KeysLogManager.Instance.SetSelectedKeyLog(this);
            KeysLogManager.Instance.UpdateVisualKeyLog();
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