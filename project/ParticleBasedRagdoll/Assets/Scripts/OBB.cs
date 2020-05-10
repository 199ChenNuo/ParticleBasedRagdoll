using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBB
{
    public GameObject m_cube;
    public Vector3 center;

    // The location of this oriented bounding boxs center.
    public Vector3 m_T;
    // The orientation of the axes of this oriented bounding box.
    public Quaternion m_R;
    // The extends along this oriented bounding boxs axis.
    public Vector3 m_ext;
    // The extents and the collision develope along this oriented bounding boxs axis.
    public Vector3 m_eps;

    public GameObject cube() { return m_cube; }

    public OBB()
    {
        m_T = Vector3.zero;
        m_R = new Quaternion(0, 0, 0, 1);
        m_ext = m_eps = new Vector3(0.5f, 0.5f, 0.5f);

        m_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        /// if added rigid body, m_cube will fall (unity built-in beheviour)
        // m_cube.AddComponent<Rigidbody>();
    }

    public void xform(Vector3 T, Quaternion R)
    {
        m_T = (R * m_T) + T;
        m_R = R * m_R;
    }

    public void set_center(Vector3 c)
    {
        center = c;
        m_cube.transform.position = center;
    }

    public void set_orientation(float[][] r)
    {
        m_R = Utils.Matrix3x3ToQuaternion(r);
    }

    public void init(float width, float hight, float depth)
    {
        // TODO: implement
        
        Debug.Log("Modify Cube Scale");
        m_cube.transform.localScale = new Vector3(width, hight, depth);
    }

    // TODO: replace Quaternion with Matrix 3x3
    public void init(Vector3 T, Quaternion R, float width, float hight, float depth)
    {
        m_cube.transform.position = T;
        m_cube.transform.rotation = R;
        m_cube.transform.localScale = new Vector3(width, hight, depth);
    }

    
}
