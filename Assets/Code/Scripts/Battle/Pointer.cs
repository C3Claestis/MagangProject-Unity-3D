namespace Nivandria.Battle
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;

    public class Pointer : MonoBehaviour
    {
        public static Pointer Instance { get; private set; }

        [SerializeField] private float moveSpeed = 10.0f;
        [SerializeField] private Transform pointerCircle;
        private Vector3 target;
        private Transform mainCamera;
        private bool isActive = true;


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one Pointer! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            HandlePointerPosition();
            MovePointer();
        }

        private void LateUpdate()
        {
            LookAtBackwards(mainCamera.position);
        }

        /// <summary>Handles the pointer's behavior.</summary>
        private void HandlePointerPosition()
        {
            if (!isActive) return;

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!LevelGrid.Instance.IsValidGridPosition(gridPosition)) return;
            if (!Pathfinding.Instance.IsWalkableGridPosition(gridPosition)) return;

            Vector3 mousePosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            target = new Vector3(mousePosition.x, GetPointerHeight(gridPosition), mousePosition.z);
        }

        private void MovePointer()
        {
            pointerCircle.position = new Vector3(transform.position.x, 0.005f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);
        }

        private void LookAtBackwards(Vector3 targetPos)
        {
            Vector3 offset = transform.position - targetPos;
            transform.LookAt(transform.position + offset);
        }

        private float GetPointerHeight(GridPosition gridPosition)
        {
            float pointerHeight = 1.0f;
            if (LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition)) pointerHeight = 2.4f;

            return pointerHeight;
        }

        public void SetPointerOnGrid(GridPosition gridPosition)
        {

            if (!LevelGrid.Instance.IsValidGridPosition(gridPosition))
            {
                Debug.LogError("Can't set pointer to outside grid system!");
                return;
            }

            if (!Pathfinding.Instance.IsWalkableGridPosition(gridPosition))
            {
                Debug.LogError("Can't set pointer to unwalkable grid!");
                return;
            }

            Vector3 mousePosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            target = new Vector3(mousePosition.x, GetPointerHeight(gridPosition), mousePosition.z);
        }

        public void SetActive(bool status) => isActive = status;
    }

}