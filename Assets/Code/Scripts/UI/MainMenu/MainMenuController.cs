namespace Nivandria.UI.MainMenu
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MainMenuController : MonoBehaviour
    {
        [Header("Panel Main Menu")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject loadGame;
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject exit;
        [SerializeField] private GameObject loadingScreen;
        private string nextSceneName;
        void Start()
        {
            loadGame.SetActive(false);
            settings.SetActive(false);
            exit.SetActive(false);
            loadingScreen.SetActive(false);
        }

        public void ShowLoadGamePanel()
        {
            mainMenu.SetActive(false);
            loadGame.SetActive(true);
        }

        // Metode untuk menampilkan panel Settings.
        public void ShowSettingsPanel()
        {
            mainMenu.SetActive(false);
            settings.SetActive(true);
        }

        // Metode untuk menampilkan panel Exit.
        public void ShowExitPanel()
        {
            exit.SetActive(true);
        }

        // Metode untuk kembali ke Main Menu dari panel lainnya.
        public void BackToMainMenu()
        {
            mainMenu.SetActive(true);
            loadGame.SetActive(false);
            settings.SetActive(false);
            exit.SetActive(false);
        }

        // Contoh metode untuk memulai permainan baru (misalnya, tombol "Start Game").
        public void StartNewGame()
        {
            // Simpan nama scene berikutnya.
            nextSceneName = "exploretest_UI";

            // Aktifkan loading screen dan mulai coroutine untuk menunggu 5 detik.
            loadingScreen.SetActive(true);
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