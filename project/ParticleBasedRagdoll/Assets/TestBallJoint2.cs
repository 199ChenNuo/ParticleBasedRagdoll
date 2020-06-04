using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallJoint2 : MonoBehaviour
{
    public Particle A;
    public Particle B;
    public Particle C;
    public Particle D;
    public Particle E;
    public Particle F;
    public Particle G;

    public RagdollBone boneA;
    public RagdollBone boneB;

    public BallJoint ball;

    public Ragdoll ragdoll;
    // Start is called before the first frame update
    void Start()
    {
        A = new Particle();
        B = new Particle();
        C = new Particle();
        D = new Particle();
        E = new Particle();
        F = new Particle();
        G = new Particle();

        boneA = new RagdollBone(true);
        boneB = new RagdollBone(true);

        ball = new BallJoint();

        ragdoll = new Ragdoll();

        A.set_init_position(new Vector3(0, 0, 0));
        B.set_init_position(new Vector3(3, 0, -3));
        C.set_init_position(new Vector3(6, 0, 0));
        D.set_init_position(new Vector3(3, 0, 3));
        E.set_init_position(new Vector3(9, 0, -3));
        F.set_init_position(new Vector3(12, 0, 0));
        G.set_init_position(new Vector3(9, 0, 3));


        boneA.init(ragdoll, A, C, B, D, "fixed_bone");
        boneA.set_fixed(true);
        boneA.set_color(Color.blue);
        boneA.set_size(new Vector3(4, 2, 2));
        boneA.set_cube_pos(new Vector3(3, 0, 0));
        // boneA.set_cube_rot(0, 0, 0);

        boneB.init(ragdoll, C, F, G, E, "moving_bone");
        boneB.set_color(Color.red);
        boneB.set_size(new Vector3(5.5f, 2, 2));
        boneB.set_cube_pos(new Vector3(3, 0, 0));
        
        ball.init(boneA, boneB, C, G, 100, new Vector3(1, 2, 0));

        ragdoll.add_ragdoll_bone(boneA);
        ragdoll.add_ragdoll_bone(boneB);
        ragdoll.add_constraint(ball);
    }

    // Update is called once per frame
    void Update()
    {

        ragdoll.run(new Vector3(0, 9, 9), Time.deltaTime);

        // Debug.Log((boneB.m_C.position() - boneB.m_A.position()).magnitude.ToString());
    }
}
