using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneVisualize : MonoBehaviour
{
    public Particle A;
    public Particle B;
    public Particle C;
    public Particle D;

    public GameObject gb_a;
    public GameObject gb_b;
    public GameObject gb_c;
    public GameObject gb_d;

    public RagdollBone bone;

    public Ragdoll ragdoll;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = new Ragdoll();

        A = new Particle();
        B = new Particle();
        C = new Particle();
        D = new Particle();

        A.set_position(new Vector3(-5.45f, 0, -10.0f));
        B.set_position(new Vector3(5.45f, 0, -10.0f));
        C.set_position(new Vector3(-5.45f, 0, 10.0f));
        D.set_position(new Vector3(5.45f, 0, 10.0f));

       
        gb_a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gb_b = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gb_c = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gb_d = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        gb_a.name = "p_A";
        gb_b.name = "p_B";
        gb_c.name = "p_C";
        gb_d.name = "p_D";

        gb_a.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        gb_b.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        gb_c.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        gb_d.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        gb_a.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
        gb_b.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
        gb_c.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");
        gb_d.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/AlphaMaterial");

        bone = new RagdollBone();
        bone.init(ragdoll, A, B, C, D);

         line = this.gameObject.AddComponent<LineRenderer>();
        line.material = Resources.Load<Material>("Materials/AlphaMaterial");
    }

    // Update is called once per frame
    void Update()
    {
        set_position();
    }

    void set_position()
    {
        gb_a.transform.position = A.position();
        gb_b.transform.position = B.position();
        gb_c.transform.position = C.position();
        gb_d.transform.position = D.position();


#pragma warning disable CS0618 // 类型或成员已过时
        line.SetVertexCount(9);
#pragma warning restore CS0618 // 类型或成员已过时
        line.SetPosition(0, A.position());
        line.SetPosition(1, B.position());
        line.SetPosition(2, C.position());
        line.SetPosition(3, D.position());
        line.SetPosition(4, A.position());
        line.SetPosition(5, C.position());
        line.SetPosition(6, D.position());
        line.SetPosition(7, B.position());

        line.SetWidth(0.01f, 0.01f);
    }
}
