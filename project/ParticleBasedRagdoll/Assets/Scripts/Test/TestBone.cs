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

        boneA = new RagdollBone(true);

        ragdoll = new Ragdoll();

        A1.set_position(0, 0, 0);
        A3.set_position(2, 0, 0);
        A4.set_position(2, 0, 2);
        A2.set_position(0, 0, 2);

        boneA.init(ragdoll, A1, A2, A3, A4);
        boneA.set_cube_pos(new Vector3(1, 1, 1));

        ragdoll.add_ragdoll_bone(boneA);
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(Vector3.zero, 0.01f);
    }
}
