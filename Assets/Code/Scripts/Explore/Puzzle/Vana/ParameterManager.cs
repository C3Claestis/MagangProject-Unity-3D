namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class ParameterManager : MonoBehaviour
    {
        [SerializeField] GameObject panelManager;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] BoxMove player;
        private Vector2[] scaleOptions = new Vector2[3]; // Tiga pilihan skala acak
        private int currentScaleOption = 0;
        private float transitionSpeed = 1.0f; // Kecepatan transisi skala
        private Vector3 targetScale; // Skala yang sedang menjadi target transisi
        private bool isHearth = true;
        private void Start()
        {
            // Inisialisasi tiga pilihan skala acak
            scaleOptions[0] = new Vector3(0.1f, 0.1f); // Skala asli
            scaleOptions[1] = new Vector3(1.2f, 1.2f); // Skala setengah
            scaleOptions[2] = new Vector3(1.5f, 1.5f); // Skala dua kali lipat

            // Mulai dengan skala asli
            targetScale = scaleOptions[0];            
        }

        private void Update()
        {           
            if (isHearth) { ScaleTransform(); }            
        }
        public void InputKondisi(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (rectTransform.localScale.x > 1.0f && rectTransform.localScale.x < 1.185f &&
                     rectTransform.localScale.y > 1.0f && rectTransform.localScale.y < 1.18f)
                {
                    isHearth = false;                    
                    Invoke("DisablePanelManager", 2f);
                    player.isDestroy = true;
                    Debug.Log("BERHASIL");
                }
                else
                {
                    DisablePanelManager();
                    Debug.Log("TIDAK BERHASIL");
                }
            }           
        }
        void ScaleTransform()
        {
            // Lerp (Linear Interpolate) antara skala saat ini dan target skala
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, Time.deltaTime * transitionSpeed);

            // Jika skala mendekati target skala yang cukup dekat, pilih target skala baru secara acak
            if (Vector3.Distance(rectTransform.localScale, targetScale) < 0.01f)
            {
                // Pilih salah satu dari tiga pilihan skala secara acak
                currentScaleOption = Random.Range(0, 3);

                // Tetapkan target skala baru
                targetScale = scaleOptions[currentScaleOption];
            }
        }
        private void DisablePanelManager()
        {
            // Menonaktifkan objek panelManager setelah waktu jeda
            panelManager.SetActive(false);
            isHearth = true;

            // Mulai dengan skala asli
            targetScale = scaleOptions[0];
        }
    }
}