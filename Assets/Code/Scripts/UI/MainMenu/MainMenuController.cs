namespace Nivandria.UI.MainMenu
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MainMenuController : MonoBehaviour
    {
        [Header("List Button Container")]
        [SerializeField] Transform listButton;

        [Header("Panel Game Object Main Menu")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject loadGame;
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject exit;
        [SerializeField] private GameObject loadingScreen;

        [Header("Panel Transform Main Menu")]
        [SerializeField] private Transform MainMenu;
        [SerializeField] private Transform LoadGame;
        [SerializeField] private Transform Settings;
        [SerializeField] private Transform Exit;
        [SerializeField] private Transform LoadingScreen;
        private string nextSceneName;
        void Start()
        {
            SetPanelMainMenuFirst();
        }

        public void SetPanelMainMenuFirst()
        {
            ButtonOnClick(MainMenu, true);
            ButtonOnClick(LoadGame, false);
            ButtonOnClick(Settings, false);
            ButtonOnClick(Exit, false);
        }

        public void ShowPanelMainMenu()
        {
            ButtonOnClick(MainMenu, true);
            ButtonOnClick(LoadGame, false);
            ButtonOnClick(Settings, false);
            ButtonOnClick(Exit, false);
        }

        public void ShowPanelLoadGame()
        {
            ButtonOnClick(MainMenu, false);
            ButtonOnClick(LoadGame, true);
            ButtonOnClick(Settings, false);
            ButtonOnClick(Exit, false);
        }

        public void ShowPanelSetting()
        {
            ButtonOnClick(MainMenu, false);
            ButtonOnClick(LoadGame, false);
            ButtonOnClick(Settings, true);
            ButtonOnClick(Exit, false);
        }

        public void ShowPanelExit()
        {
            ButtonOnClick(MainMenu, false);
            ButtonOnClick(LoadGame, false);
            ButtonOnClick(Settings, false);
            ButtonOnClick(Exit, true);
        }


        public void ButtonOnClick(Transform panel, bool status)
        {
            CanvasGroup canvasGroup = panel.transform.GetComponent<CanvasGroup>();
            canvasGroup.alpha = status? 1 : 0;
            canvasGroup.interactable = status;
            canvasGroup.blocksRaycasts = status;
        }

        // Contoh metode untuk memulai permainan baru (misalnya, tombol "Start Game").
        public void StartNewGame()
        {
            // Simpan nama scene berikutnya.
            nextSceneName = "Menu";

            // Aktifkan loading screen dan mulai coroutine untuk menunggu 5 detik.
            ButtonOnClick(LoadingScreen, true);
            StartCoroutine(LoadNextSceneAfterDelay(2f));
        }

        public void QuitGame()
        {
            // Keluar dari aplikasi (hanya berfungsi saat build standalone).
            Application.Quit();
            Debug.Log("Exit");
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private IEnumerator LoadNextSceneAfterDelay(float delay)
        {
            // Tunggu selama 'delay' detik.
            yield return new WaitForSeconds(delay);

            // Pindah ke scene berikutnya setelah menunggu.
            SceneManager.LoadScene(nextSceneName);
        }

    }

}