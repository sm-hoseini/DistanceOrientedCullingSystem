using System.Collections;
using System.Collections.Generic;
using SpatialHashTable;
using UnityEngine;

public class hashableObject : MonoBehaviour,ISpatialHashable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Position { get; }
    public int HashManagerSystemID { get; }
    public SpatialIndex SpatialIndex { get; set; }
    public SpatialHashManagerBase<ISpatialHashable> SpatialHashManagerBase { get; }
    public void SetSpatialHashIndex(in SpatialIndex index)
    {
        throw new System.NotImplementedException();
    }
}
