using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollRunTest : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, 0, 0);
    public Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = new Ragdoll();

        ragdoll.set_up_human(Vector3.zero, 1, true);
    }

    // Update is called once per frame
    void Update()
    {
        // boneA.m_obb.m_cube.GetComponent<Rigidbody>().AddForce(0.1f, 0, 0);

        ragdoll.run(gravity, Time.deltaTime);
    }

}
