using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Profiling;

namespace SpatialHashTable
{
    public abstract  class SpatialHashManagerBase<T> : MonoBehaviour where T : ISpatialHashable
    {
        /// <summary> Zero value for each of axes will ignore that axe </summary>
        [SerializeField] protected float cellSizeX = 10, cellSizeY = 10, cellSizeZ = 10;

        [SerializeField] protected int hashTableSystemId;
        public int HashTableSystemID => hashTableSystemId;
        [SerializeField] private float updateRate = 0.3f;
        protected Dictionary<SpatialIndex, List<T>> objectsTable = new Dictionary<SpatialIndex, List<T>>(20);

        [SerializeField] protected bool visualize;

        public void GetSpacialIndexes(Vector3 position, out int x, out int y, out int z)
        {
            x = (int) cellSizeX != 0 ? Mathf.FloorToInt(position.x / cellSizeX) : 0;
            y = (int) cellSizeY != 0 ? Mathf.FloorToInt(position.y / cellSizeY) : 0;
            z = (int) cellSizeZ != 0 ? Mathf.FloorToInt(position.z / cellSizeZ) : 0;
        }

        public virtual void AddObject(T Obj)
        {
            // GetSpacialIndexes(Obj.Position, out int x, out int y, out int z);
            //var spatialIndex = new SpatialIndex(x, y, z);
            var spatialIndex = Obj.SpatialIndex;
            if (!objectsTable.ContainsKey(spatialIndex))
            {
                objectsTable.Add(spatialIndex, new List<T>(5));
                cachIsDerty = true;
            }

            objectsTable[spatialIndex].Add(Obj);

        }

        public virtual void RemoveObject(T Obj)
        {
            objectsTable[Obj.SpatialIndex].Remove(Obj);
            objectsUpdateQuerry.Remove(Obj);
            if (objectsTable[Obj.SpatialIndex].Count == 0)
            {
                cachIsDerty = true;
                objectsTable.Remove(Obj.SpatialIndex);
            }
        }

        public void UpdateObjectIndex()
        {
            foreach (var hashObject in objectsUpdateQuerry)
            {
                GetSpacialIndexes(hashObject.Position, out int x, out int y, out int z);
                var newIndext = new SpatialIndex(x, y, z);
                if (newIndext != hashObject.SpatialIndex)
                {
                    RemoveObject(hashObject);
                    hashObject.SpatialIndex = newIndext;
                    AddObject(hashObject);
                }
            }
        }

        List<T> objectsUpdateQuerry = new List<T>(50);


        public void SubscribeToUpdateList(T obj)
        {
            if (!objectsUpdateQuerry.Contains(obj))
            {
                objectsUpdateQuerry.Add(obj);
            }
        }
        // SpatialIndex temp = new SpatialIndex();


        private int xMaxIndex, xMinIndex, yMaxIndex, yMinIndex, zMaxIndex, zMinIndex;
        private SpatialIndex cachedIndex;
        private float catchedRadious;
        private bool cachIsDerty;
        List<SpatialIndex> catchedIndexes = new List<SpatialIndex>(10);

        public void GetAllObjectNearbyNoneAloc(List<T> results, float radious, Vector3 centerPoint,
            bool usingCach = true)
        {
            UpdateNearbyIndexiesCach(radious, centerPoint, usingCach);
            results.Clear();
            foreach (var index in catchedIndexes)
            {
                results.AddRange(objectsTable[index]);
            }

//        if (cachedIndex == Vector3ToSpatialIndex(centerPoint) && catchedRadious == radious && usingCach && !cachIsDerty)
//        {
//            foreach (var index in catchedIndexes)
//            {
//                if (objectsTable.ContainsKey(index))
//                {
//                    results.AddRange(objectsTable[index]);
//                }
//            }
//        }
//        else
//        {
//            catchedIndexes.Clear();
//            cachedIndex = Vector3ToSpatialIndex(centerPoint);
//            catchedRadious = radious;
//            cachIsDerty = false;
//            GetSpacialIndexes(centerPoint + Vector3.one * radious, out int rightBoundIndex, out int upBoundIndex,
//                out int frontBoundIndex);
//            GetSpacialIndexes(centerPoint - Vector3.one * radious, out int leftBoundIndex, out int downBoundIndex,
//                out int backBoundIndex);
//
//
//            for (int i = leftBoundIndex; i <= rightBoundIndex; i++)
//            {
//                for (int j = downBoundIndex; j <= upBoundIndex; j++)
//                {
//                    for (int k = backBoundIndex; k <= frontBoundIndex; k++)
//                    {
//                        var temp = new SpatialIndex(i, j, k);
//                        // temp.SetValue(i, j, k);
//
//                        if (objectsTable.ContainsKey(temp))
//                        {
//                            results.AddRange(objectsTable[temp]);
//                            catchedIndexes.Add(temp);
//                        }
//                    }
//                }
//            }
//        }
        }

