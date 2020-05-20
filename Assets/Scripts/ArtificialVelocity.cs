using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialVelocity : MonoBehaviour
{
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Add rigidbody to game manager
        velocity = -GameManager.instance.levelTransform.GetComponent<Rigidbody>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity*Time.deltaTime;
    }
}
