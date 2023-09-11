namespace Nivandria.Battle.PathfindingSystem
{
    using Nivandria.Battle.Grid;
    using UnityEngine;

    public class Pathfinding : MonoBehaviour
    {
        [SerializeField] private Transform gridObjectDebugPrefab;

        private int width;
        private int height;
        private float cellSize;
        private GridSystem<PathNode> gridSystem;

        private void Awake()
        {
            int width = 8;
            int height = 4;
            float cellSize = 2;


            gridSystem = new GridSystem<PathNode>(width, height, cellSize,
                (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
            gridSystem.CreateDebugObjects(gridObjectDebugPrefab);
        }
    }

}