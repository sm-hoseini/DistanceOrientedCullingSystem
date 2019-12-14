using UnityEngine;

namespace SpatialHashTable
{
    public interface ISpatialHashable
    {
        Vector3 Position { get; }
        int HashManagerSystemID { get; }
        SpatialIndex SpatialIndex { get; set; }
    }
}