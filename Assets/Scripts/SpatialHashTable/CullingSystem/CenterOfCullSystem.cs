using UnityEngine;

namespace SpatialHashTable.CullingSystem
{
    public class CenterOfCullSystem : MonoBehaviour
    {
        [SerializeField] private int hashTableSystemID;
         public int HashTableSystemID=>hashTableSystemID;
        [SerializeField] private Transform centerOfCullSystem;

        [SerializeField] private float updateTimeStep = 0.1f;
        private ICullSystemManager cullSystemManager;

        public void Initiate(ICullSystemManager cullSystemManager)
        {
            this.cullSystemManager = cullSystemManager;
        }
        private float lastUpdateTime;

        private void Update()
        {
            if (Time.time > lastUpdateTime + updateTimeStep)
            {
                lastUpdateTime = Time.time;
                UpdateCenterPosition(centerOfCullSystem.position);
            }
        }

        private void UpdateCenterPosition(Vector3 position)
        {
            cullSystemManager.SetCenter(position);
        }
    }
}