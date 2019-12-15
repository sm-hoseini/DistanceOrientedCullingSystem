namespace SpatialHashTable.CullingSystem
{
    public class CullSystemDependencyManager : SpatialHashDependencyManager<CullableObjectTag>
    {
        // Start is called before the first frame update
        void Awake()
        {
            GetAllManagers();
        }

        public override void GetAllManagers()
        {
            HashManagersList.AddRange( FindObjectsOfType<CullSystemManager>());
          
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
