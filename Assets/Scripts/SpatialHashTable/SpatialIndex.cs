using System;

namespace SpatialHashTable
{
    public struct SpatialIndex : IEquatable<SpatialIndex>
    {
        public bool Equals(SpatialIndex other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }


        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Z { get; private set; }

        public SpatialIndex(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

//    public override int GetHashCode()
//    {
//        return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
//    }

        public static bool operator ==(SpatialIndex a, SpatialIndex b)
        {
            return (a.Equals(b));
        }

        public static bool operator !=(SpatialIndex a, SpatialIndex b)
        {
            return !(a.Equals(b));
        }

        public override string ToString()
        {
            return ($"X: {X} Y:{Y} Z:{Z} ");
        }
    }
}