using UnityEngine;

public class MyHingeJoint : MyJoint
{
    Ragdoll m_owner;
    RagdollBone m_A;
    RagdollBone m_B;

    CoorSys m_coords_wcs_to_bf;
    CoorSys m_coords_bf_to_wcs;

    Particle m_hinge_1;
    Particle m_hinge_2;
    Vector3 m_hinge_axis;

    Particle m_pB_1;
    Particle m_pB_2;
    Particle m_pA_1;

    // Stick constraint to satisfy when positive/negtive breach occur
    StickConstraint m_stick_pos;
    StickConstraint m_stick_neg;
    float pos_off;
    float neg_off;

    public int m_choice;

    public Particle hinge_particle1() { return m_hinge_1; }
    public Particle hinge_particle2() { return m_hinge_2; }

    public MyHingeJoint()
    {
        m_hinge_1 = m_hinge_2 = null;
        m_choice = 1;
    }

    public void init(RagdollBone A, RagdollBone B, Particle p1, Particle p2,
        float pos_angle, float neg_angle)
    {
        if (A.isFixed == true && pos_angle == 0 && neg_angle == 0)
        {
            B.isFixed = true;
            // return;
        }
        if (B.isFixed == true && pos_angle == 0 && neg_angle == 0)
        {
            A.isFixed = true;
            // return;
        }
        {
            m_A = A;
            m_B = B;

            m_coords_wcs_to_bf = m_A.m_coords_wcs_to_bf;
            m_coords_bf_to_wcs = m_A.m_coords_bf_to_wcs;
        }

        {
            m_hinge_1 = p1;
            m_hinge_2 = p2;
            m_hinge_axis = (p2.position() - p1.position()).normalized;

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

        m_stick_pos = new StickConstraint();
        m_stick_pos.init(m_A.ParticleA(), m_pB_1);
        m_stick_neg = new StickConstraint();
        m_stick_neg.init(m_pA_1, m_pB_1);

        Vector3 hinge_axis = m_hinge_2.position() - m_hinge_1.position();
        Vector3 boneA_xAxis = (m_hinge_1.position() - m_pA_1.position()).normalized;
        Quaternion pos_q = Quaternion.AngleAxis(Mathf.Abs(pos_angle), hinge_axis);
        Quaternion neg_q = Quaternion.AngleAxis(-Mathf.Abs(neg_angle), hinge_axis);

        Vector3 hingeTopB = m_pB_1.position() - m_hinge_1.position();
        Vector3 pAtoHinge = m_hinge_1.position() - m_pA_1.position();

        Vector3 hingeTopBXAxis = hingeTopB.magnitude * boneA_xAxis;
        Vector3 hingeTopBPos = pos_q * hingeTopBXAxis;
        Vector3 hingetopBNeg = neg_q * hingeTopBXAxis;
        Vector3 pAtopBPos = pAtoHinge + hingeTopBPos;
        Vector3 pAtopBNeg = pAtoHinge + hingetopBNeg;

        Debug.Log(pAtopBPos);
        Debug.Log(pAtopBNeg);

        pos_off = pAtopBPos.magnitude;
        neg_off = pAtopBNeg.magnitude;
        m_stick_pos.SetRestLength(pos_off);
        m_stick_neg.SetRestLength(neg_off);
    }

    public override void satisfy()
    {
        satisfy_type1();
    }


    public void satisfy_type1()
    {
        m_coords_bf_to_wcs = new CoorSys(m_A.m_coord_T, m_A.m_coord_R);
        m_coords_wcs_to_bf = m_coords_bf_to_wcs.inverse();

        Vector3 pB = m_pB_1.position();
        m_coords_wcs_to_bf.xform_point(pB);

        if ((m_pB_1.position() - m_pA_1.position()).magnitude > pos_off)
        {
            m_stick_pos.satisfy();
        }
        if ((m_pB_2.position() - m_pA_1.position()).magnitude > neg_off)
        {
            m_stick_neg.satisfy();
        }

    }


}
