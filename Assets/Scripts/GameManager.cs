using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform levelTransform;
    public Transform playerTransform;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public float DistanceToPlayer(Component target)
    {
        return Vector3.Distance(target.transform.position,playerTransform.position);
    }
}
