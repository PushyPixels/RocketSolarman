using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIntoLevelOnButtonDown : MonoBehaviour
{
    public string buttonName = "Fire1";
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(buttonName))
        {
            Instantiate(prefab,transform.position,transform.rotation,GameManager.instance.levelTransform);
        }
    }
}
