namespace Nivandria.UI.Keys
{
    using System;
    using Microsoft.Unity.VisualStudio.Editor;
    using UnityEngine;

    [Serializable]
    public class Keys
    {
        [SerializeField] private Image iconKey;
        [SerializeField] private string nameKey;
        [SerializeField] private string descriptionKey;
        [SerializeField] private int totalKey;

        public Keys(Image iconKey, string nameKey, string descriptionKey,int totalsKey)
        {
            this.iconKey = iconKey;
            this.nameKey = nameKey;
            this.descriptionKey = descriptionKey;
            this.totalKey = totalsKey;
        }

        public Image GetIcon() => iconKey;
        public string GetNameKey() => nameKey;
        public string GetDescriptionKey() => descriptionKey;
        public int GetTotalKey() => totalKey;
    }

}