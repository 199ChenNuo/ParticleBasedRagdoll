using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollRunTest : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.8f, 0);
    public Ragdoll ragdoll;

    public Particle A;
    public Particle B;
    public Particle C;
    public Particle D;

    public RagdollBone boneA = new RagdollBone();
    public RagdollBone boneB = new RagdollBone();

    public BallJoint ball = new BallJoint();

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = new Ragdoll();

        ragdoll.add_constraint(ball);
        ragdoll.add_ragdoll_bone(boneA);
        ragdoll.add_ragdoll_bone(boneB);

        
    }

    // Update is called once per frame
    void Update()
    {
        // boneA.m_obb.m_cube.GetComponent<Rigidbody>().AddForce(0.1f, 0, 0);

        ragdoll.run(gravity, Time.deltaTime);
    }

}
