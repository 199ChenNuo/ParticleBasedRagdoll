using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public bool m_coupled;      // Boolean flag indicating whether the particle position is coupled. Default is false.
    public Vector3 m_old_r;     // old position
    public Vector3 m_init_r;     // initial position
    public Vector3 m_r;             // current position
    public Vector3 m_v;             // current volocity
    public Vector3 m_f;             // current force
    public Vector3 m_a;             // current acceleration
    public double m_mass;        // mass, double.MaxValue means fixed particle
    public double m_inv_mass; // inverse mass, 0 means fixed particle

    public Particle()
    {
        m_coupled = false;
        m_r = Vector3.zero;
        m_old_r = Vector3.zero;
        m_init_r = Vector3.zero;
        m_v = Vector3.zero;
        m_f = Vector3.zero;
        m_a = Vector3.zero;
        m_mass = 1;
        m_inv_mass = 1;
    }

    public Vector3 position() { return m_r; }
    public void set_position(Vector3 r) { m_r = r; }

    public void bind(Vector3 r)
    {
        m_init_r = r;
        m_old_r = r;
        m_coupled = true;
    }

    public void release()
    {
        if (m_coupled)
        {
            m_r = m_init_r;
        }
        m_coupled = false;
    }

}
