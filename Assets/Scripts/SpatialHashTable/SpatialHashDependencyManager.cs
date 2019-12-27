using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpatialHashTable
{
    public abstract class SpatialHashDependencyManager<T> : MonoBehaviour where T : Object, ISpatialHashable
    {
        protected List<SpatialHashManagerBase<T>> HashManagersList = new List<SpatialHashManagerBase<T>>();

        [SerializeField] private bool InitiateOnAwake=true;
        List<T> unregisteredTagList = new List<T>(10);

        private bool isInitiated = false;
        // Start is called before the first frame update

        public virtual void Awake()
        {
            if (InitiateOnAwake)
            {
               Initiate();
            }
        }

        public void Initiate()
        {
            if (isInitiated) return;
            GetDependencies();
            ConnectDependencies();
            isInitiated = true;
        }
        protected virtual void GetDependencies()
        {
        
            unregisteredTagList.AddRange(FindObjectsOfType<T>());

        }

        public void RegisterToDependencyManager(T tag)
        {
            tag.Initiate();
            AddTagToManagerSystem(tag);
        }

        private void AddTagToManagerSystem(T tag)
        {
            var manager = HashManagersList.Find(x => x.HashTableSystemID == tag.HashManagerSystemID);
            if (manager == null)
            {
                throw new InvalidOperationException(
                    $" No SpatialHashMangerSystem has been defined for spatialTag with HashManagerSystemID:{tag.HashManagerSystemID}  ");
            }

            tag.SpatialIndex = manager.Vector3ToSpatialIndex(tag.Position);
            manager.AddObject(tag);
            if (!tag.IsStatic)
            {
                manager.SubscribeToUpdateList(tag);
            }
        }

        protected virtual void ConnectDependencies()
        {
            InitiatAllSpatialTags();
        }

        private void InitiatAllSpatialTags()
        {
            while (unregisteredTagList.Count > 0)
            {
                var tagIndex = unregisteredTagList.Count - 1;
                if (unregisteredTagList[tagIndex].IsInitiated)
                {
                    unregisteredTagList.RemoveAt(tagIndex);
                    continue;
                }

                RegisterToDependencyManager(unregisteredTagList[tagIndex]);
                unregisteredTagList.RemoveAt(tagIndex);
            }

            unregisteredTagList.Clear();
        }

        public void RemoveTagFromSystem(T tag)
        {
            var manager = HashManagersList.Find(x => x.HashTableSystemID == tag.HashManagerSystemID);
            if (manager == null) return;
            manager.RemoveObject(tag);
            manager.UnSubscribeToUpdateList(tag);
        }

        // Update is called once per frame
    }
}