using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;
    private GridObject gridObject;

    private void Update(){
        textMeshPro.text = gridObject.ToString();
    }

    /// <summary>Set the associated grid object for this entity.
    /// </summary>
    /// <param name="gridObject">The GridObject to associate with this entity.</param>
    public void SetGridObject(GridObject gridObject) => this.gridObject = gridObject;
}
