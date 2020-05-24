using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHingeJoint : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, 0, 0);
    public Ragdoll ragdoll;

    public Particle A;
    public Particle B;
    public Particle C;
    public Particle D;
    public Particle E;
    public Particle F;


    public RagdollBone boneA;
    public RagdollBone boneB;

    public HingeJoint hinge;


    public GameObject ground;

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
            A.init(new Vector3(-3, 0, 3));
            B.init(new Vector3(-3, 0, -3));
            C.init(new Vector3(0, 6, 0));
            D.init(new Vector3(0, 0, 0));
            E.init(new Vector3(3, 0, 3));
            F.init(new Vector3(3, 0, -3));
        }

        {
            boneA.init(ragdoll, A, B, C, D, "boneA");
            boneA.set_fixed(true);
            boneB.init(ragdoll, E, F, C, D, "boneB");

            boneA.set_obb_size(1, 1, 1);
            boneB.set_obb_size(1, 1, 1);
        }

        {
            ragdoll.add_constraint(hinge);
            ragdoll.add_ragdoll_bone(boneA);
            ragdoll.add_ragdoll_bone(boneB);

            foreach (StickConstraint stickConstraint in boneA.m_stick)
            {
                ragdoll.add_constraint(stickConstraint);
            }

            foreach (StickConstraint stickConstraint in boneB.m_stick)
            {
                ragdoll.add_constraint(stickConstraint);
            }
        }

        {
            hinge.init(boneA, boneB, C, D, 60, 60);
        }

        ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = new Vector3(0, -10, 0);
        ground.transform.localScale = new Vector3(20, 1, 20);

    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.m_ragdoll_bones[1].add_force(new Vector3(0, -0.1f, 0), Time.deltaTime);

        ragdoll.run(gravity, Time.deltaTime);
    }
}
