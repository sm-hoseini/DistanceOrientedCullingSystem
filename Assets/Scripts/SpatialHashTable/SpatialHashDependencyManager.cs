using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpatialHashTable
{
    public abstract class SpatialHashDependencyManager<T> : MonoBehaviour where T:ISpatialHashable
    {
        protected List<SpatialHashManagerBase<T> > HashManagersList= new List<SpatialHashManagerBase<T>>();
        // Start is called before the first frame update
   
        public abstract void GetAllManagers();

        public void AddTagToManagerSystem(T tag,bool isStatic)
        {
            var manager = HashManagersList.Find(x => x.HashTableSystemID == tag.HashManagerSystemID);
            if (manager==null)
            {
                throw new InvalidOperationException($" No SpatialHashMangerSystem has been defined for spatialTag with HashManagerSystemID:{tag.HashManagerSystemID}  ");
            }
            manager.AddObject(tag);
            if (!isStatic)
            {
                manager.SubscribeToUpdateList(tag);
            }
        
        }
        // Update is called once per frame
   
    }
}
