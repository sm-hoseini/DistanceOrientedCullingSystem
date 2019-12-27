using System.Collections.Generic;

namespace SpatialHashTable.CullingSystem
{
    public interface ICullableObject
    {
        int CullSystemID { get; }
        void OnCulled();
        void OnUnCulled();
        void OnConnectToCullTag(CullableObjectTag cullTag);
    }
}
