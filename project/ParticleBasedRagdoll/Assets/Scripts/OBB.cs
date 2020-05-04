using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBB : MonoBehaviour
{
    public GameObject m_cube;
    public Vector3 center;

    public void xform(Vector3 T, Quaternion R)
    {
        // TODO: implement
    }

    public void set_center(Vector3 c)
    {
        m_cube.transform.position = center;
    }

    public void set_orientation(float[][] r)
    {

    }

    public void init(float width, float hight, float depth)
    {
        // TODO: implement
        m_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        m_cube.transform.localScale = new Vector3(width, hight, depth);
    }

    // TODO: replace Quaternion with Matrix 3x3
    public void place(Vector3 T, Quaternion R)
    {
        // TODO: implement
    }
}
