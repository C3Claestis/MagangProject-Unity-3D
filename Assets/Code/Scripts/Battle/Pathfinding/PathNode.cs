namespace Nivandria.Battle
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;


    public class PathNode
    {
        private GridPosition gridPosition;
        private int gCost;
        private int hCost;
        private int fCost;
        private PathNode cameFromPathNode;

        public PathNode(GridPosition gridPosition)
        {
            this.gridPosition = gridPosition;
        }

        public override string ToString() => gridPosition.ToString();

        public void ResetCameFromPathNode()
        {
            cameFromPathNode = null;
        }

        public void SetCameFromPathNode(PathNode pathNode)
        {
            cameFromPathNode = pathNode;
        }

        public PathNode GetCameFromPathNode()
        {
            return cameFromPathNode;
        }

        public GridPosition GetGridPosition()
        {
            return gridPosition;
        }

        public int GetGCost() => gCost;
        public int GetHCost() => hCost;
        public int GetFCost() => fCost;

        public void SetGCost(int gCost) => this.gCost = gCost;
        public void SetHCost(int hCost) => this.hCost = hCost;
        public void CalculateFCost() => fCost = gCost + hCost;

    }
}