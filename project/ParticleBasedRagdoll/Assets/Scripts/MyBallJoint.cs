using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBallJoint : MonoBehaviour
{
    RagdollBone m_A;
    RagdollBone m_B;

    Particle m_ball_particle;
    Particle m_pA;
    Particle m_pB;

    float m_angle_limit;

    Vector3 m_plane_n_BF;

    StickConstraint m_stick;

    public void set_angle_limit(float limit)
    {
        m_angle_limit = limit;
    }

    public void init(Ragdoll owner, RagdollBone A, RagdollBone B,
    Particle ball_P, Particle A_p, Particle B_p, float angle_limit, Vector3 plane_N)
    {
        m_A = A;
        m_B = B;
        m_ball_particle = ball_P;
        m_pA = A_p;
        m_pB = B_p;
        m_angle_limit = angle_limit;
        m_plane_n_BF = plane_N;

        m_stick = new StickConstraint();
        m_stick.init(m_pA, m_pB);

        owner.add_constraint(this);
    }

    public void satisfy()
    {
        Vector3 plane_n_WCS = m_A.x_axis() * m_plane_n_BF.x
        + m_A.y_axis() * m_plane_n_BF.y + m_A.z_axis() * m_plane_n_BF.z;
        // Debug.Log(plane_n_WCS);

        float current_angle = Vector3.Angle(m_pB.position() - m_ball_particle.position(), plane_n_WCS);
        if (current_angle > 90)
        {
            current_angle = 180 - current_angle;
        }
        // Debug.Log(current_angle);
        if (current_angle > m_angle_limit)
        {
            Vector3 min_p_b = (Quaternion.AngleAxis(m_angle_limit, plane_n_WCS) * plane_n_WCS).normalized * (m_pB.init_position() - m_ball_particle.init_position()).magnitude;
            float min_len = (min_p_b - m_pA.position()).magnitude;
            m_stick.SetRestLength(min_len);
            // Debug.Log(min_len - (m_pB.position() - m_pA.position()).magnitude);
            m_stick.satisfy();
        }

        m_A.damp(0.5f);
        m_B.damp(0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
