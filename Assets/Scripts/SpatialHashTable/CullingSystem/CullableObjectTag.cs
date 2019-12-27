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
            dependencyManager = FindObjectOfType<CullSystemDependencyManager>();
          
            if (dependencyManager != null && !IsInitiated)//the rest should be run only when the Tag  created after DependencyManager was inititated.  in run time other wise the dependency manager will handel registration by itself
            {
                dependencyManager.RegisterToDependencyManager(this);
            }
        }

        public override void Initiate()
        {
            if (IsInitiated) return;
            var cullableComponents = GetComponentsInChildren<ICullableObject>();
            foreach (var cullableObject in cullableComponents)
            {
                if (cullableObject.CullSystemID == HashManagerSystemID)
                {
                    cullableObjects.Add(cullableObject);
                    cullableObject.OnConnectToCullTag(this);
                }
            }

            IsInitiated = true;
        }

      


        protected override void OnDestroy()
        {
            if (dependencyManager != null)
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

        public void RemoveCullableObject(CullableObjectBase cullableObject)
        {
            cullableObjects.Remove(cullableObject);
        }
    }
}