using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpTest : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        Vector3 p1 = new Vector3(0, 2, 0);
        Vector3 p2 = new Vector3(1, 2.9999f, 0);

        Debug.Log(Vector3.Project(p2, p1).ToString());
        Debug.Log(Vector3.Project(p2, p1).y.ToString());

        float val1 = 1.1f;
        float val2= 1.11f;
        float val3 = 1.111f;
        float val4 = 1.1111f;
        Debug.Log(val1.ToString());
        Debug.Log(val2.ToString());
        Debug.Log(val3.ToString());
        Debug.Log(val4.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
