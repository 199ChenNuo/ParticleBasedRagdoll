using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.8f, 0);
    public Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = new Ragdoll();
    }

    // Update is called once per frame
    void Update()
    {
        ragdoll.run(gravity, Time.deltaTime);
    }

    void AddParticles()
    {
        // TODO: implement
    }

    void AddBones()
    {
        // TODO: implement
    }

    void AddJoints()
    {
        // TODO: implement
    }
}
