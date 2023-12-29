namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    public class TriggerScene : MonoBehaviour
    {
        [SerializeField] int indexScene;
        [SerializeField] Animator transisi;

        [Header("Name Of Object")]
        [SerializeField] string Name;

        [Header("Display Taking On Canvas")]
        [SerializeField] GameObject display_taking;
        private GameObject taking;        
        private bool isTaking = false;
        private bool isCanScene = false;
        public bool GetIsScene() => isCanScene;

        public void Trigger()
        {
            if (isCanScene)
            {
                transisi.SetTrigger("In");
                Invoke(nameof(TukarNilai), 1f);
            }
        }
        void TukarNilai()
        {
            SceneManager.LoadScene(indexScene);
        }
        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                isCanScene = true;
                GameObject content = GameObject.Find("Content Taking").gameObject;
                taking = Instantiate(display_taking);
                taking.transform.SetParent(content.transform);
                // Dapatkan komponen Text dari child objek
                Text textComponent = taking.transform.Find("Name").GetComponent<Text>();

                // Ganti teks dengan nama yang sesuai
                if (textComponent != null)
                {
                    textComponent.text = Name; // Ganti dengan cara Anda mendapatkan nama yang sesuai
                }                
            }
        }
        void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                isCanScene = false;
                // Hancurkan objek yang di-instantiate saat pemain keluar dari trigger
                if (taking != null)
                {
                    Destroy(taking);
                }
            }
        }
    }
}