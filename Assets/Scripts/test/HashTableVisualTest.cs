using System;
using System.Collections;
using System.Collections.Generic;
using SpatialHashTable;
using UnityEngine;
using Random = UnityEngine.Random;

public class HashTableVisualTest : MonoBehaviour
{
    [SerializeField] private HashManagerBase hashManagerBase;

    [SerializeField] private GameObject obj; 
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            Instantiate(obj, Random.onUnitSphere * 10, Quaternion.identity);
        }
    }

   List<SpatialTagBase> result = new List<SpatialTagBase>(30);
  // SpatialTag[] result = new SpatialTag[500];

   private int last = 0;
    // Update is called once per frame
    void Update()
    {
      //last=  HashManager.GetAllObjectNearbyNoneAloc( result, 5, Vector3.one * 5);
      hashManagerBase.GetAllObjectNearbyNoneAloc( result, 5, Vector3.one * 5,false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
//        for (int i = 0; i <= last; i++)
//        {
//           // Gizmos.DrawWireCube(result[i].Position, Vector3.one * .3f);
//        }
        foreach (var obj in result)
        {
            Gizmos.DrawWireCube(obj.Position, Vector3.one * .3f);
        }
        Gizmos.DrawWireSphere(Vector3.one * 5, 5);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.one * 5,Vector3.one*10);
    }
}