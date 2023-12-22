namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Ink.Runtime;
    using UnityEngine.UI;
    using Unity.VisualScripting;
    using Nivandria.Quest;

    public class InkExternal : MonoBehaviour
    {
        [Header("Sprite Sacra")]
        [SerializeField] Sprite[] sacra_sprite;
        [Header("Sprite Vana")]
        [SerializeField] Sprite[] vana_sprite;
        [Header("Sprite Eldria")]
        [SerializeField] Sprite[] eldria_sprite;
        [Header("Image Left")]
        [SerializeField] Image avatar_1;
        [Header("Image Right")]
        [SerializeField] Image avatar_2;
        private int ParseAvatar;
        private int ParseComplete;
        private byte indexPlayer = 0;
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
                        indexPlayer = 1;
                        break;
                    case "Vana":
                        indexPlayer = 2;
                        break;
                    case "Eldria":
                        indexPlayer = 3;
                        break;
                }
            });
            story.BindExternalFunction("ekspresiavatar", (string eksvalue) =>
                        {
                            switch (indexPlayer)
                            {
                                case 1:
                                    if (eksvalue == "Default")
                                    {
                                        avatar_1.sprite = sacra_sprite[0];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Smile")
                                    {
                                        avatar_1.sprite = sacra_sprite[1];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Speak")
                                    {
                                        avatar_1.sprite = sacra_sprite[2];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Angry")
                                    {
                                        avatar_1.sprite = sacra_sprite[3];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    break;
                                case 2:
                                    if (eksvalue == "Default")
                                    {
                                        avatar_1.sprite = vana_sprite[0];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Smile")
                                    {
                                        avatar_1.sprite = vana_sprite[1];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Speak")
                                    {
                                        avatar_1.sprite = vana_sprite[2];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Angry")
                                    {
                                        avatar_1.sprite = vana_sprite[3];
                                        avatar_1.color = new Color32(255, 255, 255, 255);
                                        avatar_2.color = new Color32(100, 100, 100, 255);
                                    }
                                    break;
                                case 3:
                                    if (eksvalue == "Default")
                                    {
                                        avatar_2.sprite = eldria_sprite[0];
                                        avatar_2.color = new Color32(255, 255, 255, 255);
                                        avatar_1.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Smile")
                                    {
                                        avatar_2.sprite = eldria_sprite[1];
                                        avatar_2.color = new Color32(255, 255, 255, 255);
                                        avatar_1.color = new Color32(100, 100, 100, 255);
                                    }
                                    else if (eksvalue == "Speak")
                                    {
                                        avatar_2.sprite = eldria_sprite[2];
                                        avatar_2.color = new Color32(255, 255, 255, 255);
                                        avatar_1.color = new Color32(100, 100, 100, 255);
                                    }
                                    break;
                            }
                        });
            story.BindExternalFunction("complete", (string compvalue) =>
            {
                ParseComplete = int.Parse(compvalue);

                switch (ParseComplete)
                {
                    case 1:
                        HandleQuest.GetInstance().Mision1 = true;
                        break;
                    case 2:
                        HandleQuest.GetInstance().Mision2 = true;
                        break;
                }
            });
        }

        public void Unbind(Story story)
        {
            story.UnbindExternalFunction("playavatar");
            story.UnbindExternalFunction("nameavatar");
            story.UnbindExternalFunction("ekspresiavatar");
            story.UnbindExternalFunction("complete");
        }

        public int GetAvatar() => ParseAvatar;
    }
}