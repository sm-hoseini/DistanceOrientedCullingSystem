using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpatialHashTable.CullingSystem;
using UnityEngine;

public abstract class CullableObjectBase : MonoBehaviour, ICullableObject
{
    [SerializeField] protected int cullSystemId;

    [SerializeField] protected bool contentIsConstant;

    // Start is called before the first frame update
    List<CullableObjectTag> conncetedTags = new List<CullableObjectTag>();

    public int CullSystemID => cullSystemId;
    protected abstract void UpdateObjectList();

    private void OnDestroy()
    {
        foreach (var tag in conncetedTags)
        {
            try
            {
                tag.RemoveCullableObject(this);
            }
            catch (MissingReferenceException e)
            {
                conncetedTags.Remove(tag);

                Console.WriteLine(e);
                throw;
            }
        }
    }

    public virtual void OnCulled()
    {
        if (!contentIsConstant)
        {
            UpdateObjectList();
        }
    }

    public virtual void OnUnCulled()
    {
        if (!contentIsConstant)
        {
            UpdateObjectList();
        }
    }

    public void OnConnectToCullTag(CullableObjectTag cullTag)
    {
        if (!conncetedTags.Contains(cullTag))
        {
            conncetedTags.Add(cullTag);
        }
    }

 

}