using System;
using UnityEngine;

namespace SpatialHashTable
{
    public abstract class SpatialTagBase: MonoBehaviour,ISpatialHashable
    {

        [SerializeField]
        protected bool isStatic;
        public bool IsInitiated { get;protected set; }
        [SerializeField] private int hashTablleManagerID = 0;
        public abstract void Initiate();
        public bool IsStatic => isStatic;

        public Vector3 Position => transform.position;
        public int HashManagerSystemID => hashTablleManagerID;

        public SpatialIndex SpatialIndex { get;  set; }

        protected abstract void OnDestroy();
    }
}