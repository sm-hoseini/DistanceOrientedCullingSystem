using UnityEngine;

namespace SpatialHashTable.CullingSystem
{
    public class CullMeshRenderer : MonoBehaviour,ICullableObject
    {
        [SerializeField] private int cullSystemID;
        //List<MeshRenderer>meshRenderers= new List<MeshRenderer>(2);
        private MeshRenderer meshRenderer;
        // Start is called before the first frame update
        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public int CullSystemID => cullSystemID;
        public void OnCulled()
        {
            meshRenderer.enabled = false;
        }

        public void OnUnCulled()
        {
            meshRenderer.enabled = true;
        }
    }
}
