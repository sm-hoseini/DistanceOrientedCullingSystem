using System;
using UnityEngine;

namespace SpatialHashTable
{
    public abstract class SpatialTagBase: MonoBehaviour,ISpatialHashable
    {

        [SerializeField]
        protected bool isStatic;

        [SerializeField] private int hashTablleManagerID = 0;
        protected abstract void Initiate();

        public Vector3 Position => transform.position;
        public int HashManagerSystemID => hashTablleManagerID;

        public SpatialIndex SpatialIndex { get;  set; }

        protected abstract void OnDestroy();
    }
}