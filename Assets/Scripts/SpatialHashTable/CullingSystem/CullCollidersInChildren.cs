using System.Collections;
using System.Collections.Generic;
using SpatialHashTable.CullingSystem;
using UnityEngine;

public class CullCollidersInChildren : CullableObjectBase
{
    List<Collider>childrenColliders= new List<Collider>(3);
        
    // Start is called before the first frame update
    void Start()
    {
        UpdateObjectList();
    }
   

    protected override void UpdateObjectList()
    {
        childrenColliders.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenColliders.AddRange(transform.GetChild(i).gameObject.GetComponentsInChildren<Collider>());
        }  
    }

    public override void OnCulled()
    {
        base.OnCulled();
        foreach (var collider in childrenColliders)
        {
            collider.enabled=false;
        }
    }

    public override void OnUnCulled()
    {
        base.OnUnCulled();

        foreach (var collider in childrenColliders)
        {
            collider.enabled=true;
        }
    }
}
