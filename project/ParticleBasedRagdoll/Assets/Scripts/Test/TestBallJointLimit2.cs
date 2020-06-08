using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                ↑ z
//                |
//             p1---p2
//              \   /
//                p3
//                |
//               p4
//              /  \
// p8---┐     /      \
// |   p10--p5--------p6----> x
// p9---┘     \      /
//              \  /
//               p7
public class TestBallJointLimit2 : MonoBehaviour
{
  [Range(0, 90)]
  public float joint_limit;

  Particle p1;
  Particle p2;
  Particle p3;
  Particle p4;
  Particle p5;
  Particle p6;
  Particle p7;
  Particle p8;
  Particle p9;
  Particle p10;

  RagdollBone fix_bone;
  RagdollBone bone1;
  RagdollBone bone2;

  BallJoint ball1;
  BallJoint ball2;

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
    p8 = new Particle();
    p9 = new Particle();
    p10 = new Particle();

    fix_bone = new RagdollBone(/*visualize=*/true);
    bone1 = new RagdollBone(true);
    bone2 = new RagdollBone(true);

    ball1 = new BallJoint();
    ball2 = new BallJoint();

    ragdoll = new Ragdoll();

    p1.set_init_position(-3, 9, 0);
    p2.set_init_position(3, 9, 0);
    p3.set_init_position(0, 6, 0);
    p4.set_init_position(0, 3, 0);
    p5.set_init_position(-3, 0, 0);
    p6.set_init_position(3, 0, 0);
    p7.set_init_position(0, -3, 0);
    p8.set_init_position(-9, 3, 0);
    p9.set_init_position(-9, -3, 0);
    p10.set_init_position(-6, 0, 0);

    // x axis and y axis are transformed
    fix_bone.init(ragdoll, p8, p9, p10, p5, "fixed bone");
    fix_bone.set_fixed(true);
    fix_bone.set_cube_pos(3, 3, 0);
    fix_bone.set_cube_size(6, 6, 3, 0.9f);
    fix_bone.cube().GetComponent<BoxCollider>().isTrigger = true;

    bone1.init(ragdoll, p5, p6, p4, p7, "bone 1");
    bone1.set_cube_pos(6, 0, 0);
    bone1.set_cube_size(6, 6, 3, 0.9f);
    bone1.cube().GetComponent<BoxCollider>().isTrigger = true;

        bone2.init(ragdoll, p1, p2, p3, p4, "bone 2");
        bone2.set_cube_pos(3, 3, 0);
        bone2.set_cube_size(6, 6, 3, 0.9f);
        bone2.cube().GetComponent<BoxCollider>().isTrigger = true;

        // ball1.init(ragdoll, fix_bone, bone1, p5, p6, 45, new Vector3(0, 1, 0));
        ball1.init(ragdoll, fix_bone, bone1, p5, p10, p6, 45, new Vector3(0, 1, 0));
    ball2.init(ragdoll, bone1, bone2, p4, p7, p3, joint_limit, new Vector3(0, 1, 0));
  }

  // Update is called once per frame
  void Update()
  {
    ragdoll.run(new Vector3(0, -9, 0), Time.deltaTime);
  }
}
