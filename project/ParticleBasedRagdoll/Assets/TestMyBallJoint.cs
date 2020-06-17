using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

//
// p1                       p6
//  |    p3---p4---p5   |
// p2                       p7
//
//

    // [CustomEditor (typeof(TestMyBallJoint))]
public class TestMyBallJoint : MonoBehaviour
{
    [Range (0, 180)]
    public float angle_limit;

    Particle p1;
    Particle p2;
    Particle p3;
    Particle p4;
    Particle p5;
    Particle p6;
    Particle p7;

    RagdollBone boneA;
    RagdollBone boneB;

    MyBallJoint ball;

    Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        p1 = new Particle();
        p2 = new Particle();
        p3 = new Particle();
        p4 = new Particle();
        p5 = new Particle();
        p6 = new Particle();
        p7 = new Particle();

        boneA = new RagdollBone(true);
        boneB = new RagdollBone(true);

        ball = new MyBallJoint();

        ragdoll = new Ragdoll();

        p1.set_init_position(-6, 3, 0);
        p2.set_init_position(-6, -3, 0);
        p3.set_init_position(-3, 0, 0);
        p4.set_init_position(0, 0, 0);
        p5.set_init_position(3, 0, 0);
        p6.set_init_position(6, 3, 0);
        p7.set_init_position(6, -3, 0);

        boneA.init(ragdoll, p1, p2, p3, p4, "fixed bone");
        boneA.set_cube_pos(3, 3, 0);
        boneA.set_cube_size(3, 3, 2);
        boneA.set_color(Color.red);
        boneA.set_fixed(true);

        boneB.init(ragdoll, p6, p7, p5, p4, "move bone");
        boneB.set_cube_pos(3, 3, 0);
        boneB.set_cube_size(3, 3, 2);
        boneB.set_color(Color.blue);

        ball.init(ragdoll, boneA, boneB, p4, p3, p5, angle_limit, new Vector3(0, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(new Vector3(0, -9, 0), 0.05f);
    }

    public void changeAngleLimit()
    {
        angle_limit = GameObject.Find("Canvas/Slider").GetComponent<Slider>().value;
        ball.set_angle_limit(angle_limit);
    }

    /*
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUI.changed)
        {
            ball.set_angle_limit(angle_limit);
        }
    }
    */
}
