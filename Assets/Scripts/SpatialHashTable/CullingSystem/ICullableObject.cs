namespace SpatialHashTable.CullingSystem
{
    public interface ICullableObject
    {
        int CullSystemID { get; }
        void OnCulled();
        void OnUnCulled();
    }
}
