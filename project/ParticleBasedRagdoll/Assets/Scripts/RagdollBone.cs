using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBone
{
    public Particle m_A;
    public Particle m_B;
    public Particle m_C;
    public Particle m_D;

    public bool visualiza_particle;
     LineRenderer line;
     LineRenderer xline;
     LineRenderer yline;
     LineRenderer zline;
     GameObject gb_a;
     GameObject gb_b;
     GameObject gb_c;
     GameObject gb_d;

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
    float cube_x_rot;
    float cube_y_rot;
    float cube_z_rot;


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
        m_cube.GetComponent<MeshRenderer>().material.color = color;
    }

    public void set_cube_size(float x, float y, float z, float scale)
    {
        set_size(new Vector3(x, y, z) * scale);
    }
    public void set_cube_size(float x, float y, float z)
    {
        set_size(new Vector3(x, y, z));
    }
    public void set_size(Vector3 size)
    {
        m_cube.transform.localScale = size;
    }

    public void set_cube_rot(float x_rot, float y_rot, float z_rot)
    {
        cube_x_rot = x_rot;
        cube_y_rot = y_rot;
        cube_z_rot = z_rot;
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

        m_cube_pos_BF = Vector3.zero;
        cube_x_rot = 0;
        cube_y_rot = 0;
        cube_z_rot = 0;

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
            xline = gb_b.AddComponent<LineRenderer>();
            xline.material = Resources.Load<Material>("Materials/x-axis");
            yline = gb_c.AddComponent<LineRenderer>();
            yline.material = Resources.Load<Material>("Materials/y-axis");
            zline = gb_d.AddComponent<LineRenderer>();
            zline.material = Resources.Load<Material>("Materials/z-axis");
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
            line.SetVertexCount(8);
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

        update_cube_position();
    }

    private void update_cube_position()
    {
        Vector3 BF_x = (m_B.position() - m_A.position()).normalized;
        Vector3 BF_z = Vector3.Cross(BF_x, m_D.position() - m_A.position()).normalized;
        Vector3 BF_y = Vector3.Cross(BF_z, BF_x);
        /*
        Vector3 cube_pos_wcs = m_cube_pos_BF;
        cube_pos_wcs = m_coords_bf_to_wcs.xform_point(cube_pos_wcs);
        */
        // m_cube.transform.position = cube_pos_wcs;
        m_cube.transform.position = m_A.position()
            + m_cube_pos_BF.x * BF_x
            + m_cube_pos_BF.y * BF_y
            + m_cube_pos_BF.z * BF_z;
        // m_cube.transform.rotation = m_coords_wcs_to_bf.Q();
        // m_cube.transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), BF_x);
        Quaternion x_rot = Quaternion.AngleAxis(cube_x_rot, BF_x);
        Quaternion y_rot = Quaternion.AngleAxis(cube_y_rot, BF_z);
        Quaternion z_rot = Quaternion.AngleAxis(cube_z_rot, BF_z);

        Quaternion xq = Quaternion.FromToRotation(new Vector3(1, 0, 0), BF_x);
        m_cube.transform.rotation = xq * x_rot * y_rot * z_rot;
    }



    public void update_coordsys()
    {
        Vector3 BF_x = (m_B.position() - m_A.position()).normalized;
        Vector3 BF_z = Vector3.Cross(BF_x, m_D.position() - m_A.position()).normalized;
        Vector3 BF_y = Vector3.Cross(BF_z, BF_x);

        if (visualiza_particle)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            xline.SetWidth(0.1f, 0.01f);
            // xline.SetColors(Color.red, Color.red);
            xline.SetVertexCount(2);
            yline.SetWidth(0.1f, 0.01f);
            // yline.SetColors(Color.yellow, Color.yellow);
            yline.SetVertexCount(2);
            zline.SetWidth(0.1f, 0.01f);
            // zline.SetColors(Color.green, Color.green);
            zline.SetVertexCount(2);
#pragma warning restore CS0618 // 类型或成员已过时
            xline.SetPosition(0, m_A.position());
            xline.SetPosition(1, m_A.position() + 2 * BF_x);
            yline.SetPosition(0, m_A.position());
            yline.SetPosition(1, m_A.position() + 2 * BF_y);
            zline.SetPosition(0, m_A.position());
            zline.SetPosition(1, m_A.position() + 2 * BF_z);
        }


        float[][] tmp_T =
        {
            new float[3],
            new float[3],
            new float[3]
        };
        tmp_T = Utils.Vector3s2Matrix(BF_x, BF_y, BF_z);

        m_coords_wcs_to_bf = new CoorSys(m_A.position(), Utils.Matrix3x3ToQuaternion(tmp_T));
        m_coords_bf_to_wcs = m_coords_wcs_to_bf.inverse();
    }

    public void set_fixed(bool fix)
    {
        isFixed = fix;
    }

    public void set_cube_pos(float x, float y, float z)
    {
        set_cube_pos(new Vector3(x, y, z));
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
        m_owner.add_ragdoll_bone(this);

        update_coordsys();

        Vector3 vec1 = C.position() - A.position();
        Vector3 vec2 = B.position() - A.position();
        Vector3 vec3 = D.position() - A.position();
        Plane tmp_plane = new Plane(A.position(), B.position(), C.position());

        float A_to_B = vec2.magnitude;
        float A_to_C = vec1.magnitude;
        float A_to_D = vec3.magnitude;

        m_mass = A_to_B * A_to_C * A_to_D;

        m_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        m_cube.name = m_name;
    }

    public void set_obb_size(Vector3 size)
    {
        // TODO: implement
    }

    public void set_obb_size(float x, float y, float z)
    {
        set_obb_size(new Vector3(x, y, z));
    }
}
