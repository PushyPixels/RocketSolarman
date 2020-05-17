using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnButtonDown : MonoBehaviour
{
    public string buttonName = "Fire1";
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(buttonName))
        {
            Instantiate(prefab,transform.position,transform.rotation);
        }
    }
}
