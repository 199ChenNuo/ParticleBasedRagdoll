using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBone : MonoBehaviour
{
    public Particle m_A;
    public Particle m_B;
    public Particle m_C;
    public Particle m_D;

    public Ragdoll m_owner;
    public List<StickConstraint> m_stick = new List<StickConstraint>(new StickConstraint[6]);

    public Vector3 m_coord_T;
    // TODO: replace Quaternion with Matrix 3x3
    public Quaternion m_coord_R;

    public CoorSys m_coords_wcs_to_bf;
    public CoorSys m_coords_bf_to_wcs;

    public OBB m_obb;

    public Vector3 m_center;
    public Color m_color;

    public List<RagdollBone> m_ragdoll_bones;

    public double m_mass;
    public bool m_fixed;
    public string m_name;

    public Particle ParticleA() { return m_A; }
    public Particle ParticleB() { return m_B; }
    public Particle ParticleC() { return m_C; }
    public Particle ParticleD() { return m_D; }

    public void connect(Ragdoll owner)
    {
        m_owner = owner;
    }
    public void disconnect()
    {
        m_owner = null;
    }

    public OBB get_obb_in_WCS()
    {
        OBB tmp = m_obb;
        tmp.xform(m_coord_T, m_coord_R);
        return tmp;
    }

    public void connect(RagdollBone bone)
    {
        m_ragdoll_bones.Add(bone);
    }
    public void disconnect(RagdollBone bone)
    {
        m_ragdoll_bones.Remove(bone);
    }

    public RagdollBone()
    {
        m_A = m_B = m_C = m_D = null;
        m_owner = null;
        m_color = Color.blue;
        m_fixed = false;
        m_mass = 1;
    }

    public void init(Ragdoll owner, Particle A, Particle B, Particle C, Particle D)
    {
        m_owner = owner;
        m_A = A;
        m_B = B;
        m_C = C;
        m_D = D;

        m_stick[0].init(A, B);
        m_stick[1].init(A, C);
        m_stick[2].init(A, D);
        m_stick[3].init(B, C);
        m_stick[4].init(B, D);
        m_stick[5].init(C, D);

        for (int i = 0; i < 6; ++i)
        {
            m_owner.add_constraint(m_stick[i]);
        }

        update_coordsys();

        float A_to_B = (B.m_r - A.m_r).magnitude;
        float A_to_C = (C.m_r - A.m_r).magnitude;
        float A_to_D = (D.m_r - A.m_r).magnitude;

        set_obb_size(A_to_B, A_to_C, A_to_D);

        m_center = (A.m_r + B.m_r + C.m_r + D.m_r) / 4;
        m_coords_wcs_to_bf.xform_point(m_center);

        // TODO: replace Quaternion with Matrix 3x3
        Quaternion r = new Quaternion(1, 1, 1, 0);
        m_obb.place(m_center, r);
    }

    void set_coord()
    {
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    void update_coordsys()
    {
        // TODO: implement
        Vector3 X = (m_B.m_r - m_A.m_r).normalized;
        Vector3 Z = (Utils.module(X, m_D.m_r)).normalized;
        Vector3 Y = (Utils.module(Z, X));
        // TODO: replace m_coord_R with Matrix 3x3 or write a function to convert Matrix and Quaternion
        m_coord_R.Set(1, 1, 1, 0);

        m_coord_T = m_A.m_r;
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    void set_obb_size(float width, float hight, float depth)
    {
        // TODO: implement
    }
}
