using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlane : MonoBehaviour
{
    public Vector3 m_n; // the plane normal
    public double m_d; // the offset from the origin of the coordinate frame (in the direction of normal)
   
    public float get_signed_distance(Vector3 p)
    {
        // TODO: implement
        return 1.0f;
    }

    public float get_distance(Vector3 p)
    {
        // TODO: implement
        return 1.0f;
    }

    public void set(Vector3 normal_BF, Vector3 point_BF)
    {
        // TODO: implement
    }

    /**
* constructor.
*
* @param p1   A point in the plane.
* @param p2   A point in the plane.
* @param p3   A point in the plane.
*/
    public void set(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // TODO: implement
    }
}
