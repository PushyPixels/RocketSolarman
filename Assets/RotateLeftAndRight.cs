using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftAndRight : MonoBehaviour
{
    public float rotationSpeed = 60.0f;
    public string axisName = "Horizontal";

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,Input.GetAxis(axisName)*rotationSpeed*Time.deltaTime);
    }
}
