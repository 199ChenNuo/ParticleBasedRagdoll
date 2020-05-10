using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBone:MonoBehaviour
{
    public Particle m_A;
    public Particle m_B;
    public Particle m_C;
    public Particle m_D;

    public GameObject gb_a;
    public GameObject gb_b;
    public GameObject gb_c;
    public GameObject gb_d;

    public string m_name;
    public string name() { return m_name; }
    public void setname(string name) { m_name = name; }

    LineRenderer line;

    public bool visualiza_particle;

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

    public Particle ParticleA() { return m_A; }
    public Particle ParticleB() { return m_B; }
    public Particle ParticleC() { return m_C; }
    public Particle ParticleD() { return m_D; }

    public OBB get_OBB() { return m_obb; }

    public void add_force(Vector3 force, float delta_t)
    {
        m_A.add_force(force, delta_t);
        m_B.add_force(force, delta_t);
        m_C.add_force(force, delta_t);
        m_D.add_force(force, delta_t);

        set_obb_center_wcs();

    }

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
    public bool is_connect(RagdollBone bone)
    {
        return m_ragdoll_bones.Contains(bone);
    }

    public RagdollBone(bool visualize = false)
    {
        m_A = m_B = m_C = m_D = null;
        m_owner = null;
        m_color = Color.blue;
        m_fixed = false;
        m_mass = 1;
        m_ragdoll_bones = new List<RagdollBone>();
        visualiza_particle = visualize;

        if (visualiza_particle)
        {
            gb_a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gb_b = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gb_c = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gb_d = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            gb_a.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
            gb_b.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
            gb_c.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
            gb_d.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");

            gb_a.name = m_name + "-p_A";
            gb_b.name = m_name + "-p_B";
            gb_c.name = m_name + "-p_C";
            gb_d.name = m_name + "-p_D";

            line = gb_a.AddComponent<LineRenderer>();
            line.material = Resources.Load<Material>("Materials/AlphaMaterial");
        }
    }

    void set_position()
    {
        // TODO: find out why boneB in BallJointTest won't move
        /*
        if (m_name == "boneB")
        {
            for (int i = 0; i < 3; ++i)
            {
                foreach (StickConstraint stick in m_stick)
                {
                    Debug.Log("================" + stick.name() + "===============");
                    Debug.Log(stick.m_length);
                    Debug.Log((stick.m_A.position() - stick.m_B.position()).magnitude);
                    stick.satisfy();
                }
            }
        }
        */

        if (visualiza_particle)
        {
            gb_a.transform.position = m_A.position();
            gb_b.transform.position = m_B.position();
            gb_c.transform.position = m_C.position();
            gb_d.transform.position = m_D.position();


#pragma warning disable CS0618 // 类型或成员已过时
            line.SetVertexCount(9);
#pragma warning restore CS0618 // 类型或成员已过时
            line.SetPosition(0, m_A.position());
            line.SetPosition(1, m_B.position());
            line.SetPosition(2, m_C.position());
            line.SetPosition(3, m_D.position());
            line.SetPosition(4, m_A.position());
            line.SetPosition(5, m_C.position());
            line.SetPosition(6, m_D.position());
            line.SetPosition(7, m_B.position());

#pragma warning disable CS0618 // 类型或成员已过时
            line.SetWidth(0.01f, 0.01f);
#pragma warning restore CS0618 // 类型或成员已过时
        }

    }

    public void set_visualize_particle(bool flag)
    {
        visualiza_particle = flag;

    }

    public void draw_particle()
    {


    }

    public void update_position()
    {
        set_position();
    }

    public void init(Ragdoll owner, Particle A, Particle B, Particle C, Particle D, string name = "bone")
    {
        m_owner = owner;
        m_A = A;
        m_B = B;
        m_C = C;
        m_D = D;
        m_name = name;

        if (visualiza_particle)
        {
            gb_a.name = m_name + "-p_A";
            gb_b.name = m_name + "-p_B";
            gb_c.name = m_name + "-p_C";
            gb_d.name = m_name + "-p_D";
        }

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
        m_stick[0].setname(m_name + "-0");
        m_stick[1].setname(m_name + "-1");
        m_stick[2].setname(m_name + "-2");
        m_stick[3].setname(m_name + "-3");
        m_stick[4].setname(m_name + "-4");
        m_stick[5].setname(m_name + "-5");



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
        m_obb.init(m_center, r, line1, line3, line2);
    }

    void set_coord()
    {
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    public void update_coordsys()
    {
        Vector3 X = (m_B.position() - m_A.position()).normalized;
        Vector3 Z = (Utils.module(X, m_D.position())).normalized;
        Vector3 Y = (Utils.module(Z, X));

        float[][] tmp_r =
        {
            new float[3],
            new float[3],
            new float[3]
        };
        tmp_r[0][0] = X.x;
        tmp_r[0][1] = X.y;
        tmp_r[0][2] = X.z;
        tmp_r[1][0] = Y.x;
        tmp_r[1][1] = Y.y;
        tmp_r[1][2] = Y.z;
        tmp_r[1][0] = Y.x;
        tmp_r[2][0] = Z.x;
        tmp_r[2][1] = Z.y;
        tmp_r[2][2] = Z.z;

        m_coord_R = Utils.Matrix3x3ToQuaternion(tmp_r);

        m_coord_T = m_A.position();
        m_coords_wcs_to_bf = new CoorSys(m_coord_T, m_coord_R);
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    public void set_obb_size(float width, float hight, float depth)
    {
        m_obb.init(width, hight, depth);
        m_mass = width * hight * depth;
    }

    public void set_obb_center_wcs()
    {
        Vector3 center = (m_A.position() + m_B.position() + m_C.position() + m_D.position()) / 4;
        // m_coords_wcs_to_bf.xform_point(center);
        m_obb.set_center(center);
    }

    public void set_obb_orientation_wcs(float[][] ori)
    {
        float[][] r = ori;
        m_coords_wcs_to_bf.xform_matrix(r);
        m_obb.set_orientation(r);
    }
}
