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

    public Vector3 position()
    {
        return get_obb_in_WCS().center;
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

        for (int i = 0; i < 6; ++i)
        {
            m_stick[i] = new StickConstraint();
        } 

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


        float line1 = (A.position() - B.position()).magnitude;

        Vector3 vec1 = C.position() - A.position();
        Vector3 vec2 = B.position() - A.position();
        Vector3 vecProj = Vector3.Project(vec1, vec2);
        float line2 = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));


        Vector3 vec3 = D.position() - A.position();

        // float line3 = Vector3.Project(Vector3.Cross(vec1, vec2), vec3).magnitude;
        Plane tmp_plane = new Plane(A.position(), B.position(), C.position());
        float line3 = tmp_plane.GetDistanceToPoint(D.position());

        float A_to_B = vec2.magnitude;
        float A_to_C = vec1.magnitude;
        float A_to_D = vec3.magnitude;

        m_mass = A_to_B * A_to_C * A_to_D;

        m_obb = new OBB();

        m_center = (A.position() + B.position() + C.position() + D.position()) / 4;
        m_coords_wcs_to_bf.xform_point(m_center);

        // TODO: replace Quaternion with Matrix 3x3
        Quaternion r = new Quaternion(0, 0, 0, 1);
        m_obb.init(m_center, r, line1,line3, line2);
    }

    void set_coord()
    {
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    void update_coordsys()
    {
        // TODO: implement
        Vector3 X = (m_B.position() - m_A.position()).normalized;
        Vector3 Z = (Utils.module(X, m_D.position())).normalized;
        Vector3 Y = (Utils.module(Z, X));
        // TODO: replace m_coord_R with Matrix 3x3 or write a function to convert Matrix and Quaternion
        m_coord_R.Set(0, 0, 0, 1);

        m_coord_T = m_A.position();
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    public void set_obb_size(float width, float hight, float depth)
    {
        m_obb.init(width, hight, depth);
        m_mass = width * hight * depth;
    }

    public void set_obb_center_wcs(Vector3 center)
    {
        Vector3 c = center;
        m_coords_wcs_to_bf.xform_point(c);
        m_obb.set_center(c);
    }

    public void set_obb_orientation_wcs(float[][] ori)
    {
        float[][] r = ori;
        m_coords_wcs_to_bf.xform_matrix(r);
        m_obb.set_orientation(r);
    }
}
