using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public List<RagdollBone> m_ragdoll_bones;

    public float m_friction;

    public void add_ragdoll_bone(RagdollBone bone)
    {
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
    }

    public void clear()
    {
        m_ragdoll_bones.Clear();
    }

    public void add_constraint(StickConstraint stick)
    {
        // TODO: implement
    }

    public void run()
    { 
        // TODO: implement
    }

    void collision_detection()
    {
        int collisions = 0;
        Vector3[] collision_points = new Vector3[16];

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
