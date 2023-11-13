namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Ink.Runtime;
    using UnityEngine.UI;
    public class InkExternal : MonoBehaviour
    {
        [Header("Image All Sprite")]
        [SerializeField] Sprite[] image_dialog;
        [Header("Image Left")]
        [SerializeField] Image avatar_1;
        [Header("Image Right")]
        [SerializeField] Image avatar_2;
        private int ParseAvatar;
        
        public void Bind(Story story)
        {
            story.BindExternalFunction("playavatar", (string avatarvalue) =>
                {
                    ParseAvatar = int.Parse(avatarvalue);
                });

            story.BindExternalFunction("nameavatar", (string namevalue) =>
            {
                switch (namevalue)
                {
                    case "Sacra":
                        avatar_1.sprite = image_dialog[0];
                        avatar_1.color = new Color32(255, 255, 255, 255);
                        avatar_2.color = new Color32(100, 100, 100, 255);
                        break;
                    case "Vana":
                        avatar_1.sprite = image_dialog[1];
                        avatar_1.color = new Color32(255, 255, 255, 255);
                        avatar_2.color = new Color32(100, 100, 100, 255);
                        break;
                    case "Eldria":
                        avatar_2.sprite = image_dialog[2];
                        avatar_2.color = new Color32(255, 255, 255, 255);
                        avatar_1.color = new Color32(100, 100, 100, 255);
                        break;
                    case "Boar":
                        avatar_2.sprite = image_dialog[3];
                        avatar_2.color = new Color32(255, 255, 255, 255);
                        avatar_1.color = new Color32(100, 100, 100, 255);
                        break;
                }                
            });
        }

        public void Unbind(Story story)
        {
            story.UnbindExternalFunction("playavatar");
            story.UnbindExternalFunction("nameavatar");
        }        

        public int GetAvatar() => ParseAvatar;
    }
}