using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public Particle m_A;
    public Particle m_B;
    public Particle m_C;
    public Particle m_D;

    public bool visualiza_particle;
    public  LineRenderer line;
    public GameObject gb_a;
    public GameObject gb_b;
    public GameObject gb_c;
    public GameObject gb_d;

    public bool isFixed;

    public string m_name;
    public string Name() { return m_name; }
    public void setname(string name) { m_name = name; }

    public Ragdoll m_owner;
    public List<StickConstraint> m_stick = new List<StickConstraint>(new StickConstraint[6]);


    public Vector3 m_coord_T;
    // TODO: replace Quaternion with Matrix 3x3
    public Quaternion m_coord_R;

    public CoorSys m_coords_wcs_to_bf;
    public CoorSys m_coords_bf_to_wcs;

    // the positio of cube in BF
    public Vector3 m_cube_pos_BF;
    public GameObject m_cube;
    public Color m_color;


    public List<RagdollBone> m_ragdoll_bones;

    public double m_mass;
    public bool m_fixed;

    public Particle ParticleA() { return m_A; }
    public Particle ParticleB() { return m_B; }
    public Particle ParticleC() { return m_C; }
    public Particle ParticleD() { return m_D; }
    
    public GameObject cube() { return m_cube; }

    public void add_force(Vector3 force, float delta_t)
    {
        m_A.add_force(force, delta_t);
        m_B.add_force(force, delta_t);
        m_C.add_force(force, delta_t);
        m_D.add_force(force, delta_t);
    }

    public void connect(Ragdoll owner)
    {
        m_owner = owner;
    }
    public void disconnect()
    {
        m_owner = null;
    }

    public Vector3 position()
    {
        Vector3 cube_pos_wcs = m_cube_pos_BF;
        m_coords_bf_to_wcs.xform_point(cube_pos_wcs);
        return cube_pos_wcs;
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

    public void set_color(Color color)
    {
        m_color = color;
    }


    public Bone(bool visualize = false)
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

    public void update_position()
    {
        if (isFixed)
        {
            m_A.set_position(m_A.init_position());
            m_B.set_position(m_B.init_position());
            m_C.set_position(m_C.init_position());
            m_D.set_position(m_D.init_position());
        }



        update_coordsys();
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

        set_cube_position();
    }

    public void set_cube_position()
    {
        Vector3 cube_pos_wcs = m_cube_pos_BF;

        cube_pos_wcs = m_coords_bf_to_wcs.xform_point(cube_pos_wcs);
        Debug.Log("cube_ops_wcs: " + cube_pos_wcs.ToString());
        m_cube.transform.position = cube_pos_wcs;
        m_cube.transform.rotation = m_coords_wcs_to_bf.Q();
    }

    public void update_coordsys()
    {
        Debug.Log("==============");


        Vector3 BF_x = (m_B.position() - m_A.position()).normalized;
        Vector3 BF_z = Vector3.Cross(BF_x, m_D.position() - m_A.position()).normalized;
        Vector3 BF_y = -Vector3.Cross(BF_z, BF_x);

        float[][] tmp_T =
        {
            new float[3],
            new float[3],
            new float[3]
        };
        tmp_T = Utils.Vector3s2Matrix(BF_x, BF_y, BF_z);

        Debug.Log(tmp_T.ToString());

        m_coords_wcs_to_bf = new CoorSys(m_A.position(), Utils.Matrix3x3ToQuaternion(tmp_T));
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    public void set_fixed(bool fix)
    {
        isFixed = fix;
    }

    public void set_cube_pos(Vector3 pos)
    {
        m_cube_pos_BF = pos;
    }

    public void init(Ragdoll owner, Particle A, Particle B, Particle C, Particle D, string name = "bone")
    {
        m_owner = owner;
        m_A = A;
        m_B = B;
        m_C = C;
        m_D = D;
        m_name = name;

        isFixed = false;

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

        // m_obb = new OBB();
        m_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);  
    }
}
