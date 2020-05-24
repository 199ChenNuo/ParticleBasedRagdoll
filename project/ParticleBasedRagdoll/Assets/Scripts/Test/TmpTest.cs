using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpTest : MonoBehaviour
{
    GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
