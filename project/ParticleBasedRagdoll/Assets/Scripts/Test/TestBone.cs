﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBone : MonoBehaviour
{
    public Ragdoll ragdoll;

    Particle A1;
    Particle A2;
    Particle A3;
    Particle A4;

    Bone boneA;

    // Start is called before the first frame update
    void Start()
    {
        A1 = new Particle();
        A2 = new Particle();
        A3 = new Particle();
        A4 = new Particle();

        boneA = new Bone(true);

        ragdoll = new Ragdoll();

        A1.set_position(1, 2, 1);
        A2.set_position(-2, 0, 2);
        A3.set_position(2, 0, -2);
        A4.set_position(-2, 0, -2);

        boneA.init(ragdoll, A1, A2, A3, A4);
        // boneA.m_obb.m_cube.name = "flat bone";

        boneA.set_cube_pos(new Vector3(1, 1, 1));
        
    }

    // Update is called once per frame
    void Update()
    {
        // boneA.add_force(new Vector3(0, 0, 0), 0.1f);
        A4.add_force(new Vector3(0, 0.01f, 0), 0.1f);
        for (int i=0; i <boneA.m_stick.Count; ++i)
        {
            boneA.m_stick[i].satisfy();
        }
        boneA.update_position();
    }
}
