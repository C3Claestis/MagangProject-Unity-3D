using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;
    private GridObject gridObject;

    public void SetGridObject(GridObject gridObject){
        this.gridObject = gridObject;
    }

    private void Update(){
        textMeshPro.text = gridObject.ToString();
    }
}
