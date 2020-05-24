using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJointTest : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, 0, 0);
    public Ragdoll ragdoll;

    #region Particles
    public Particle A1;
    public Particle A2;
    public Particle A3;
    public Particle A4;

    // bone B and bone A shares a particle, so I do not declare B4
    public Particle B1;
    public Particle B2;
    public Particle B3;
    #endregion

    #region Bones

    public RagdollBone boneA;
    public RagdollBone boneB;
    #endregion

    #region Joints
    public BallJoint ball;
    #endregion

    public GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
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
        }

        {
            A1.set_position(new Vector3(0, 2 * Mathf.Sqrt(6), 2 * Mathf.Sqrt(3)));
            A2.set_position(new Vector3(-3, 2 * Mathf.Sqrt(6), -Mathf.Sqrt(3)));
            A3.set_position(new Vector3(3, 2 * Mathf.Sqrt(6), -Mathf.Sqrt(3)));
            // The shared particle
            A4.set_position(Vector3.zero);
            B1.set_position(new Vector3(0, -2 * Mathf.Sqrt(6), 2 * Mathf.Sqrt(3)));
            B2.set_position(new Vector3(-3, -2 * Mathf.Sqrt(6), -Mathf.Sqrt(3)));
            B3.set_position(new Vector3(3, -2 * Mathf.Sqrt(6), -Mathf.Sqrt(3)));
        }

        {
            boneA.init(ragdoll, A1, A2, A3, A4, "boneA");
            boneB.init(ragdoll, B1, B2, B3, A4, "boneB");
        }

        {
            ragdoll.add_constraint(ball);
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
            ball.init(boneA, boneB, A4, B1, 60, new Vector3(0, 1, 0));
        }

        ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = new Vector3(0, -10, 0);
        ground.transform.localScale = new Vector3(20, 1, 20);
       
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.m_ragdoll_bones[0].add_force(new Vector3(0, -0.1f, 0), Time.deltaTime);

        ragdoll.run(gravity, Time.deltaTime);
    }
}
