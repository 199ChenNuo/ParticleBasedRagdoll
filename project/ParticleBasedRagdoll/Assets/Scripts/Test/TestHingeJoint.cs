using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHingeJoint : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, 0, 0);
    public Ragdoll ragdoll;

    public Vector3 posA;
    public Vector3 posB;

    public Particle A;
    public Particle B;
    public Particle C;
    public Particle D;
    public Particle E;
    public Particle F;


    public RagdollBone boneA;
    public RagdollBone boneB;

    public HingeJoint hinge;


    // public GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        {
            A = new Particle();
            B = new Particle();
            C = new Particle();
            D = new Particle();
            E = new Particle();
            F = new Particle();

            boneA = new RagdollBone(true);
            boneB = new RagdollBone(true);

            hinge = new HingeJoint();

            ragdoll = new Ragdoll();
        }

        {
            A.init(new Vector3(0, 0, 0));
            B.init(new Vector3(6, 0, -4));
            C.init(new Vector3(6, 0, 4));
            D.init(new Vector3(12, 0, 0));
            E.init(new Vector3(3, 3, 0));
            F.init(new Vector3(9, 3, 0));
        }

        {
            boneA.init(ragdoll, A, B, C, E, "boneA");
            boneA.set_fixed(true);
            boneA.set_color(Color.blue);
            boneA.set_cube_pos(posA);
            boneA.set_size(new Vector3(2, 2, 2));

            boneB.init(ragdoll, B, C, D, F, "boneB");
            boneB.set_color(Color.red);
            boneB.set_cube_pos(posB);
            boneB.set_size(new Vector3(2, 2, 2));
        }

        {
            ragdoll.add_constraint(hinge);
            ragdoll.add_ragdoll_bone(boneA);
            ragdoll.add_ragdoll_bone(boneB);
        }

        {
            hinge.init(boneA, boneB, B, C, 60, 60);
        }
    }

    // Update is called once per frame
    void Update()
    {
         ragdoll.run(gravity, 0.01f);
    }
}
