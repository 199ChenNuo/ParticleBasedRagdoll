using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public virtual void satisfy()
    {
        // Base class function.
        // Debug.Log("Joint@satisfy()");
    }

    public virtual void init(RagdollBone boneA, RagdollBone boneB)
    {
        // Base class function.
        // Debug.Log("Joint@init()");
    }
}
