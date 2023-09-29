namespace Nivandria.UI.Gears
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GearsLog : MonoBehaviour
    {
        [SerializeField] private Image iconGear;
        [SerializeField] private TextMeshProUGUI gearName;

        public void SetNameArchiveLog(string titleName)
        {
            gearName.text = titleName;
        }

        public void SetColorText()
        {
            
        }
    }

}