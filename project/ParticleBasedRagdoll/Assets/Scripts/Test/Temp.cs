using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{



    // public GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 center = new Vector3(1, 2, 0);
        Vector3 axis = new Vector3(1, 0, 0);
        float theta = 1;
        Vector3 point = Quaternion.AngleAxis(theta, axis) * (new Vector3(1, 3, 0) - center);
        Vector3 result = center + point;
        Debug.Log(result);
        */
        Vector3 v = new Vector3(1, 1, 1);
        Debug.Log(Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * v);
        Debug.Log(Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * v);
        Debug.Log(Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * v);

        Debug.Log(Vector3.Angle(new Vector3(1, 1, 1), new Vector3(-1, 1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
