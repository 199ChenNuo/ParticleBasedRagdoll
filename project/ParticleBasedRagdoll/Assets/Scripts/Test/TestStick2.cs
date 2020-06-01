using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test triangle stab
/// </summary>
public class TestStick2 : MonoBehaviour
{
    public StickConstraint.SatisfyChoice satisfyChoice;
    public Particle.StepMethod stepMethod;
           
    Particle A, B, C;
    StickConstraint A2B, A2C, B2C;


    GameObject sphereA;
    GameObject sphereB;
    GameObject sphereC;
    LineRenderer line;

    ExcelLoger excelLoger;
    int scene_number;

    // Start is called before the first frame update
    void Start()
    {
        A = new Particle();
        B = new Particle();
        C = new Particle();
        A.set_init_position(Vector3.zero);
        B.set_init_position(new Vector3(0, 0, 3));
        C.set_init_position(new Vector3(3, 0, 0));

        A2B = new StickConstraint();
        B2C = new StickConstraint();
        A2C = new StickConstraint();

        A2B.init(A, B);
        B2C.init(B, C);
        A2C.init(A, C);

        A.stepMethod = stepMethod;
        C.stepMethod = stepMethod;
        B.stepMethod = stepMethod;

        A2C.m_choice = satisfyChoice;
        A2B.m_choice = satisfyChoice;
        B2C.m_choice = satisfyChoice;

        sphereA = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereC = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        line = sphereA.AddComponent<LineRenderer>();
        line.material = Resources.Load<Material>("Materials/AlphaMaterial");


        excelLoger = this.gameObject.AddComponent<ExcelLoger>();
        excelLoger.CreateExcel("TestStickTriangle");
        excelLoger.addCol("TestStickTriangle", 1, "序号");
        excelLoger.addCol("TestStickTriangle", 2, "AB距离");
        excelLoger.addCol("TestStickTriangle", 3, "AC距离");
        excelLoger.addCol("TestStickTriangle", 4, "BC距离");


        scene_number = 1;
    }

    // Update is called once per frame
    void Update()
    {
        A.set_position(A.init_position());
        // C.set_position(C.init_position());
        B.add_force(new Vector3(100, 0, 0), 0.025f);

        A2B.satisfy();
        A2C.satisfy();
        B2C.satisfy();


        sphereA.transform.position = A.position();
        sphereB.transform.position = B.position();
        sphereC.transform.position = C.position();
#pragma warning disable CS0618 // 类型或成员已过时
        line.SetVertexCount(3);
#pragma warning restore CS0618 // 类型或成员已过时
#pragma warning disable CS0618 // 类型或成员已过时
        line.SetWidth(0.03f, 0.03f);
#pragma warning restore CS0618 // 类型或成员已过时
        line.SetPosition(0, A.position());
        line.SetPosition(1, B.position());
        line.SetPosition(2, C.position());


        scene_number++;
        excelLoger.writeValue("TestStickTriangle", scene_number, 1, scene_number);
        excelLoger.writeValue("TestStickTriangle", scene_number, 2, (A.position() - B.position()).magnitude);
        excelLoger.writeValue("TestStickTriangle", scene_number, 3, (A.position() - C.position()).magnitude);
        excelLoger.writeValue("TestStickTriangle", scene_number, 4, (B.position() - C.position()).magnitude);
    }
}
