using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    Vector3 m_f;
    public float m_mass = 1;
    Vector3 m_a;
    Vector3 m_v;

    // Start is called before the first frame update
    void Start()
    {
        m_f = Vector3.zero;
        m_a = Vector3.zero;
        m_v = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_f = gravity;
        m_a = m_f / m_mass;
        m_v += m_a * Time.deltaTime;
        transform.position += m_v * Time.deltaTime;
    }
}
