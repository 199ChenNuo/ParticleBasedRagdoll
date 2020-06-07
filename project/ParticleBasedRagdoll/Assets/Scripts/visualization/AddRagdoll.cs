using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRagdoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void OnClick()
    {
        Debug.Log("Add ragdoll");
        SingeRagdoll ragdoll = this.gameObject.AddComponent<SingeRagdoll>();
    }
}
