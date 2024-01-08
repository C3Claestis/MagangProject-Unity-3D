namespace Nivandria.Battle.PathfindingSystem
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    using TMPro;
    using System;

    public class PathFindingGridDebugObject : GridDebugObject
    {
        [SerializeField] TextMeshPro gCostText;
        [SerializeField] TextMeshPro hCostText;
        [SerializeField] TextMeshPro fCostText;
        [SerializeField] GameObject unWalkablePlane;
        [SerializeField] GameObject walkablePlane;

        private PathNode pathNode;

        private void Start() {
            UnitActionSystem.Instance.OnMoveActionPerformed += UnitActionSystem_OnMoveActionPerformed;
        }

        public override void SetGridObject(object gridObject)
        {
            base.SetGridObject(gridObject);
            pathNode = (PathNode)gridObject;
        }

        protected override void UpdateGridDebugText()
        {
            base.UpdateGridDebugText();

            Debug.Log("Pathfinding Updated");

            gCostText.text = pathNode.GetGCost().ToString();
            hCostText.text = pathNode.GetHCost().ToString();
            fCostText.text = pathNode.GetFCost().ToString();

            if (LevelGrid.Instance.WalkableGridDebugStatus())
            {
                if (pathNode.IsWalkable())
                {
                    walkablePlane.SetActive(true);
                }
                else
                {
                    unWalkablePlane.SetActive(true);
                }

            }
            else
            {
                walkablePlane.SetActive(false);
                unWalkablePlane.SetActive(false);
            }
        }

        private void UnitActionSystem_OnMoveActionPerformed(object sender, EventArgs e){
            UpdateGridDebugText();
        }

    }
}