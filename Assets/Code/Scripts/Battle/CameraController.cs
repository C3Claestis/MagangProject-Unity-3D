namespace Nivandria.Battle
{
    using Cinemachine;
    using UnityEngine;
    using Nivandria.Battle.Grid;

    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private float cameraMoveSpeed = 10f;
        [SerializeField] private float cameraZoomSpeed = 3f;
        [SerializeField] private float cameraFocusStoppingDistance = .3f;
        [SerializeField] private float cameraFocusActiveMoveSpeed = 30f;

        private CinemachineTransposer cinemachineTransposer;
        private PlayerInputController playerInputController;

        private Vector3 targetPosition = new Vector3(0, 0, 0);
        private Vector3 targetFollowOffset;

        private float widthMoveLimit, heighMoveLimit;
        private float minZoomLimit = 2f;
        private float maxZoomLimit = 7f;
        private float zoomAmmount = 0.8f;
        private bool cameraFocusActive = false;

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

            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            targetFollowOffset = cinemachineTransposer.m_FollowOffset;
            playerInputController = PlayerInputController.Instance;

            transform.position = new Vector3(widthMoveLimit / 2, 0, 1);
            targetFollowOffset.y = maxZoomLimit;
        }

        void Update()
        {
            HandleCameraMovement();
            HandleCameraZoom();
            HandleCameraFocusToPosition();
        }

        /// <summary>Handles camera movement based on player input.</summary>
        private void HandleCameraMovement()
        {
            if (!playerInputController.GetCameraMovementStatus()) return;

            Vector2 cameraMovement = playerInputController.GetCameraMovementValue();
            Vector3 inputMoveDir = new Vector3(cameraMovement.x, 0, cameraMovement.y);
            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
            Vector3 newPosition = transform.position + moveVector * cameraMoveSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, -1f, widthMoveLimit); // Restrict x movement between -1 and 15
            newPosition.z = Mathf.Clamp(newPosition.z, -1f, heighMoveLimit); // Restrict z movement between -1 and 7

            transform.position = newPosition;
        }

        /// <summary>Handles camera zoom based on player input.</summary>
        private void HandleCameraZoom()
        {
            if (!playerInputController.GetCameraZoomStatus()) return;

            float cameraZoomValue = playerInputController.GetCameraZoomValue();

            if (cameraZoomValue > 0)
            {
                targetFollowOffset.y -= zoomAmmount;
            }

            if (cameraZoomValue < 0)
            {
                targetFollowOffset.y += zoomAmmount;
            }

            targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minZoomLimit, maxZoomLimit);
            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * cameraZoomSpeed);
        }

        /// <summary>Handles the camera focusing to a specific position.</summary>
        private void HandleCameraFocusToPosition()
        {
            if (!cameraFocusActive) return;
            
            

            if (Vector3.Distance(transform.position, targetPosition) > cameraFocusStoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * Time.deltaTime * cameraFocusActiveMoveSpeed;

                transform.position = Vector3.Lerp(transform.position, moveDirection, Time.deltaTime);
            }
            else {
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
    }
}