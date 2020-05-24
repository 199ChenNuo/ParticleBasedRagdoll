using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 p1 = new Vector3(0, 0, 0);
        Vector3 p2 = new Vector3(2, 0, 0);
        Vector3 p3 = new Vector3(1, Mathf.Sqrt(3), 0);


        Vector3 BF_x = (p2 - p1).normalized;
        /*
        Vector3 help_vec = (p3 - p1);
        Vector3 BF_z = help_vec - BF_x / (Vector3.Dot(BF_x, help_vec));
        BF_z = BF_z.normalized;
        Vector3 BF_y = Vector3.Cross(BF_x, BF_z).normalized;
        */
        Vector3 BF_z = Vector3.Cross(BF_x, p3 - p1).normalized;
        Vector3 BF_y = Vector3.Cross(BF_z, BF_x);
        Debug.Log(BF_x.ToString());
        Debug.Log(BF_y.ToString());
        Debug.Log(BF_z.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
