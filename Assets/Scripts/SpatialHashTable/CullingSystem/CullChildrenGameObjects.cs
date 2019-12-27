using System;
using System.Collections.Generic;
using SpatialHashTable.CullingSystem;
using UnityEngine;

namespace SpatialHashTable.CullingSystem
{
    public class CullChildrenGameObjects : CullableObjectBase
    {
        List<GameObject> ChildrenGameObjects = new List<GameObject>(3);

        // Start is called before the first frame update
        void Awake()
        {
            if (contentIsConstant)
            {
                UpdateObjectList();
            }
        }

      
        protected override void UpdateObjectList()
        {
            ChildrenGameObjects.Clear();
          
            for (int i = 0; i < transform.childCount; i++)
            {
                ChildrenGameObjects.Add(transform.GetChild(i).gameObject);
            }
        }

        public override void OnCulled()
        {
            base.OnCulled();

            foreach (var gameObject in ChildrenGameObjects)
            {
                gameObject.SetActive(false);
            }
        }

        public override void OnUnCulled()
        {
            base.OnUnCulled();
            foreach (var gameObject in ChildrenGameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}