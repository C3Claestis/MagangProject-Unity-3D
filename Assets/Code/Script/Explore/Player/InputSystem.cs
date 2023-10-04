namespace Nivandria.Explore
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Handles player input and character control in the game.
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        private static InputSystem instance;

        [SerializeField] ExploreManager exploreManager;
        [SerializeField] GameObject[] objekPlayer;
        [SerializeField] private InteraksiNPC interaksiNPC;
        [SerializeField] PlayerInput playerInput;
        [SerializeField] DialogueManager dialogueManager;

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

        public static InputSystem GetInstance()
        {
            return instance;
        }
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
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

        /// <summary>
        /// Handle Fungsi Run Untuk Stamina
        /// </summary>
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

            // Regenerate stamina if not running
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
        /// Spawns the 3D character.
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
        /// Handles running using Left Shift.
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
        }

        /// <summary>
        /// Handles movement using WASD.
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
        /// Handles interaction using F.
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

                TriggerScene[] trigger = FindObjectsOfType<TriggerScene>();

                foreach (TriggerScene sceneTrigger in trigger)
                {
                    if (sceneTrigger.GetIsScene())
                    {
                        sceneTrigger.Trigger();
                    }
                }
            }

            if (interaksiNPC.GetIsNPC() == true)
            {
                if (context.performed && interaksiNPC.GetIsTalk() == false)
                {
                    interaksiNPC.SetIsTalk(true);
                    playerInput.SwitchCurrentActionMap("UI");
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRun", false);
                    isSetup = false;
                }
            }
        }

        public void SetAfterDialogue()
        {
            interaksiNPC.SetIsTalk(false);
            playerInput.SwitchCurrentActionMap("Player");
            isSetup = true;
        }

        public void Dialogue(InputAction.CallbackContext context)
        {
            if (context.performed && dialogueManager.GetPlaying() && !dialogueManager.GetPilih() && dialogueManager.GetContinueLine())
            {
                dialogueManager.ContinueStory();
            }
        }

        /// <summary>
        /// Enters the Party Setup UI using J.
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
