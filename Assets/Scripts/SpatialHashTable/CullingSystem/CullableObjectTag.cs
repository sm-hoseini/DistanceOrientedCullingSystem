using System.Collections.Generic;

namespace SpatialHashTable.CullingSystem
{
    public class CullableObjectTag : SpatialTagBase
    {
        List<ICullableObject> cullableObjects = new List<ICullableObject>();
        private bool isCulled;
        private CullSystemDependencyManager dependencyManager;
        private void Start()
        {
            Initiate();
        }

        protected override void Initiate()
        {
            var cullableComponents = GetComponentsInChildren<ICullableObject>();
            foreach (var cullableObject in cullableComponents)
            {
                if (cullableObject.CullSystemID == HashManagerSystemID)
                {
                    cullableObjects.Add(cullableObject);
                }
            }


             dependencyManager = FindObjectOfType<CullSystemDependencyManager>();
            dependencyManager.AddTagToManagerSystem(this, isStatic);
        }

        protected override void OnDestroy()
        {
            dependencyManager.RemoveTagFromSystem(this);
        }

        public void CullChildrenTags()
        {
            if (isCulled) return;
            foreach (var cullTag in cullableObjects)
            {
                cullTag.OnCulled();
            }

            isCulled = true;
        }

        public void UnCullChildrenTags()
        {
            if (!isCulled) return;
            foreach (var cullables in cullableObjects)
            {
                cullables.OnUnCulled();
            }

            isCulled = false;
        }
    }
}