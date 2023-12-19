namespace Nivandria.UI.Menu
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]

    public class Menu
    {
        [SerializeField] private string NameButton;
        [SerializeField] private Sprite IconLeftButton;
        [SerializeField] private Sprite IconRightButton;

        public Menu (string NameButton, Sprite IconLeftButton, Sprite IconRightButton)
        {
            this.NameButton = NameButton;
            this.IconLeftButton = IconLeftButton;
            this.IconRightButton = IconRightButton;
        }

        public string GetNameButton() => NameButton;
        public Sprite GetIconLeftButton() => IconLeftButton;
        public Sprite GetIconRightButton() => IconRightButton;
    }
}