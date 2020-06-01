using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHinge2 : MonoBehaviour
{
    Particle A;
    Particle B;
    Particle C;
    Particle D;
    Particle E;
    Particle F;

    RagdollBone boneA;
    RagdollBone boneB;

    MyHingeJoint hinge;

    Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        A = new Particle();
        B = new Particle();
        C = new Particle();
        D = new Particle();
        E = new Particle();
        F = new Particle();

        A.set_init_position(Vector3.zero);
        B.set_init_position(new Vector3(6, 0, 0));
        C.set_init_position(new Vector3(6, 0, 6));
        D.set_init_position(new Vector3(0, 0, 6));
        E.set_init_position(new Vector3(12, 0, 0));
        F.set_init_position(new Vector3(12, 0, 6));

        ragdoll = new Ragdoll();

        boneA = new RagdollBone(true);
        boneB = new RagdollBone(true);

        boneA.init(ragdoll, A, B, C, D, "fixed");
        boneA.set_fixed(true);
        boneA.set_cube_pos(new Vector3(3, 0, 0));
        boneA.set_size(new Vector3(4, 1, 1));
        
        boneB.init(ragdoll, B, E, C, F, "movable");
        boneB.set_cube_pos(new Vector3(3, 1, 0));
        boneB.set_size(new Vector3(4, 1, 1));


        ragdoll.add_ragdoll_bone(boneA);
        ragdoll.add_ragdoll_bone(boneB);

        hinge = new MyHingeJoint();
        hinge.init(boneA, boneB, B, C, 30, 30);
        ragdoll.add_constraint(hinge);
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(new Vector3(0, -9, 0), Time.deltaTime);
    }
}
