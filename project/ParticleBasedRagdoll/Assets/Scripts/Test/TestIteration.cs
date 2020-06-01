using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIteration : MonoBehaviour
{
    public int iteration;
    Ragdoll ragdoll;
    Vector3 gravity = new Vector3(0, -9, 0);

    int frame_count;

    Particle A1;
    Particle A2;
    Particle A3;
    Particle A4;

    // bone B and bone A shares a particle, so I do not declare B4
    Particle B1;
    Particle B2;
    Particle B3;

    RagdollBone boneA;
    RagdollBone boneB;

    BallJoint ball;

    // Start is called before the first frame update
    void Start()
    {
        frame_count = 0;
        addBone(Vector3.zero);
        /*
        addBone(new Vector3(10, 0, 0));
        addBone(new Vector3(20, 0, 0));
        addBone(new Vector3(-10, 0, 0));
        addBone(new Vector3(-20, 0, 0));
        */
        ragdoll.m_itertaion = iteration;
    }

    // Update is called once per frame
    void Update()
    {
        frame_count++;
        ragdoll.run(gravity, Time.deltaTime);

        if ((A4.position() - B3.position()).magnitude > 4.4f)
        {
            Debug.Log(frame_count);
            Debug.Log(Time.timeSinceLevelLoad);
        }
    }

    void addBone(Vector3 offset)
    {
        

        A1 = new Particle();
        A2 = new Particle();
        A3 = new Particle();
        A4 = new Particle();
        B1 = new Particle();
        B2 = new Particle();
        B3 = new Particle();

        boneA = new RagdollBone(true);
        boneB = new RagdollBone(true);

        ball = new BallJoint();

        ragdoll = new Ragdoll();

        A1.set_init_position(new Vector3(0, 0, 0)  + offset);
        A2.set_init_position(new Vector3(2, 2, -1) + offset);
        A3.set_init_position(new Vector3(2, -2, 1) + offset);
        // The shared particle
        A4.set_init_position(new Vector3(4, 0, 0) + offset);
        B1.set_init_position(new Vector3(6, 2, -1) + offset);
        B2.set_init_position(new Vector3(6, -2, -1) + offset);
        B3.set_init_position(new Vector3(8, 0, 0) + offset);


        boneA.init(ragdoll, A1, A4, A3, A2, "fixed_bone");
        boneA.set_fixed(true);
        boneA.set_color(Color.blue);
        boneA.set_size(new Vector3(4, 2, 2));
        boneA.set_cube_pos(new Vector3(1.5f, 0, 1.5f));

        boneB.init(ragdoll, B3, A4, B2, B1, "moving_bone");
        boneB.set_color(Color.red);
        boneB.set_size(new Vector3(5.5f, 2, 2));
        boneB.set_cube_pos(new Vector3(0.5f, 0, 0));

        ball.init(boneA, boneB, A4, B3, 1, new Vector3(1, 0, 0));

        ragdoll.add_ragdoll_bone(boneA);
        ragdoll.add_ragdoll_bone(boneB);
        ragdoll.add_constraint(ball);
    }
}
