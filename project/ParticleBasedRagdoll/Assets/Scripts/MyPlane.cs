using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlane : MonoBehaviour
{
    public Vector3 m_n; // the plane normal
    public float m_d; // the offset from the origin of the coordinate frame (in the direction of normal)
   
    public float get_signed_distance(Vector3 p)
    {
        return Mathf.Abs(Utils.VecMulVecF(m_n, p) - m_d);
    }

    public float get_distance(Vector3 p)
    {
        return Utils.VecMulVecF(m_n, p) - m_d;
    }

    public void set(Vector3 normal_BF, Vector3 point_BF)
    {
        m_n = normal_BF.normalized;
        m_d = Utils.VecMulVecF(m_n, point_BF);
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
        Vector3 u1, u2, u1Xu2;
        u1 = p2 - p1;
        u2 = p3 - p2;
        u1Xu2 = Vector3.Cross(u1, u2);
        m_n = u1Xu2.normalized;
        m_d = Utils.VecMulVecF(m_n, p1);
    }
}
