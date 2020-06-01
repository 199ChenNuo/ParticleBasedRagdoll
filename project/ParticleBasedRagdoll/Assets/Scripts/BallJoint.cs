﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJoint : MyJoint
{
    public RagdollBone m_A;
    public RagdollBone m_B;

    public CoorSys m_coords_wcs_to_bf;
    public CoorSys m_coords_bf_to_wcs;    

    /// <summary>
    /// the particle that is shared between boneA and boneB in ball joint
    /// </summary>
    public Particle m_ball_particle;
    /// <summary>
    /// the particle in boneA to correct ball joint violation
    /// </summary>
    public Particle m_pA_1;
    /// <summary>
    /// the particle in boneB to detect wether ball joint is violated
    /// </summary>
    public Particle m_pB_ref;

    /// <summary>
    ///     angle limit for ball joint
    /// </summary>
    public double m_angle_limit;

    public StickConstraint m_stick_ref;

    public float m_min_length_ref;

    public Vector3 m_plane_normal_BF;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="A">Bone A</param>
    /// <param name="B">Bone B</param>
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

            m_coords_bf_to_wcs = m_A.m_coords_bf_to_wcs;
            m_coords_wcs_to_bf = m_A.m_coords_wcs_to_bf;
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
            m_plane_normal_BF = plane_normal;
            m_coords_wcs_to_bf.xform_vector(m_plane_normal_BF);
            
            m_min_length_ref = Vector3.Project(m_pB_ref.position() - m_ball_particle.position(), plane_normal).magnitude;
        }
    }

    public override void satisfy()
    {
        m_coords_bf_to_wcs = m_A.m_coords_bf_to_wcs;
        m_coords_wcs_to_bf = m_A.m_coords_wcs_to_bf;

        Vector3 plane_normal_WCS = m_plane_normal_BF;
        m_coords_bf_to_wcs.xform_vector(plane_normal_WCS);

        float current_angle = Vector3.Angle(m_pB_ref.position() - m_ball_particle.position(), plane_normal_WCS);

        if (Mathf.Abs(current_angle) > m_angle_limit)
        {
            float current_dist = Vector3.Project(m_pB_ref.position() - m_ball_particle.position(), plane_normal_WCS).magnitude;
            m_stick_ref.SetRestLength((m_pB_ref.position() - m_pA_1.position()).magnitude + (m_min_length_ref - current_dist));
            m_stick_ref.satisfy();
        }
    }
}
