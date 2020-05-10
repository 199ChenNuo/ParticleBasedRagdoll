using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJoint : Joint
{
    public RagdollBone m_A;
    public RagdollBone m_B;

    public CoorSys m_coords_wcs_to_bf;
    public CoorSys m_coords_bf_to_wcs;

    public Particle m_hinge_1;
    public Particle m_hinge_2;
    public Particle m_pB_1;
    public Particle m_pB_2;
    public Particle m_pA_1;

    // Stick constraint to satisfy when positive/negtive breach occur
    public StickConstraint m_stick_pos;
    public StickConstraint m_stick_neg;

    public Vector3 m_rotation_axis;

    public MyPlane m_plane_pos;
    public MyPlane m_plane_neg;
    public MyPlane m_plane_B;

    public Vector3 m_pos_point_1;
    public Vector3 m_pos_point_2;
    public Vector3 m_neg_point_1;
    public Vector3 m_neg_point_2;

    public int m_choice;

    public Particle hinge_particle1() { return m_hinge_1; }
    public Particle hinge_particle2() { return m_hinge_2; }

    public HingeJoint()
    {
        m_hinge_1 = m_hinge_2 = null;
        m_choice = 2;
    }

    public void init(RagdollBone A, RagdollBone B, Particle p1, Particle p2,
        float pos_angle, float neg_angle)
    {
        {
            m_A = A;
            m_B = B;

            m_coords_wcs_to_bf = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
            m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
        }

        {
            m_hinge_1 = p1;
            m_hinge_2 = p2;

            // Find the particles in Bone B that is not part of the hinge joint .
            if (B.ParticleA() != p1 && B.ParticleA() != p2)
            {
                m_pB_1 = B.ParticleA();
                m_pB_2 = (B.ParticleB() != p1 && B.ParticleB() != p2)
                    ? B.ParticleB()
                    : B.ParticleC();
                if (m_pB_2 != B.ParticleB())
                {
                    m_pB_2 = (B.ParticleC() != p1 && B.ParticleC() != p2)
                        ? B.ParticleC()
                        : B.ParticleD();
                }
            }
            else if (B.ParticleB() != p1 && B.ParticleB() != p2)
            {
                m_pB_1 = B.ParticleB();
                m_pB_2 = (B.ParticleC() != p1 && B.ParticleC() != p2)
                    ? B.ParticleC()
                    : B.ParticleD();
            }
            else
            {
                m_pB_1 = B.ParticleC();
                m_pB_2 = B.ParticleD();
            }

            if (A.ParticleA() != p1 && A.ParticleA() != p2)
            {
                m_pA_1 = A.ParticleA();
            }
            else if (A.ParticleB() != p1 && A.ParticleB() != p2)
            {
                m_pA_1 = A.ParticleB();
            }
            else
            {
                m_pA_1 = A.ParticleC();
            }
        }

        {
            m_rotation_axis = m_hinge_2.position() - m_hinge_1.position();

            float[][] R1 = Utils.Ru(pos_angle, m_rotation_axis);
            m_pos_point_1 = Utils.MatrixMulVector(R1, (m_pB_1.position() - m_hinge_1.position())) + m_hinge_1.position();
            m_pos_point_2 = Utils.MatrixMulVector(R1, (m_pB_2.position() - m_hinge_1.position())) + m_hinge_1.position();

            float[][] R2 = Utils.Ru(-neg_angle, m_rotation_axis);
            m_neg_point_1 = Utils.MatrixMulVector(R2, (m_pB_1.position() - m_hinge_1.position())) + m_hinge_1.position();
            m_neg_point_2 = Utils.MatrixMulVector(R2, (m_pB_2.position() - m_hinge_1.position())) + m_hinge_1.position();
        }

        {
            m_stick_pos.init(m_pA_1, m_pB_1);
            m_stick_pos.SetRestLength((m_pos_point_1 - m_pA_1.position()).magnitude);

            m_stick_neg.init(m_pA_1, m_pB_1);
            m_stick_neg.SetRestLength((m_neg_point_1 - m_pA_1.position()).magnitude);

        }

        {
            Vector3 h1 = m_hinge_1.position();
            Vector3 h2 = m_hinge_2.position();
            Vector3 pB = m_pB_1.position();

            m_coords_bf_to_wcs = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
            m_coords_wcs_to_bf = m_coords_bf_to_wcs.inverse();

            m_coords_wcs_to_bf.xform_point(m_pos_point_1);
            m_coords_wcs_to_bf.xform_point(m_pos_point_2);
            m_coords_wcs_to_bf.xform_point(m_neg_point_1);
            m_coords_wcs_to_bf.xform_point(m_neg_point_2);
            m_coords_wcs_to_bf.xform_point(h1);
            m_coords_wcs_to_bf.xform_point(h2);
            m_coords_wcs_to_bf.xform_point(pB);

            m_plane_pos.set(h1, h2, m_pos_point_1);
            m_plane_neg.set(h1, h2, m_neg_point_1);
            m_plane_B.set(h1, h2, pB);
        }
    }

    public override void satisfy()
    {
        switch (m_choice)
        {
            case 1:
                {
                    satisfy_type1();
                    break;
                }
            case 2:
            default:
                {

                    satisfy_type2();
                    break;
                }
        }
    }


    public void satisfy_type1()
    {
        m_coords_bf_to_wcs = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
        m_coords_wcs_to_bf = m_coords_bf_to_wcs.inverse();

        Vector3 pB = m_pB_1.position();
        m_coords_wcs_to_bf.xform_point(pB);

        if (m_plane_pos.get_signed_distance(pB) > 0 && m_plane_B.get_signed_distance(pB) > 0)
        {
            Vector3 new_pos_1 = m_pos_point_1;
            Vector3 new_pos_2 = m_pos_point_2;
            m_coords_bf_to_wcs.xform_point(new_pos_1);
            m_coords_bf_to_wcs.xform_point(new_pos_2);
            m_pB_1.set_position(new_pos_1);
            m_pB_2.set_position(new_pos_2);
        }
        else if (m_plane_neg.get_signed_distance(pB) < 0 && m_plane_B.get_signed_distance(pB) < 0)
        {
            Vector3 new_pos_1 = m_neg_point_1;
            Vector3 new_pos_2 = m_neg_point_2;
            m_coords_bf_to_wcs.xform_point(new_pos_1);
            m_coords_bf_to_wcs.xform_point(new_pos_2);
            m_pB_1.set_position(new_pos_1);
            m_pB_2.set_position(new_pos_2);
        }
    }

    public void satisfy_type2()
    {
        m_coords_bf_to_wcs = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
        m_coords_wcs_to_bf = m_coords_bf_to_wcs.inverse();

        Vector3 pB = m_pB_1.position();
        m_coords_wcs_to_bf.xform_point(pB);

        if (m_plane_pos.get_signed_distance(pB) > 0 && m_plane_B.get_signed_distance(pB)>0)
        {
            m_stick_pos.Satisfy();
        }
        else if(m_plane_neg.get_signed_distance(pB) < 0 && m_plane_B.get_signed_distance(pB) < 0)
        {
            m_stick_neg.Satisfy();
        }
    }
}
