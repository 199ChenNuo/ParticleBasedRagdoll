using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpTest : MonoBehaviour
{
    public Ragdoll ragdoll;

    public RagdollBone m_boneA;
    public RagdollBone m_boneB;

    public Particle p1;
    public Particle p2;
    public Particle p3;
    public Particle p4;
    public Particle p5;
    public Particle p6;
    public Particle p7;

    public BallJoint m_ball_joint;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = new Ragdoll();

        m_boneA= new RagdollBone();
        m_boneA.init(ragdoll, p1, p2, p3, p4);

        m_boneB = new RagdollBone();
        m_boneB.init(ragdoll, p4, p5, p6, p7);

        m_ball_joint = new BallJoint();
        m_ball_joint.init(m_boneA, m_boneB, p4, p5, 90.0f, new Vector3(1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
