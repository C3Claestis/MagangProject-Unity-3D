namespace Nivandria.Battle
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask mousePlaneLayerMask;
        private static MouseWorld instance;
        private static bool isPointerOnUI;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There's more than one MouseWorld! " + transform + " - " + instance);
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Update() {
            isPointerOnUI = EventSystem.current.IsPointerOverGameObject(); // ! TEMPORARY
        }

        /// <summary>Gets the world position in 3D space corresponding to the current mouse cursor position on the screen.</summary>
        /// <remarks>Uses a raycast from the camera through the mouse cursor to interact with the scene.</remarks>
        /// <returns>The 3D world position where the raycast from the mouse cursor hits an object.</returns>
        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.mousePlaneLayerMask);
            return hitInfo.point;
        }

        public static bool IsPointerOnUI(){
            return isPointerOnUI;
        }

    }
}