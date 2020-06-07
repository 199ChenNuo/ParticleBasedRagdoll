using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// 
//  p1--┐          ┌--p6
//   |  p3---p4---p5   |
//  p2--┘          └--p7
// 

public class TestBallJointLimit : MonoBehaviour
{
    [Range (0, 180)]
    public float angle_limit = 90;
    Particle p1;
    Particle p2;
    Particle p3;
    Particle p4;
    Particle p5;
    Particle p6;
    Particle p7;

    RagdollBone boneA;
    RagdollBone boneB;

    BallJoint ball;

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

        boneA = new RagdollBone(/*visualize_particle=*/true);
        boneB = new RagdollBone(/*visualize_particle=*/true);

        ball = new BallJoint();

        ragdoll = new Ragdoll();
        

        p1.set_init_position(0, 0, 3);
        p2.set_init_position(0, 0, -3);
        p3.set_init_position(3, 0, 0);
        p4.set_init_position(6, 0, 0);
        p5.set_init_position(9, 0, 0);
        p6.set_init_position(12, 0, 3);
        p7.set_init_position(12, 0, -3);

        boneA.init(ragdoll, p1, p2, p3, p4, "fixed bone");
        boneA.set_color(Color.red);
        boneA.set_cube_pos(3, 3, 0);
        boneA.set_cube_size(/*x=*/6, /*y=*/6, /*z=*/2, /*scale=*/0.9f);
        boneA.set_fixed(true);

        boneB.init(ragdoll, p4, p5, p6, p7, "movable bone");
        boneB.set_color(Color.blue);
        boneB.set_cube_pos(3, 0, 0);
        boneB.set_cube_size(6, 6, 2, 0.9f);
        boneB.set_fixed(false);
        
        ball.init(ragdoll, boneA, boneB, p4, p5, angle_limit, new Vector3(1, 0, 0));
        // ragdoll.add_constraint(ball);
        // ragdoll.add_ragdoll_bone(boneA);
        // ragdoll.add_ragdoll_bone(boneB);
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(new Vector3(0, -9, 0), 0.005f);

#if false
        Debug.Log(boneB.m_stick[2].get_current_len());
#endif
    }
}
