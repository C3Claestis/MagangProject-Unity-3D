namespace Nivandria.Battle.Grid
{
    using System;
    using UnityEngine;
    using TMPro;

    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] TextMeshPro textMeshPro;
        private GridObject gridObject;

        private void Start() {
            GridSystemVisual.Instance.OnGridVisualUpdated += GridSystemVisual_OnGridVisualUpdated;
            UpdateGridDebugText();
        }

        /// <summary>Set the associated grid object for this entity.
        /// </summary>
        /// <param name="gridObject">The GridObject to associate with this entity.</param>
        public void SetGridObject(GridObject gridObject) => this.gridObject = gridObject;

        public void UpdateGridDebugText(){
            textMeshPro.text = gridObject.ToString();
        }

        private void GridSystemVisual_OnGridVisualUpdated(object sender, EventArgs e){
            UpdateGridDebugText();
        }
    }

}