namespace WorldMap
{
    using System.Collections.Generic;
    using Dreamteck.Splines;
    using UnityEngine;


    public class PlayerPointer : MonoBehaviour
    {
        private SplinePositioner splinePositioner;

        [SerializeField] private Transform icon;
        private float iconSizeMin = 0.3f;
        private float iconSizeMax = 0.7f;
        private float cameraMaxZoom;
        private float cameraMinZoom;

        private float normalMoveSpeed = 0.2f;
        private float fastMoveSpeed = 0.6f;
        [SerializeField] private bool fastSpeedEnable = false;
        private bool isMoving = false;
        private double moveProgress = 0.0;

        [SerializeField] private Road currentRoad;
        [SerializeField] private Destination currentTown;
        private Destination targetTown;
        private List<string> path;

        private void Awake()
        {
            splinePositioner = GetComponent<SplinePositioner>();
        }

        private void Start()
        {
            cameraMaxZoom = CameraController.Instance.GetMaxZoom();
            cameraMinZoom = CameraController.Instance.GetMinZoom();
            path = new List<string>();

            if (currentTown != null)
            {
                SetPlayerPosition(currentTown.transform.position);
            }
        }

        private void Update()
        {
            HandleIconSize();
        }

        private void HandleIconSize()
        {
            float currentZoom = CameraController.Instance.GetZoomValue();
            float iconSize = Mathf.Lerp(iconSizeMin, iconSizeMax, Mathf.InverseLerp(cameraMinZoom, cameraMaxZoom, currentZoom));
            icon.localScale = new Vector3(iconSize, iconSize, iconSize);
        }


        public void ResetMoveProgress() => moveProgress = 0.0;

        public double GetMoveProgress() => moveProgress;
        public bool GetMoveStatus() => isMoving;

        public void SetPlayerPosition(Vector3 targetPosition) => transform.position = targetPosition;
        public void SetMoveStatus(bool status) => isMoving = status;
    }
}