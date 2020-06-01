using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    public StickConstraint.SatisfyChoice satisfyChoice;
    public Particle.StepMethod stepMethod;
    StickConstraint stick;
    Particle A;
    Particle B;

    GameObject sphereA;
    GameObject sphereB;

    LineRenderer line;

    ExcelLoger excelLoger;
    int scene_number;

    // Start is called before the first frame update
    void Start()
    {
        A = new Particle();
        B = new Particle();
        A.set_init_position(Vector3.zero);
        A.set_position(new Vector3(0, 0, 0));
        A.stepMethod = stepMethod;
        B.stepMethod = stepMethod;
        B.set_init_position(new Vector3(6,  0, 0));
        B.set_position(new Vector3(6, 0,  0));


        stick = new StickConstraint();
        stick.SetRestLength(6);
        stick.init(A, B);
        stick.m_choice = satisfyChoice;

        sphereA = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        line = sphereA.AddComponent<LineRenderer>();
        line.material = Resources.Load<Material>("Materials/AlphaMaterial");

        excelLoger = this.gameObject.AddComponent<ExcelLoger>();
        excelLoger.CreateExcel("TestStickStable1000");
        excelLoger.addCol("TestStickStable1000", 1, "序号");
        excelLoger.addCol("TestStickStable1000", 2, "时间");
        excelLoger.addCol("TestStickStable1000", 3, "A偏移距离");
        excelLoger.addCol("TestStickStable1000", 4, "AB距离");


        scene_number = 1;
    }

    // Update is called once per frame
    void Update()
    {
        A.set_position(A.init_position());
        B.add_force(new Vector3(1000f, 0, 0), 0.025f);

        stick.satisfy();



        sphereA.transform.position = A.position();
        sphereB.transform.position = B.position();
#pragma warning disable CS0618 // 类型或成员已过时
        line.SetVertexCount(2);
#pragma warning restore CS0618 // 类型或成员已过时
        line.SetPosition(0, A.position());
        line.SetPosition(1, B.position());
#pragma warning disable CS0618 // 类型或成员已过时
        line.SetWidth(0.03f, 0.03f);
#pragma warning restore CS0618 // 类型或成员已过时

        scene_number++;
        excelLoger.writeValue("TestStickStable1000", scene_number, 1, scene_number);
        excelLoger.writeValue("TestStickStable1000", scene_number, 2, Time.timeSinceLevelLoad);
        excelLoger.writeValue("TestStickStable1000", scene_number, 3, A.position().magnitude);
        excelLoger.writeValue("TestStickStable1000", scene_number, 4, (A.position() - B.position()).magnitude);
    }
}
