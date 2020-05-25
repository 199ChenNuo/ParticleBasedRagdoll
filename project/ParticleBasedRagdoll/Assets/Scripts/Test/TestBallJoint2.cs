using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallJoint2 : MonoBehaviour
{
    public Particle A1;
    public Particle A2;
    public Particle A3;
    public Particle A4;

    // bone B and bone A shares a particle, so I do not declare B4
    public Particle B1;
    public Particle B2;
    public Particle B3;

    public RagdollBone boneA;
    public RagdollBone boneB;

    public BallJoint ball;

    public Ragdoll ragdoll;
    // Start is called before the first frame update
    void Start()
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

        A1.set_position(new Vector3(0, 0, 0));
        A1.set_init_position(new Vector3(0, 0, 0));
        A2.set_position(new Vector3(2, 2, -1));
        A2.set_init_position(new Vector3(2, 2, -1));
        A3.set_position(new Vector3(2, -2, 1));
        A3.set_init_position(new Vector3(2, -2, 1));
        // The shared particle
        A4.set_position(new Vector3(4, 0, 0));
        A4.set_init_position(new Vector3(4, 0, 0));
        B1.set_position(new Vector3(6, 2, -1));
        B1.set_init_position(new Vector3(6, 2, -1));
        B2.set_position(new Vector3(6, -2, -1));
        B2.set_init_position(new Vector3(6, -2, -1));
        B3.set_position(new Vector3(8, 0, 0));
        B3.set_init_position(new Vector3(8, 0, 0));


        boneA.init(ragdoll, A1, A2, A3, A4, "fixed_bone");
        boneA.set_fixed(true);
        boneA.set_color(Color.blue);
        boneB.init(ragdoll, A4, B1, B2, B3, "moving_bone");
        boneB.set_color(Color.red);

        ball.init(boneA, boneB, A4, B3, 10, new Vector3(1, 0, 0));

        ragdoll.add_ragdoll_bone(boneA);
        ragdoll.add_ragdoll_bone(boneB);
        ragdoll.add_constraint(ball);
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(new Vector3(0, -9.8f, 0), 0.01f);

        // Debug.Log((boneB.m_C.position() - boneB.m_A.position()).magnitude.ToString());
    }
}
