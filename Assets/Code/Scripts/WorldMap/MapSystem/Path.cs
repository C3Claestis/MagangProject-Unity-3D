namespace WorldMap.MapSystem
{
    using System.Collections.Generic;

    public class Path
    {
        
        private List<Destination> destinationList = new List<Destination>();
        private float pathCost;

        public Path(List<Destination> destinationList, float pathCost)
        {
            this.destinationList = destinationList;
            this.pathCost = pathCost;
        }

        public bool Contains(Destination destination)
        {
            return destinationList.Contains(destination);
        }

        public bool IsLowerThan(float pathCost) => this.pathCost < pathCost;
        public float GetPathCost() => pathCost;
        public List<Destination> GetPath() => destinationList;
    }
}