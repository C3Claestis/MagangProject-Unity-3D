namespace Nivandria.Battle.Grid
{
    using System;
    using UnityEngine;
    using TMPro;

    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] TextMeshPro textMeshPro;
        private GridObject gridObject;

        private void Start()
        {
            GridSystemVisual.Instance.OnGridVisualUpdated += GridSystemVisual_OnGridVisualUpdated;
            UpdateGridDebugText();
        }

        /// <summary>Set the associated grid object for this entity.</summary>
        /// <param name="gridObject">The GridObject to associate with this entity.</param>
        public void SetGridObject(GridObject gridObject) => this.gridObject = gridObject;

        /// <summary>Updates the debug text of the grid with information from the associated grid object.</summary>
        private void UpdateGridDebugText()
        {
            textMeshPro.text = gridObject.ToString();
        }


        //EVENT FUNCTION
        private void GridSystemVisual_OnGridVisualUpdated(object sender, EventArgs e)
        {
            UpdateGridDebugText();
        }
    }
}