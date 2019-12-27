using UnityEngine;

namespace SpatialHashTable
{
    public interface ISpatialHashable
    {
        Vector3 Position { get; }
        int HashManagerSystemID { get; }
        SpatialIndex SpatialIndex { get; set; }
        /// <summary>Call this method to get inner initial needed dependency</summary>
       void Initiate();
        bool IsStatic { get; }
        bool IsInitiated { get; }
    }
}