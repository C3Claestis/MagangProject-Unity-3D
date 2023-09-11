namespace Nivandria.Battle
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;


    public class PathNode
    {
        private GridPosition gridPosition;
        private PathNode cameFromPathNode;
        private int gCost;
        private int hCost;
        private int fCost;

        public PathNode(GridPosition gridPosition)
        {
            this.gridPosition = gridPosition;
        }

        public override string ToString()
        {
            return gridPosition.ToString();
        }

        public int GetGCost() => gCost;
        public int GetHCost() => hCost;
        public int GetFCost() => fCost;
    }
}