namespace Nivandria.UI.Archive
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

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
