using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public enum StepMethod
    {
        ExplicitEluer = 0,
        ImplicitEuler,
        Midpoint,
        Verlet
    };

    public int implicit_iteration;

    public StepMethod stepMethod;
    public bool m_coupled;      // Boolean flag indicating whether the particle position is coupled. Default is false.
    public Vector3 m_2_old_r;   // position 2 frames ago
    public Vector3 m_old_r;     // old position
    public Vector3 m_init_r;     // initial position
    public Vector3 m_r;             // current position
    public Vector3 m_v;             // current volocity
    public Vector3 m_f;             // current gravity
    public Vector3 m_a;             // current acceleration
    public float m_mass;        // mass, double.MaxValue means fixed particle
    public float m_inv_mass; // inverse mass, 0 means fixed particle
    public bool m_fixed;

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

        stepMethod = StepMethod.ImplicitEuler;
        implicit_iteration = 3;
        m_fixed = false;
    }

    public void set_fixed(bool fix)
    {
        m_fixed = fix;
    }

    public Vector3 position() { return m_r; }
    public void set_init_position(float x, float y, float z)
    {
        set_init_position(new Vector3(x, y, z));
    }
    public void set_init_position(Vector3 r) {
        m_init_r = r;
        m_r = r;
        m_old_r = r;
        m_2_old_r = r;
    }
    public Vector3 init_position() { return m_init_r; }
    public void set_position(Vector3 r) { m_r = r; }
    public void set_position(float x, float y, float z) { m_r = new Vector3(x, y, z); }

    public float mass() { return m_mass; }
    public void set_mass(float mass) { m_mass = mass; m_inv_mass = 1 / m_mass; }

    public void init(Vector3 position)
    {
        m_r = position;
        m_init_r = position;
        m_old_r = position;
        m_2_old_r = position;
    }

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


    public void add_force(Vector3 gravity, float delta_t)
    {
        if (m_fixed)
        {
            m_r = m_init_r;
        }
        else
        {
            switch (stepMethod)
            {
                case StepMethod.ExplicitEluer:
                    explicitEuler(gravity, delta_t);
                    break;
                case StepMethod.ImplicitEuler:
                    implicitEuler(gravity, delta_t);
                    break;
                case StepMethod.Midpoint:
                    midpoint(gravity, delta_t);
                    break;
                case StepMethod.Verlet:
                default:
                    verlet(gravity, delta_t);
                    break;
            }
        }
    }

    private void explicitEuler(Vector3 gravity, float delta_t)
    {
        // store these values in case the stepMethod is changed during simulation
        m_2_old_r = m_old_r;
        m_old_r = m_r;

        m_f = gravity * m_mass;
        m_a = m_f * m_inv_mass;
        m_v += m_a * delta_t;
        m_r += m_v * delta_t;
    }

    private void implicitEuler(Vector3 gravity, float delta_t)
    {
        Vector3 tmp_r = m_r;
        // v' = v0 + f/m * t
        Vector3 tmp_v = m_v + gravity * m_inv_mass * delta_t;
        for (int i = 0; i < implicit_iteration; ++i)
        {
            tmp_r = m_r + tmp_v * delta_t;
            tmp_v = (tmp_r - m_r) / delta_t;
        }

        m_2_old_r = m_old_r;
        m_old_r = m_r;
        m_r = tmp_r;
        m_v = tmp_v;
    }

    private void midpoint(Vector3 gravity, float delta_t)
    {
        Vector3 mid_v = m_v + gravity * delta_t / 2;
        m_2_old_r = m_old_r;
        m_old_r = m_r;
        m_r += delta_t * mid_v;

        // for tests
        m_v = mid_v;
    }

    private void verlet(Vector3 gravity, float delta_t)
    {
        m_a = gravity;
        m_2_old_r = m_old_r;
        m_old_r = m_r;
        m_r = 2 * m_old_r - m_2_old_r + m_a * delta_t * delta_t;

        // for test 
        m_v = (m_r - m_old_r) / delta_t;
    }
}
