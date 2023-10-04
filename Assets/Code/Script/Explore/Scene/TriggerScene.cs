namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;    
    using UnityEngine;
    using UnityEngine.SceneManagement;
    public class TriggerScene : MonoBehaviour
    {
        [SerializeField] int indexScene;
        [SerializeField] int indexPosisi;
        [SerializeField] Animator transisi;
        private bool isCanScene = false;   
        public bool GetIsScene() => isCanScene;

        public void Trigger()
        {
            if (isCanScene)
            {
                transisi.SetTrigger("In");
                Invoke("TukarNilai", 1f);
            }
        }
        void TukarNilai()
        {
            ExploreManager.set_lastScene = indexPosisi;
            SceneManager.LoadScene(indexScene);
        }
        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                isCanScene = true;
            }
        }
        void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                isCanScene = false;
            }
        }
    }
}