        public void GetAllIndexesNearbyNoneAloc(List<SpatialIndex> results, float radious, Vector3 centerPoint,
            bool usingCach = true)
        {
            results.Clear();
            UpdateNearbyIndexiesCach(radious, centerPoint, usingCach);
            results.AddRange(catchedIndexes);
//        results.Clear();
//        if (CachedValid(radious, centerPoint) && usingCach)
//        {
//            results = catchedIndexes;
//            return;
//        }
//
//        catchedIndexes.Clear();
//        cachedIndex = Vector3ToSpatialIndex(centerPoint);
//        catchedRadious = radious;
//        cachIsDerty = false;
//        GetSpacialIndexes(centerPoint + Vector3.one * radious, out int rightBoundIndex, out int upBoundIndex,
//            out int frontBoundIndex);
//        GetSpacialIndexes(centerPoint - Vector3.one * radious, out int leftBoundIndex, out int downBoundIndex,
//            out int backBoundIndex);
//        
//        var querry = from index in objectsTable.Keys
//            where index.X >= leftBoundIndex && index.Y >= downBoundIndex && index.Z >= backBoundIndex &&
//                  index.X <= rightBoundIndex && index.Y <= upBoundIndex && index.Z <= frontBoundIndex
//            select index;
//        
//        foreach (var index in querry)
//        {
//            if (objectsTable.ContainsKey(index))
//            {
//                results.Add(index);
//                catchedIndexes.Add(index);
//            }
            //   }

//        for (int i = leftBoundIndex; i <= rightBoundIndex; i++)
//        {
//            for (int j = downBoundIndex; j <= upBoundIndex; j++)
//            {
//                for (int k = backBoundIndex; k <= frontBoundIndex; k++)
//                {
//                    var temp = new SpatialIndex(i, j, k);
//                    // temp.SetValue(i, j, k);
//
//                    if (objectsTable.ContainsKey(temp))
//                    {
//                        results.Add(temp);
//                        catchedIndexes.Add(temp);
//                    }
//                }
//            }
//        }
        }

        private void UpdateNearbyIndexiesCach(float radious, Vector3 centerPoint,
            bool usingCach = true)
        {
            if (CachedValid(radious, centerPoint) && usingCach)
            {
                return;
            }

            catchedIndexes.Clear();
            cachedIndex = Vector3ToSpatialIndex(centerPoint);
            catchedRadious = radious;
            cachIsDerty = false;
            GetSpacialIndexes(centerPoint + Vector3.one * radious, out int rightBoundIndex, out int upBoundIndex,
                out int frontBoundIndex);
            GetSpacialIndexes(centerPoint - Vector3.one * radious, out int leftBoundIndex, out int downBoundIndex,
                out int backBoundIndex);

            var querry = from index in objectsTable.Keys
                where index.X >= leftBoundIndex && index.Y >= downBoundIndex && index.Z >= backBoundIndex &&
                      index.X <= rightBoundIndex && index.Y <= upBoundIndex && index.Z <= frontBoundIndex
                select index;

            foreach (var index in querry)
            {
                if (objectsTable.ContainsKey(index))
                {
                    catchedIndexes.Add(index);
                }
            }

//        for (int i = leftBoundIndex; i <= rightBoundIndex; i++)
//        {
//            for (int j = downBoundIndex; j <= upBoundIndex; j++)
//            {
//                for (int k = backBoundIndex; k <= frontBoundIndex; k++)
//                {
//                    var temp = new SpatialIndex(i, j, k);
//                    // temp.SetValue(i, j, k);
//
//                    if (objectsTable.ContainsKey(temp))
//                    {
//                        results.Add(temp);
//                        catchedIndexes.Add(temp);
//                    }
//                }
//            }
//        }
        }

        private bool CachedValid(float radious, Vector3 centerPoint)
        {
            return cachedIndex == Vector3ToSpatialIndex(centerPoint) && Mathf.Approximately(catchedRadious, radious) &&
                   !cachIsDerty;
        }

        protected SpatialIndex Vector3ToSpatialIndex(Vector3 position)
        {
            GetSpacialIndexes(position, out int x, out int y, out int z);
            return new SpatialIndex(x, y, z);
        }

        private void OnDrawGizmos()
        {
            if (visualize)
            {
                Gizmos.color = Color.gray;
                foreach (var cell in objectsTable.Keys)
                {
                    Gizmos.DrawWireCube(
                        new Vector3(cell.X * cellSizeX, cell.Y * cellSizeY, cell.Z * cellSizeZ) +
                        new Vector3(cellSizeX, cellSizeY, cellSizeZ) * .5f,
                        new Vector3(cellSizeX, cellSizeY, cellSizeZ));
                }

                Gizmos.color = Color.cyan;
                foreach (var objs in objectsTable.Values)
                {
                    foreach (var obj in objs)
                    {
                        Gizmos.DrawSphere(obj.Position, 0.1f);
                    }
                }
            }
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            InvokeRepeating(nameof(UpdateObjectIndex), 0, updateRate);
        }





    }
}