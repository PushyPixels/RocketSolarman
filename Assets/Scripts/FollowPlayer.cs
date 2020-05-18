﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
