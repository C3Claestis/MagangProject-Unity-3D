namespace Nivandria.Explore
{
    using UnityEngine;

    public class PanelClose : MonoBehaviour
    {
        [SerializeField] GameObject panel;        

        public void Tutup()
        {
            panel.SetActive(false);
        }
    }
}