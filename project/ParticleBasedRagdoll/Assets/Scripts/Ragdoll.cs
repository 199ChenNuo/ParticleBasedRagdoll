using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll
{
    public List<RagdollBone> m_ragdoll_bones;
    public List<BallJoint> m_ball_joints;
    public List<HingeJoint> m_hinge_joints;
    public List<StickConstraint> m_stick_constraints;
    public int m_itertaion = 3;

    public float m_friction;

    public void add_ragdoll_bone(RagdollBone bone)
    {
        // TODO: atomatice add stick constraint in bone to ragdoll
        bone.connect(this);
        m_ragdoll_bones.Add(bone);
    }

    public void remove_ragdoll_bone(RagdollBone bone)
    {
        bone.disconnect();
        m_ragdoll_bones.Remove(bone);
    }

    public Ragdoll()
    {
        m_friction = 0.00025f;
        m_ragdoll_bones = new List<RagdollBone>();
        m_ball_joints = new List<BallJoint>();
        m_hinge_joints = new List<HingeJoint>();
        m_stick_constraints = new List<StickConstraint>(); 
    }

    public void clear()
    {
        m_ragdoll_bones.Clear();
    }

    public void add_constraint(BallJoint joint)
    {
        m_ball_joints.Add(joint);
    }

    public void add_constraint(HingeJoint hinge)
    {
        m_hinge_joints.Add(hinge);
    }

    public void add_constraint(StickConstraint stick)
    {
        m_stick_constraints.Add(stick);
    }

    public void run(Vector3 gravity, float delta_T)
    {

        foreach (RagdollBone bone in m_ragdoll_bones)
        {
            bone.add_force(gravity, delta_T);
        }

        for (int i = 0; i < m_itertaion; ++i)
        {
            foreach (RagdollBone bone in m_ragdoll_bones)
            {
                bone.update_coordsys();
            }

            collision_detection();

          
            foreach (BallJoint ballJoint in m_ball_joints)
            {
                ballJoint.satisfy();
            }
            foreach(HingeJoint hinge in m_hinge_joints)
            {
                hinge.satisfy();
            }
            foreach(StickConstraint stickConstraint in m_stick_constraints)
            {
                stickConstraint.satisfy();
            }

            foreach (RagdollBone bone in m_ragdoll_bones)
            {
                bone.update_position();
            }

        }
    }

    void collision_detection()
    {
        int collisions = 0;
        Vector3[] collision_points = new Vector3[16];
        Vector3 n;
        float[] disconnect = new float[16];

        for (int i = 0; i < m_ragdoll_bones.Count; ++i)
        {
            for (int j = i + 1; j < m_ragdoll_bones.Count; ++j)
            {
                if (m_ragdoll_bones[i].is_connect(m_ragdoll_bones[j])) { continue; }

                // detect collision for bone[i] and bone[j]
            }
        }
        // TODO: implement
    }

    /**
    Handles a collision
    Including particle scaling ,bone mass scaling and friction
    @param A                Bone A
    @param B                Bone B
    @param collisions       Number of collisions
    @param P                List of collision points
    @param n                Collision normal
    @param distance         List of collision depths
    **/
    void collision_handling(RagdollBone A, RagdollBone B, int collistions, Vector3 p, Vector3 n, float distance)
    {
        // TODO: implement
    }

    // set up a human with center being `position`
    public void set_up_human(Vector3 position, float size, bool visual_flag = false)
    {
        // ground
        // p0
        Particle ground_A = new Particle();
        // p1
        Particle ground_B = new Particle();
        // p2
        Particle ground_C = new Particle();
        // p3
        Particle ground_D = new Particle();
        ground_A.set_position(new Vector3(100, -100, -400) * size + position);
        ground_B.set_position(new Vector3(100, 100, -400) * size + position);
        ground_C.set_position(new Vector3(-100, -100, -400) * size + position);
        ground_D.set_position(new Vector3(-100, 100, -400) * size + position);
        RagdollBone ground = new RagdollBone(visual_flag);
        ground.init(this, ground_A, ground_B, ground_C, ground_D, "ground");

        // head
        // p4
        Particle head_A = new Particle();
        // p5
        Particle head_B = new Particle();
        // p6
        Particle head_C = new Particle();
        // p7
        Particle head_D = new Particle();
        head_A.set_position(new Vector3(-5.45f, 0, -10) * size + position);
        head_B.set_position(new Vector3(5.45f, 0, -10) * size + position);
        head_C.set_position(new Vector3(-5.45f, 0, 10) * size + position);
        head_D.set_position(new Vector3(5.45f, 0, 10) * size + position);
        RagdollBone head = new RagdollBone(visual_flag);
        head.init(this, head_A, head_B, head_C, head_D, "head");

        // neck, shares particles head_A -> neck_A, head_B -> neck_B
        // p8
        Particle neck_C = new Particle();
        // p9
        Particle neck_D = new Particle();
        neck_C.set_position(new Vector3(0, 0, -22.3f) * size + position);
        neck_D.set_position(new Vector3(0, 0, -15) * size + position);
        RagdollBone neck = new RagdollBone(visual_flag);
        neck.init(this, head_A, head_B, neck_C, neck_D, "neck");


        // chest, shares particles neck_C -> chest_A
        // p10
        Particle chest_B = new Particle();
        // p11
        Particle chest_C = new Particle();
        // p12
        Particle chest_D = new Particle();
        chest_B.set_position(new Vector3(19.6f, 0, 26.7f) * size + position);
        chest_C.set_position(new Vector3(-19.6f, 0, -26.7f) * size + position);
        chest_D.set_position(new Vector3(0, 0, -61.5f) * size + position);
        RagdollBone chest = new RagdollBone(visual_flag);
        chest.init(this, neck_C, chest_B, chest_C, chest_D, "chest");

        // hip, shares particle chest_D -> hip_A
        // p13
        Particle hip_B = new Particle();
        // p14
        Particle hip_C = new Particle();
        // p15
        Particle hip_D = new Particle();
        hip_B.set_position(new Vector3(0, 0, -70.4f) * size + position);
        hip_C.set_position(new Vector3(9.45f, 0, -77.7f) * size + position);
        hip_D.set_position(new Vector3(-9.45f, 0, -77.7f) * size + position);
        RagdollBone hip = new RagdollBone(visual_flag);
        hip.init(this, chest_D, hip_B, hip_C, hip_D, "hip");

        // left overarm, chest_C->leaftOverarm_A
        // p16
        Particle leftOverarm_B = new Particle();
        // p17
        Particle leftOverarm_C = new Particle();
        // p18
        Particle leftOverarm_D = new Particle();
        leftOverarm_B.set_position(new Vector3(37.9f, 0, -26.7f) * size + position);
        leftOverarm_C.set_position(new Vector3(56.2f, 0, 30.95f) * size + position);
        leftOverarm_D.set_position(new Vector3(56.2f, 0, -22.45f) * size + position);
        RagdollBone leftOverarm = new RagdollBone(visual_flag);
        leftOverarm.init(this, chest_B, leftOverarm_B, leftOverarm_C, leftOverarm_D, "left overarm");

        // right overarm, chect_C->rightOverarm_A
        // p19
        Particle rightOverarm_B = new Particle();
        // p20
        Particle rightOverarm_C = new Particle();
        // p21
        Particle rightOverarm_D = new Particle();
        rightOverarm_B.set_position(new Vector3(-37.9f, 0, -26.7f) * size + position);
        rightOverarm_C.set_position(new Vector3(-56.2f, 0, -30.95f) * size + position);
        rightOverarm_D.set_position(new Vector3(-56.2f, 0, -22.45f) * size + position);
        RagdollBone rightOverarm = new RagdollBone(visual_flag);
        rightOverarm.init(this, chest_C, rightOverarm_B, rightOverarm_C, rightOverarm_D, "right overarm");

        // left underarm, 
        // p22
        Particle leftUnderarm_A = new Particle();
        // p23
        Particle leftUnderarm_B = new Particle();
        leftUnderarm_A.set_position(new Vector3(85, 0, -30.95f) * size + position);
        leftUnderarm_B.set_position(new Vector3(85, 0, -22.45f) * size + position);
        RagdollBone leftUnderarm = new RagdollBone(visual_flag);
        leftUnderarm.init(this, leftOverarm_C, leftOverarm_D, leftUnderarm_A, leftUnderarm_B, "left underarm");
        
        // right underarm
        // p24
        Particle rightUnderarm_A = new Particle();
        // p25
        Particle rightUnderarm_B = new Particle();
        rightUnderarm_A.set_position(new Vector3(-85, 0, -30.95f) * size + position);
        rightUnderarm_B.set_position(new Vector3(-95, 0, -22.45f) * size + position);
        RagdollBone rightUnderarm = new RagdollBone(visual_flag);
        rightUnderarm.init(this, rightOverarm_C, rightOverarm_D, rightUnderarm_A, rightUnderarm_B, "right underarm");

        // left hand
        // p26
        Particle leftHand_C = new Particle();
        // p27
        Particle leftHand_D = new Particle();
        leftHand_C.set_position(new Vector3(93.9f, 0, -30.95f) * size + position);
        leftHand_D.set_position(new Vector3(93.9f, 0, -22.45f) * size + position);
        RagdollBone leftHand = new RagdollBone(visual_flag);
        leftHand.init(this, leftUnderarm_A, leftUnderarm_B, leftHand_C, leftHand_D, "left hand");

        //right hand
        // p28
        Particle rightHand_C = new Particle();
        // p29
        Particle rightHand_D = new Particle();
        rightHand_C.set_position(new Vector3(-93.9f, 0, -30.95f) * size + position);
        rightHand_D.set_position(new Vector3(-93.9f, 0, -22.45f) * size + position);
        RagdollBone rightHand = new RagdollBone(visual_flag);
        rightHand.init(this, rightUnderarm_A, rightUnderarm_B, rightHand_C, rightHand_D, "right hand");

        // left thigh
        // p30
        Particle leftThigh_B = new Particle();
        // p31
        Particle leftThigh_C = new Particle();
        // p32
        Particle leftThigh_D = new Particle();
        leftThigh_B.set_position(new Vector3(9.45f, 0, 100) * size + position);
        leftThigh_C.set_position(new Vector3(4.15f, 0, -128.9f) * size + position);
        leftThigh_D.set_position(new Vector3(14.75f, 0, -128.9f) * size + position);
        RagdollBone leftThigh = new RagdollBone(visual_flag);
        leftThigh.init(this, hip_C, leftThigh_B, leftThigh_C, leftThigh_D, "left thigh");
       
        // right thigh
        // p33
        Particle rightThigh_B = new Particle();
        // p34
        Particle rightThigh_C = new Particle();
        // p35
        Particle rightThigh_D = new Particle();
        rightThigh_B.set_position(new Vector3(-9.45f, 0, -100) * size + position);
        rightThigh_C.set_position(new Vector3(-4.15f, 0, -128.9f) * size + position);
        rightThigh_D.set_position(new Vector3(-14.75f, 0, -128.9f) * size + position);
        RagdollBone rightThigh = new RagdollBone(visual_flag);
        rightThigh.init(this, hip_D, rightThigh_B, rightThigh_C, rightThigh_D, "right thigh");

        // left calf
        // p36
        Particle leftCalf_A = new Particle();
        // p37
        Particle leftCalf_B = new Particle();
        leftCalf_A.set_position(new Vector3(4.15f, 0, -159.4f) * size + position);
        leftCalf_B.set_position(new Vector3(14.75f, 0, -159.4f) * size + position);
        RagdollBone leftCalf = new RagdollBone(visual_flag);
        leftCalf.init(this, leftThigh_C, leftThigh_D, leftCalf_A, leftCalf_B, "left calf");

        // right calf
        // p38
        Particle rightCalf_A = new Particle();
        // p39
        Particle rightCalf_B = new Particle();
        rightCalf_A.set_position(new Vector3(-4.15f, 0, -159.4f) * size + position);
        rightCalf_B.set_position(new Vector3(-14.75f, 0, -159.4f) * size + position);
        RagdollBone rightCalf = new RagdollBone(visual_flag);
        rightCalf.init(this, rightThigh_C, rightThigh_D, rightCalf_A, rightCalf_B, "right calf");
        
        // left foot
        // p40
        Particle leftFoot_C = new Particle();
        // p41
        Particle leftFoot_D = new Particle();
        leftFoot_C.set_position(new Vector3(4.15f, -10, -173.3f) * size + position);
        leftFoot_D.set_position(new Vector3(14.75f, -10, -173.3f) * size + position);
        RagdollBone leftFoot = new RagdollBone(visual_flag);
        leftFoot.init(this, leftCalf_A, leftCalf_B, leftFoot_C, leftFoot_D, "left foot");


        // right foot
        // p42
        Particle rightFoot_C = new Particle();
        // p43
        Particle rightFoot_D = new Particle();
        rightFoot_C.set_position(new Vector3(-4.15f, -10, -173.3f) * size + position);
        rightFoot_D.set_position(new Vector3(-14.75f, -10, -173.3f) * size + position);
        RagdollBone rightFoot = new RagdollBone(visual_flag);
        rightFoot.init(this, rightCalf_A, rightCalf_B, rightFoot_C, rightFoot_D, "right foot");

        add_ragdoll_bone(ground);
        add_ragdoll_bone(head);
        add_ragdoll_bone(neck);
        add_ragdoll_bone(chest);
        add_ragdoll_bone(hip);
        add_ragdoll_bone(leftOverarm);
        add_ragdoll_bone(rightOverarm);
        add_ragdoll_bone(leftUnderarm);
        add_ragdoll_bone(rightUnderarm);
        add_ragdoll_bone(leftHand);
        add_ragdoll_bone(rightHand);
        add_ragdoll_bone(leftThigh);
        add_ragdoll_bone(rightThigh);
        add_ragdoll_bone(leftCalf);
        add_ragdoll_bone(rightCalf);
        add_ragdoll_bone(leftFoot);
        add_ragdoll_bone(rightFoot);



    }

}
