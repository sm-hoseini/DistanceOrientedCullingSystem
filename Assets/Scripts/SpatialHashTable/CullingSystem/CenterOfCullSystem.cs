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
            isInitiated=true;
        }
        private float lastUpdateTime;
        private bool isInitiated;

        private void Update()
        {
            if (isInitiated && Time.time > lastUpdateTime + updateTimeStep)
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