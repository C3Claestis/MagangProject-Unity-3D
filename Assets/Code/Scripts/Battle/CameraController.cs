namespace Nivandria.Battle
{
    using Cinemachine;
    using UnityEngine;
    using Nivandria.Battle.Grid;
    using System;

    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        [Header("Virtual Camera")]
        [SerializeField] CinemachineVirtualCamera verticalCamera;
        [SerializeField] CinemachineVirtualCamera horizontalCamera;
        [SerializeField] CinemachineVirtualCamera UpCamera;

        [Header("Speed")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float moveFocusStoppingDistance = .3f;
        [SerializeField] private float focusActiveMoveSpeed = 30f;

        private CinemachineTransposer cinemachineTransposer;
        private PlayerInputController playerInputController;

        private Vector3 targetPosition = new Vector3(0, 0, 0);
        private Vector3 targetFollowOffset;

        private float widthMoveLimit, heighMoveLimit;
        private float minZoomLimit = 5f;
        private float maxZoomLimit = 8f;
        private float zoomSmoothSpeed = 5f;
        private float zoomSpeed = 0.4f;

        private bool cameraFocusActive = false;
        private bool isActive = true;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one CameraController! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            widthMoveLimit = LevelGrid.Instance.GetGridWidth() * 2f - 1f;
            heighMoveLimit = LevelGrid.Instance.GetGridHeight() * 2 - 1f;

            InitializedVirtualCamera(UpCamera);
            playerInputController = PlayerInputController.Instance;

            // transform.position = new Vector3(widthMoveLimit / 2, 0, 1);
            targetFollowOffset.y = maxZoomLimit;

            playerInputController.OnActionMapChanged += PlayerInputController_OnActionMapChanged;
            playerInputController.OnInputControlChanged += PlayerInputController_OnInputControlChanged;
        }

        public void InitializedVirtualCamera(CinemachineVirtualCamera cinemachineVirtualCamera)
        {
            cinemachineVirtualCamera.enabled = true;
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            targetFollowOffset = cinemachineTransposer.m_FollowOffset;
            targetFollowOffset.y = maxZoomLimit;

            if (cinemachineVirtualCamera == verticalCamera)
            {
                horizontalCamera.enabled = false;
                UpCamera.enabled = false;

                minZoomLimit = 5;
                maxZoomLimit = 8;
            }
            else if (cinemachineVirtualCamera == horizontalCamera)
            {
                verticalCamera.enabled = false;
                UpCamera.enabled = false;

                minZoomLimit = 5;
                maxZoomLimit = 8;
            }
            else if (cinemachineVirtualCamera == UpCamera)
            {
                verticalCamera.enabled = false;
                horizontalCamera.enabled = false;

                minZoomLimit = 10;
                maxZoomLimit = 14;
            }
            else
            {
                Debug.LogError("Camera Not Identified!");
                return;
            }
        }


        void Update()
        {
            HandleCameraMoveToTarget();

            if (!isActive) return;

            HandleCameraMovement();
            HandleCameraZoom();
        }

        /// <summary>Handles camera movement based on player input.</summary>
        private void HandleCameraMovement()
        {
            if (cameraFocusActive) return;

            Vector2 cameraMovement = playerInputController.GetCameraMovementInputValue();
            Vector3 inputMoveDir = new Vector3(cameraMovement.x, 0, cameraMovement.y);
            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
            Vector3 newPosition = transform.position + moveVector * moveSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, -1f, widthMoveLimit); // Restrict x movement between -1 and 15
            newPosition.z = Mathf.Clamp(newPosition.z, -1f, heighMoveLimit); // Restrict z movement between -1 and 7

            transform.position = newPosition;
        }

        /// <summary>Handles camera zoom based on player input.</summary>
        private void HandleCameraZoom()
        {
            float cameraZoomValue = playerInputController.GetCameraZoomInputValue();
            targetFollowOffset.y += zoomSpeed * cameraZoomValue;
            targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minZoomLimit, maxZoomLimit);

            if (Vector3.Distance(cinemachineTransposer.m_FollowOffset, targetFollowOffset) > 0.01f)
            {
                cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSmoothSpeed);
            }
        }

        /// <summary>Handles the camera focusing to a specific position.</summary>
        private void HandleCameraMoveToTarget()
        {
            if (!cameraFocusActive) return;

            if (Vector3.Distance(transform.position, targetPosition) > moveFocusStoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * Time.deltaTime * focusActiveMoveSpeed;

                transform.position = Vector3.Lerp(transform.position, moveDirection, Time.deltaTime);
            }
            else
            {
                cameraFocusActive = false;
                transform.position = targetPosition;
            }
        }

        /// <summary> Sets the camera's focus to a specific position. </summary>
        /// <param name="targetPosition">The position to focus the camera on.</param>
        public void SetCameraFocusToPosition(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
            cameraFocusActive = true;
        }


        private void PlayerInputController_OnActionMapChanged(object sender, string actionMap)
        {
            if (actionMap == "Gridmap" || actionMap == "PlacingUnits")
            {
                SetActive(true);
            }
            else
            {
                SetActive(false);
            }
        }

        private void PlayerInputController_OnInputControlChanged(object sender, EventArgs e)
        {
            bool isCurrentControllerGamepad = playerInputController.IsCurrentControllerGamepad();

            if (isCurrentControllerGamepad) zoomSpeed = 0.15f;
            else zoomSpeed = 0.4f;
        }

        public void SetActive(bool status) => isActive = status;
        public CinemachineVirtualCamera GetVerticalCamera() => verticalCamera;
        public GridPosition GetCurrentGridPosition() => LevelGrid.Instance.GetGridPosition(transform.position);
    }
}