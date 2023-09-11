namespace Nivandria.Battle.Grid
{
    using System;
    using UnityEngine;
    using TMPro;

    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] TextMeshPro textMeshPro;
        private object gridObject;

        private void Start()
        {
            // GridSystemVisual.Instance.OnGridVisualUpdated += GridSystemVisual_OnGridVisualUpdated;
        }

        protected virtual void Update()
        {
            UpdateGridDebugText();

        }

        /// <summary>Updates the debug text of the grid with information from the associated grid object.</summary>
        private void UpdateGridDebugText()
        {
            textMeshPro.text = gridObject.ToString();
        }

        /// <summary>Set the associated grid object for this entity.</summary>
        /// <param name="gridObject">The GridObject to associate with this entity.</param>
        public virtual void SetGridObject(object gridObject) => this.gridObject = gridObject;

        // //EVENT FUNCTION
        // private void GridSystemVisual_OnGridVisualUpdated(object sender, EventArgs e)
        // {
        //     UpdateGridDebugText();
        // }


    }
}