namespace WorldMap
{
    using Cinemachine;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {

        public static CameraController Instance { get; private set; }

        [SerializeField] private float cameraMoveSpeed = 50f;
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private float zoomMultiplier = 0.1f;
        private float movementOffset = 2f;
        private float zoomTarget;
        private float minZoom = 0.5f;
        private float maxZoom = 2.4f;
        private float velocity = 0f;

        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] Camera mainCamera;
        private PlayerInputController playerInputController;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one CameraController! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;

            playerInputController = GetComponent<PlayerInputController>();
            zoomTarget = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        }

        private void Update()
        {
            HandleCameraMovement();
            HandleCameraZoom();
        }

        /// <summary>Handles camera movement based on player input.
        /// </summary>
        private void HandleCameraMovement()
        {
            if (!playerInputController.GetCameraMovementStatus()) return;

            Vector2 cameraMovement = playerInputController.GetCameraMovementValue();
            Vector3 inputMoveDir = new Vector3(cameraMovement.x, cameraMovement.y, 0);
            Vector3 newPosition = transform.position + inputMoveDir * cameraMoveSpeed * Time.deltaTime * cinemachineVirtualCamera.m_Lens.OrthographicSize;
            Vector3 offset = newPosition - mainCamera.transform.position;

            float distance = offset.magnitude;

            if (distance > movementOffset)
            {
                Vector3 clampedPosition = mainCamera.transform.position + offset.normalized * movementOffset;
                newPosition = new Vector3(clampedPosition.x, clampedPosition.y, 0);
            }

            transform.position = newPosition;
        }

        /// <summary>Handles camera zoom based on player input.
        /// </summary>
        private void HandleCameraZoom()
        {
            float cameraZoomValue = playerInputController.GetCameraZoomValue();
            float baseZoom = cinemachineVirtualCamera.m_Lens.OrthographicSize;

            if (cameraZoomValue > 0) zoomTarget -= zoomMultiplier;
            else if (cameraZoomValue < 0) zoomTarget += zoomMultiplier;

            zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(baseZoom, zoomTarget, ref velocity, smoothTime);
        }

        public float GetMinZoom() => minZoom;
        public float GetMaxZoom() => maxZoom;
        public float GetZoomValue() => cinemachineVirtualCamera.m_Lens.OrthographicSize;

    }
}