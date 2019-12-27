using UnityEngine;

namespace SpatialHashTable.CullingSystem
{
    public class CullMeshRenderer :CullableObjectBase
    {
        private MeshRenderer meshRenderer;
        // Start is called before the first frame update
        void Start()
        {
            if (contentIsConstant)
            {
                UpdateObjectList();
            }
        }

        // Update is called once per frame
      

        protected override void UpdateObjectList()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public override void OnCulled()
        {
            base.OnCulled();
            meshRenderer.enabled = false;
        }

        public override void OnUnCulled()
        {
            base.OnUnCulled();
            meshRenderer.enabled = true;
        }
    }
}
