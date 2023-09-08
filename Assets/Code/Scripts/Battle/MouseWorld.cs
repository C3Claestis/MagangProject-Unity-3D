namespace Nivandria.Battle
{
    using UnityEngine;

    public class MouseWorld : MonoBehaviour
    {
        private static MouseWorld instance;
        [SerializeField] private LayerMask mousePlaneLayerMask;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            transform.position = MouseWorld.GetPosition();
        }

        /// <summary>Gets the world position in 3D space corresponding to the current mouse cursor position on the screen.
        /// </summary>
        /// <remarks>Uses a raycast from the camera through the mouse cursor to interact with the scene.</remarks>
        /// <returns>The 3D world position where the raycast from the mouse cursor hits an object.</returns>
        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.mousePlaneLayerMask);
            return hitInfo.point;
        }
    }

}