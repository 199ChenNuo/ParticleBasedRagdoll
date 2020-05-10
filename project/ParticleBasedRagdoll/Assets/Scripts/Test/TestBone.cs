using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBone : MonoBehaviour
{
    public Ragdoll ragdoll;

    Particle A1;
    Particle A2;
    Particle A3;
    Particle A4;

    RagdollBone boneA;

    // Start is called before the first frame update
    void Start()
    {
        A1 = new Particle();
        A2 = new Particle();
        A3 = new Particle();
        A4 = new Particle();

        boneA = new RagdollBone(false);

        ragdoll = new Ragdoll();

        A1.set_position(1, 0, 1);
        A2.set_position(-1, 0, 1);
        A3.set_position(1, 0, -1);
        A4.set_position(-1, 0, -1);

        boneA.init(ragdoll, A1, A2, A3, A4);
        boneA.m_obb.m_cube.name = "flat bone";

        ragdoll.add_ragdoll_bone(boneA);
    }

    // Update is called once per frame
    void Update()
    {
        // set gracity to 0 for observation
        ragdoll.run(new Vector3(0, 0, 0), Time.deltaTime);
    }
}
