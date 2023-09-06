namespace WorldMap
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Destination : MonoBehaviour
    {

        [SerializeField] private Transform icon;
        [SerializeField] private string destinationName;
        private List<Destination> connectionList = new List<Destination>();

        private float iconSizeMin = 0.1f;
        private float iconSizeMax = 0.25f;
        private float cameraMaxZoom;
        private float cameraMinZoom;

        private void Start()
        {
            gameObject.name = destinationName;
            cameraMaxZoom = CameraController.Instance.GetMaxZoom();
            cameraMinZoom = CameraController.Instance.GetMinZoom();
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

        public void AddConnection(Destination destination) => connectionList.Add(destination);
        public List<Destination> GetConnections() => connectionList;
        public string GetName() => destinationName;
        public Vector3 GetPosition() => transform.position;
    }
}