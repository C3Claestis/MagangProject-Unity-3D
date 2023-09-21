namespace Nivandria.Battle.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitIconUI : MonoBehaviour
    {
        [SerializeField] Image image;

        public void ShadeStatus(bool status)
        {
            Color lightShade = new Color(0.9f, 0.9f, 0.9f, 1f);
            Color darkShade = new Color(0.4f, 0.4f, 0.4f, 1f);

            image.color = status ? darkShade : lightShade;
        }

        public void SetIcon(Sprite sprite) => image.sprite = sprite;
        public void SetIconName(string name) => gameObject.name = name;



    }

}