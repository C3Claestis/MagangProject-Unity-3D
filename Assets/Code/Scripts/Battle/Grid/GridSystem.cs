using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }    

    /// <summary>
    /// Creates debug objects for visualizing the grid.
    /// </summary>
    /// <param name="debugPrefab">The prefab used for debug visualization.</param>
    public void CreateDebugObjects(Transform debugPrefab){
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform debugTransform = 
                    GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    /// <summary>
    /// Retrieves the grid object at the specified grid position.
    /// </summary>
    /// <param name="gridPosition">The grid position to query.</param>
    /// <returns>The grid object at the specified grid position.</returns>
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
    
    /// <summary>
    /// Converts a grid position to world space.
    /// </summary>
    /// <param name="gridPosition">The grid position to convert.</param>
    /// <returns>The corresponding world position.</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition){
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    /// <summary>
    /// Converts a world position to a grid position.
    /// </summary>
    /// <param name="worldPosition">The world position to convert.</param>
    /// <returns>The corresponding grid position.</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition){
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x/cellSize),
            Mathf.RoundToInt(worldPosition.z/cellSize)
        );
    }

    public bool IsValidGridPosition(GridPosition gridPosition){
        return  gridPosition.x >= 0 && 
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < height;
    }
}
