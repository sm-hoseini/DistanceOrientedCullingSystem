using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpatialHashTable.CullingSystem
{
    public class CullCollider : CullableObjectBase
    {
        readonly List<Collider> colliderList = new List<Collider>();
        void Start()
        {
            if (contentIsConstant)
            {
                UpdateObjectList();
            }
        }

        protected override void UpdateObjectList()
        {
            colliderList.Clear();
            colliderList.AddRange(GetComponents<Collider>());
        }

        public override void OnCulled()
        {
            base.OnCulled();
            foreach (var colliderComp in colliderList)
            {
                colliderComp.enabled = false;
            }
        }

        public override void OnUnCulled()
        {
            base.OnUnCulled();
            foreach (var colliderComp in colliderList)
            {
                colliderComp.enabled = true;
            }
        }
    }
}