using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 60.0f;
    public Vector3 axis = Vector3.forward;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis,rotationSpeed*Time.deltaTime);
    }
}
