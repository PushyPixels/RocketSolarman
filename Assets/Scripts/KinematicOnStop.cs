using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicOnStop : MonoBehaviour
{
    public new Rigidbody rigidbody;

    // Update is called once per physics update
    void FixedUpdate()
    {
        if(!rigidbody.isKinematic && rigidbody.velocity.magnitude < Physics.sleepThreshold);
        {
            rigidbody.isKinematic = true;
        }
    }

    void OnValidate()
    {
        if(rigidbody == null)
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
        }
    }
}
