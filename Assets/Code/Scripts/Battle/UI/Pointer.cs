namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;

    public class Pointer : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10.0f;
        [SerializeField] private Transform pointerCircle;
        private Vector3 target;
        private Transform mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            float pointerHeight = 1.0f;

            pointerCircle.position = new Vector3(transform.position.x, 0.005f , transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);


            if (!LevelGrid.Instance.IsValidGridPosition(gridPosition)) return;
            if (!Pathfinding.Instance.IsWalkableGridPosition(gridPosition)) return;
            if (LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition)) pointerHeight = 2.4f;

            Vector3 mousePosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            target = new Vector3(mousePosition.x, pointerHeight, mousePosition.z);
        }


        private void LateUpdate()
        {
            LookAtBackwards(mainCamera.position);
        }


        private void LookAtBackwards(Vector3 targetPos)
        {
            Vector3 offset = transform.position - targetPos;
            transform.LookAt(transform.position + offset);
        }
    }

}