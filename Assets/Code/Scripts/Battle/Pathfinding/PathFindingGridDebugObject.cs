namespace Nivandria.Battle.PathfindingSystem
{
    using Nivandria.Battle.Grid;
    using UnityEngine;
    using TMPro;

    public class PathFindingGridDebugObject : GridDebugObject
    {
        [SerializeField] TextMeshPro gCostText;
        [SerializeField] TextMeshPro hCostText;
        [SerializeField] TextMeshPro fCostText;

        private PathNode pathNode;

        public override void SetGridObject(object gridObject)
        {
            base.SetGridObject(gridObject);
            pathNode = (PathNode)gridObject;
        }

        protected override void Update(){
            base.Update();
            gCostText.text = pathNode.GetGCost().ToString();
            hCostText.text = pathNode.GetHCost().ToString();
            fCostText.text = pathNode.GetFCost().ToString();
        }

    }
}