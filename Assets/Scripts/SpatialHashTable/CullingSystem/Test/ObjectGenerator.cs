using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private Vector3 centerPoint,areaDimensions;

    [SerializeField] private int number;
    [SerializeField] private GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= number; i++)
        {
            var postion=new Vector3( Random.Range(-areaDimensions.x,areaDimensions.x)+centerPoint.x,
                                     Random.Range(-areaDimensions.y,areaDimensions.y)+centerPoint.y,
                                     Random.Range(-areaDimensions.z,areaDimensions.z)+centerPoint.z);
            Instantiate(item, postion, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
