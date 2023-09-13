namespace Nivandria.UI.Keys
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class KeysLog : MonoBehaviour
    {
        [SerializeField] Image iconKey;
        [SerializeField] TextMeshProUGUI nameKey;
        //[SerializeField] TextMeshProUGUI descriptionKey;
        [SerializeField] TextMeshProUGUI totalKey;

        private Keys key;

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
            if (status)
            {
                nameKey.fontStyle = FontStyles.Bold;
                GetComponent<Outline>().enabled = true;
                GetComponent<Button>().Select();
            }
            else
            {
                nameKey.fontStyle = FontStyles.Normal;
                // tambahin off outline
                GetComponent<Outline>().enabled = false;
                
            }
        }

        public void SetSelected()
        {
            KeysLogManager.Instance.SetSelectedKeyLog(this);
            KeysLogManager.Instance.UpdateVisualKeyLog();

        }

        public Keys GetKeys() => key;

    }

}