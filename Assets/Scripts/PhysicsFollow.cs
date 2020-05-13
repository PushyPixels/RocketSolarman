using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsFollow : MonoBehaviour
{
    public Rigidbody target;

    public new Rigidbody rigidbody;

    // Update is called once per physics update
    void FixedUpdate()
    {
        rigidbody.velocity = target.velocity;
    }

    void OnValidate()
    {
        if(rigidbody == null)
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
        }
    }
}
