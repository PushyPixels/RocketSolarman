﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicOnSleep : MonoBehaviour
{
    public new Rigidbody rigidbody;

    // Update is called once per physics update
    void FixedUpdate()
    {
        if(!rigidbody.isKinematic && rigidbody.IsSleeping())
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
