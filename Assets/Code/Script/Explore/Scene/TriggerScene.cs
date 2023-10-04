namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    public class TriggerScene : MonoBehaviour
    {
        [SerializeField] int indexScene;
        [SerializeField] int indexPosisi;
        private bool isCanScene = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isCanScene)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {                    
                    Invoke("TukarNilai", 0.5f);
                }
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