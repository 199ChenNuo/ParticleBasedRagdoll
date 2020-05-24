using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickConstraintTest : MonoBehaviour
{
    public StickConstraint stick;
    Particle A;
    Particle B;

    GameObject sphereA;
    GameObject sphereB;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("stick constraint test");
        A = new Particle();
        B = new Particle();
        A.set_position(new Vector3(0, 0, 0));
        B.set_position(new Vector3(0, 1, 0));
        stick = new StickConstraint();
        stick.SetRestLength(1);
        stick.init(A, B);

        sphereA = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    // Update is called once per frame
    void Update()
    {
        A.add_force(new Vector3(0, 0.01f, 0), 0.1f);
        stick.satisfy();

        sphereA.transform.position = A.position();
        sphereB.transform.position = B.position();
    }
}
