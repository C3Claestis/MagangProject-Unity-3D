namespace Nivandria.Battle.Grid
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int z;

        public GridPosition(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public float DistanceTo(GridPosition other)
        {
            int dx = other.x - this.x;
            int dz = other.z - this.z;
            return Mathf.Sqrt(dx * dx + dz * dz);
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition position &&
                    x == position.x &&
                    z == position.z;
        }

        public bool Equals(GridPosition other)
        {
            return this == other;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }

        public override string ToString()
        {
            return $"x: {x}; z: {z}";
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.x == b.x && a.z == b.z;
        }

        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return !(a == b);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x + b.x, a.z + b.z);
        }

        public static GridPosition operator -(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x - b.x, a.z - b.z);
        }
    }
}