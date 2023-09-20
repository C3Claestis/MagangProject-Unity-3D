namespace Nivandria.UI.Archive
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Archive
    {
        [SerializeField] private string title;
        [SerializeField] private string description;

        public Archive(string title, string description)
        {
            this.title = title;
            this.description = description;

        }
        public string GetTitle() => title;
        public string GetDescription() => description;
    }
}
