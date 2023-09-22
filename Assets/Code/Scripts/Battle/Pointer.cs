namespace Nivandria.Battle
{
    using System;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class Pointer : MonoBehaviour
    {
        public static Pointer Instance { get; private set; }

        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private Transform pointerCircle;
        [SerializeField] private Transform rotateVisual;

        private Vector3 target;
        private Transform mainCamera;
        private GridPosition currentGrid;
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

        private void Start()
        {
            PlayerInputController.Instance.OnActionMapChanged += PlayerInputController_OnActionMapChanged;
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
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

            GridPosition gridPosition = new GridPosition();

            if (PlayerInputController.Instance.IsCurrentControllerGamepad())
            {
                gridPosition = CameraController.Instance.GetCurrentGridPosition();
            }
            else
            {
                gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            }

            if (gridPosition == currentGrid) return;
            if (!LevelGrid.Instance.IsValidGridPosition(gridPosition)) return;

            currentGrid = gridPosition;
            Vector3 mousePosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            target = new Vector3(mousePosition.x, GetPointerHeight(gridPosition), mousePosition.z);
        }

        private void MovePointer()
        {
            pointerCircle.position = new Vector3(transform.position.x, pointerCircle.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);
        }

        private void LookAtBackwards(Vector3 targetPos)
        {
            Vector3 offset = transform.position - targetPos;
            transform.LookAt(transform.position + offset);
        }

        private void PlayerInputController_OnActionMapChanged(object sender, string actionMap)
        {
            if (actionMap == "Gridmap")
            {
                SetActive(true);
            }
            else
            {
                SetActive(false);
            }
        }

        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            SetPointerOnGrid(UnitTurnSystem.Instance.GetSelectedUnit().GetGridPosition());
        }

        private float GetPointerHeight(GridPosition gridPosition)
        {
            float pointerHeight = 1.0f;

            if (Pathfinding.Instance.IsObstacleOnGrid(LevelGrid.Instance.GetWorldPosition(currentGrid), out string objectTag)) pointerHeight = 3.5f;
            if (objectTag == "Obstacle") pointerHeight = 1.75f;
            if (objectTag == "Holes") pointerHeight = 1.0f;
            if (LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition)) pointerHeight = 2.4f;
            if (LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition) && objectTag == "Obstacle") pointerHeight = 2.9f;

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


        public GridPosition GetCurrentGrid() => currentGrid;
        public RotateVisual GetRotateVisual() => rotateVisual.GetComponent<RotateVisual>();

        public void SetActive(bool status) => isActive = status;
    }

}