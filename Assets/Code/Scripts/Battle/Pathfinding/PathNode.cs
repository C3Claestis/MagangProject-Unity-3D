namespace Nivandria.Battle.PathfindingSystem
{
    using Nivandria.Battle.Grid;

    public class PathNode
    {
        private GridPosition gridPosition;
        private int gCost;
        private int hCost;
        private int fCost;
        private PathNode cameFromPathNode;
        private bool isWalkable = true;

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
        public bool IsWalkable() => isWalkable;

        public void SetGCost(int gCost) => this.gCost = gCost;
        public void SetHCost(int hCost) => this.hCost = hCost;
        public void SetIsWalkable(bool isWalkable) => this.isWalkable = isWalkable;
        public void CalculateFCost() => fCost = gCost + hCost;

    }
}