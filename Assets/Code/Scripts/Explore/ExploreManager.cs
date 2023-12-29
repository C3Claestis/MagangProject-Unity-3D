namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Database;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;
    using Cinemachine;
    using UnityEngine.SceneManagement;
    using Nivandria.UI.Menu;

    public class ExploreManager : MonoBehaviour
    {
        [Header("Input System Player")]
        [SerializeField] InputSystem inputSystem;
        [SerializeField] PlayerInput playerInput;

        [Header("Component After Quest")]
        [SerializeField] GameObject boar_traininghround;

        [Header("Scene Transisi")]        
        [SerializeField] Animator transisi;
        
        [Header("Pause Menu")]
        [SerializeField] public Transform PanelMenu;

        private bool isPause = false;

        #region Get And Set
        private static ExploreManager instance;
        private int set_lead;
        public static int set_lastScene;
        public int GetLead() => set_lead;
        public void SetIsPause(bool isPausing) => isPause = isPausing;
        public bool GetIsPause() => isPause;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
            SaveSystem.Init();
        }
        public static ExploreManager GetInstance()
        {
            return instance;
        }
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            transisi.SetTrigger("Out");           

            if(PlayerPrefs.GetInt("Quest") > 1 && SceneManager.GetActiveScene().buildIndex == 3){
                boar_traininghround.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !isPause){
                MenuManager.Instance.ButtonOnClickOpenMenu(PanelMenu, true);
                Debug.Log("INSERT");
            }
            
        }
        
    }
}