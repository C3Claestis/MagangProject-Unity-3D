namespace Nivandria.Explore
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class InputSystem : MonoBehaviour
    {
        [SerializeField] ExploreManager exploreManager;
        [SerializeField] GameObject[] objekPlayer;
        [SerializeField] private InteraksiNPC interaksiNPC;
        [SerializeField] PlayerInput playerInput;
        [SerializeField] GameObject _cameraMain, _cameraTalk;
        private Animator animator;
        private Vector2 movementValue;
        private bool isMoving = false;
        private bool canRunning = false;
        private bool isSpawn = false;
        private bool isSetup = true;

        [SerializeField] RectTransform staminaBarRectTransform;     
        private float initialStaminaBarWidth;   
        private float maxStamina = 50f;
        private float currentStamina;
        private float staminaRegenRate = 10f;
        private float staminaCostPerRun = 5f;
        private void Start()
        {
            currentStamina = maxStamina;
            initialStaminaBarWidth = staminaBarRectTransform.sizeDelta.x;
            GameObject newPlayer = Instantiate(objekPlayer[exploreManager.GetLead()], transform);
        }
        private void Update()
        {
            SpawnKarakter();
            HandleRun();
        }
        void HandleRun()
        {
            if (canRunning && isMoving && currentStamina >= staminaCostPerRun)
            {
                SetAnimatorRunning(true);
                currentStamina -= staminaCostPerRun * Time.deltaTime;
                UpdateStaminaBar();
            }
            else
            {
                canRunning = false;
                SetAnimatorRunning(false);
            }

            if (!canRunning && isMoving)
            {
                SetAnimatorWalking(true);
            }

            // Regenerasi stamina jika tidak berlari
            if (!canRunning && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
                UpdateStaminaBar();
            }
        }
        private void UpdateStaminaBar()
        {
            float fillAmount = currentStamina / maxStamina;
            staminaBarRectTransform.sizeDelta = new Vector2(fillAmount * initialStaminaBarWidth, staminaBarRectTransform.sizeDelta.y);
        }

        /// <summary>
        /// Fungsi untuk memanggil 3D karakter
        /// </summary>
        void SpawnKarakter()
        {
            if (animator == null)
            {
                animator = GameObject.FindGameObjectWithTag("Karakter").GetComponent<Animator>();
            }
            if (isSpawn)
            {
                Destroy(transform.GetChild(3).gameObject);
                GameObject newPlayer = Instantiate(objekPlayer[exploreManager.GetLead()], transform);
                isSpawn = false;
            }
        }

        /// <summary>
        /// Untuk Lari Menggunakan Left Shift
        /// </summary>
        /// <param name="context"></param>
        public void RunAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                canRunning = true;
            }

            if (context.canceled)
            {
                canRunning = false;
            }
            /*
            if (context.performed)
            {
                canRunning = true;

                if (isMoving)
                {
                    SetAnimatorRunning(true);
                }
            }

            if (context.canceled)
            {
                canRunning = false;
                SetAnimatorRunning(false);

                if (isMoving)
                {
                    SetAnimatorWalking(true);
                }
            }*/
        }

        /// <summary>
        /// Untuk Bergerak Sesuai WASD
        /// </summary>
        /// <param name="context"></param>
        public void MovingAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementValue = context.ReadValue<Vector2>();
                isMoving = true;

                if (canRunning)
                {
                    SetAnimatorRunning(true);
                }
                else
                {
                    SetAnimatorWalking(true);
                }
            }

            if (context.canceled)
            {
                movementValue = context.ReadValue<Vector2>();
                isMoving = false;
                SetAnimatorRunning(false);
                SetAnimatorWalking(false);
            }
        }

        /// <summary>
        /// Untuk Interaksi Menggunakan F
        /// </summary>
        /// <param name="context"></param>
        public void InteractionAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Chest[] chest = FindObjectsOfType<Chest>();

                foreach (Chest ces in chest)
                {
                    if (ces.GetOpen() == false && ces.GetIsPlayer() == true)
                    {
                        ces.SetOpenChest(true);
                    }
                }
            }

            if (interaksiNPC.GetIsNPC() == true)
            {
                if (context.performed && interaksiNPC.GetIsTalk() == false)
                {
                    interaksiNPC.SetIsTalk(true);
                    playerInput.SwitchCurrentActionMap("UI");
                    interaksiNPC.interaksi.text = "Sedang Interaksi";
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRun", false);
                    _cameraTalk.SetActive(true);
                    _cameraMain.SetActive(false);
                    isSetup = false;
                }
                else if (context.performed && interaksiNPC.GetIsTalk() == true)
                {
                    interaksiNPC.interaksi.text = "";
                    interaksiNPC.SetIsTalk(false);
                    playerInput.SwitchCurrentActionMap("Player");
                    _cameraTalk.SetActive(false);
                    _cameraMain.SetActive(true);
                    isSetup = true;
                }
            }
        }

        /// <summary>
        /// Untuk Masuk Panel UI Party Setup Menggunakan J
        /// </summary>
        /// <param name="context"></param>
        public void PartySetup(InputAction.CallbackContext context)
        {
            if (context.performed && isSetup)
            {
                exploreManager.PanelParty();
                playerInput.SwitchCurrentActionMap("UI");
            }
        }

        //////////////////////////////
        private void SetAnimatorRunning(bool value)
        {
            if (interaksiNPC.GetIsTalk() == false)
                animator.SetBool("isRun", value);
        }
        private void SetAnimatorWalking(bool value)
        {
            if (interaksiNPC.GetIsTalk() == false)
                animator.SetBool("isWalking", value);
        }
        public Vector2 GetMovementValue() => movementValue;
        public bool CanRunning() => canRunning;
        public bool SetIsSpawn(bool spawn) => this.isSpawn = spawn;
    }
}
