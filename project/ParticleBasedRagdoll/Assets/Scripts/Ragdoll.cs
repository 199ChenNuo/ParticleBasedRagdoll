using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll
{
    public List<RagdollBone> m_ragdoll_bones;
    public List<BallJoint> m_ball_joints;
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
        m_stick_constraints = new List<StickConstraint>(); 
    }

    public void clear()
    {
        m_ragdoll_bones.Clear();
    }

    public void add_constraint(BallJoint joint)
    {
        // TODO: implement
        m_ball_joints.Add(joint);
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

            /*
            foreach (Joint j in m_joints)
            {
                j.satisfy();
            }
            */
            foreach (BallJoint ballJoint in m_ball_joints)
            {
                ballJoint.satisfy();
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

}
