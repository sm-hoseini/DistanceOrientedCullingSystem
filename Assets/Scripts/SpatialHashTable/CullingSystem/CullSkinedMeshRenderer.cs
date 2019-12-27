using System.Collections;
using System.Collections.Generic;
using SpatialHashTable.CullingSystem;
using UnityEngine;

public class CullSkinedMeshRenderer : CullableObjectBase
{
    //List<MeshRenderer>meshRenderers= new List<MeshRenderer>(2);
    private SkinnedMeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (contentIsConstant)
        {
            UpdateObjectList();
        }
    }

    protected override void UpdateObjectList()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public override void OnCulled()
    {
        base.OnCulled();
        meshRenderer.enabled = false;
    }

    public override void OnUnCulled()
    {
        base.OnUnCulled();

        meshRenderer.enabled = true;
    }
}