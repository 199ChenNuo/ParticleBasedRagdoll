using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComplexStick : MonoBehaviour
{
    Particle p1, p2, p3, p4;
    RagdollBone bone;
    Ragdoll ragdoll;
    // Start is called before the first frame update
    void Start()
    {
        p1 = new Particle();
        p2 = new Particle();
        p3 = new Particle();
        p4 = new Particle();
        bone = new RagdollBone(true);
        ragdoll = new Ragdoll();

        p1.set_init_position(0, 0, 0);
        p2.set_init_position(3, 0, 0);
        p3.set_init_position(6, 0, 3);
        p4.set_init_position(6, 0, -3);
        p1.set_fixed(true);
        bone.init(ragdoll, p1, p2, p3, p4);
        bone.set_cube_pos(3, 0, 0);
        bone.set_cube_size(6, 6, 2, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(new Vector3(0, -9, 9), Time.deltaTime);
    }
}
