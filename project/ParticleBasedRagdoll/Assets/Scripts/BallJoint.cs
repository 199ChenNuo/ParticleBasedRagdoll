using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJoint : MyJoint
{
    public RagdollBone m_A;
    public RagdollBone m_B;

    public CoorSys m_coords_wcs_to_bf;
    public CoorSys m_coords_bf_to_wcs;    

    public Particle m_ball_particle;
    public Particle m_pA;
    public Particle m_pB;
    public Particle m_pA_1;
    public Particle m_pB_ref;

    public double m_angle_limit;

    public MyPlane m_plane;

    public StickConstraint m_stick_ref;

    public float m_min_length_ref;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="A">Bone A</param>
    /// <param name="B">Bone N</param>
    /// <param name="p">The shared ball particle</param>
    /// <param name="ref_p">he reference particle used to detect and correct breaches(in  bone B)</param>
    /// <param name="angle">The angle from the plane to the cone, Defines the width of the  reach cone</param>
    /// <param name="plane_normal">Plane normal defining the direction of the cone</param>
    public void init(RagdollBone A, RagdollBone B, Particle p, Particle ref_p,
        float angle, Vector3 plane_normal)
    {
        {
            m_A = A;
            m_B = B;

            m_coords_wcs_to_bf = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
            m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
        }

        {
            m_stick_ref = new StickConstraint();
            m_ball_particle = p;
            m_angle_limit = Mathf.Abs(angle);
            m_pB_ref = ref_p;

            m_pA_1 = (A.ParticleA() == p) ? A.ParticleA() : A.ParticleB();
            m_stick_ref.init(m_pA_1, m_pB_ref);
        }

        {
            m_plane = new MyPlane();
            
            Vector3 plane_point_BF = m_ball_particle.position();
            m_coords_wcs_to_bf.xform_point(plane_point_BF);
            Vector3 plane_nornal_BF = plane_normal;
            m_coords_wcs_to_bf.xform_vector(plane_nornal_BF);

            m_plane.set(plane_normal, m_ball_particle.position());

            m_min_length_ref = Mathf.Abs(Mathf.Sin((Mathf.PI / 2) - angle) * (m_pB_ref.position() - m_ball_particle.position()).magnitude);
        }
    }

    public override void satisfy()
    {

        m_coords_bf_to_wcs = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
        m_coords_wcs_to_bf = m_coords_bf_to_wcs.inverse();

        Vector3 p = m_pB_ref.position();
        m_coords_wcs_to_bf.xform_point(p);

        double dist = m_plane.get_signed_distance(p);

        if (dist < m_min_length_ref)
        {
            m_stick_ref.SetRestLength((m_pB_ref.position()- m_pA_1.position()).magnitude + (float)(m_min_length_ref - dist));
            m_stick_ref.satisfy();
            m_A.set_obb_center_wcs();
            m_B.set_obb_center_wcs();
        }
    }
}
