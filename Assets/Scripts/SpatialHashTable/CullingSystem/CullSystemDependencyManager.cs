namespace SpatialHashTable.CullingSystem
{
    public class CullSystemDependencyManager : SpatialHashDependencyManager<CullableObjectTagBase>
    {
        // Start is called before the first frame update
        void Awake()
        {
            GetAllManagers();
        }

        public override void GetAllManagers()
        {
            HashManagersList.AddRange( FindObjectsOfType<CullSystemManagerBase>());
          
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
