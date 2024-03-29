﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WorldGrabMovement : MonoBehaviour
{
    public HandInfo leftHand;
    public HandInfo rightHand;
    public LayerMask grabbableObjectLayerMask = -1;

    [System.Serializable]
    public class HandInfo
    {
        [Header("Required Object References")]
        public Rigidbody rigidbody;
        public string gripAxis;

        [Header("Debug Info")]
        public HandState state;
        public Vector3 grabOffset;
        public Rigidbody grippedObject;
        public Vector3 previousPostion;
        public bool skip;
    }
    public enum HandState { Empty, MovementGrab, ObjectGrab }

    [Header("Required Component References")]
    public new Rigidbody rigidbody;

	// Update is called once per frame
	void Update ()
    {
        ObjectGrabbingLogic(leftHand);
        ObjectGrabbingLogic(rightHand);

        WorldMovementLogic(leftHand,rightHand);
        WorldMovementLogic(rightHand,leftHand);

        if(leftHand.skip && Input.GetAxis(leftHand.gripAxis) < 0.25f)
        {
            leftHand.skip = false;
        }
        if(rightHand.skip && Input.GetAxis(rightHand.gripAxis) < 0.25f)
        {
            rightHand.skip = false;
        }

        leftHand.previousPostion = leftHand.rigidbody.transform.position;
        rightHand.previousPostion = rightHand.rigidbody.transform.position;
    }

    void ObjectGrabbingLogic(HandInfo hand)
    {
        Collider[] colliders = Physics.OverlapSphere(hand.previousPostion, 0.05f*transform.localScale.x, grabbableObjectLayerMask);
        if (Input.GetAxis(hand.gripAxis) >= 0.25f)
        {
            if (colliders.Length > 0 && hand.state == HandState.Empty)
            {
                Collider firstColliderFound = colliders[0];
                Rigidbody rb = firstColliderFound.GetComponent<Rigidbody>();

                Spawnable spawnable = firstColliderFound.GetComponent<Spawnable>();
                if(spawnable)
                { 
                    rb = Instantiate(spawnable.spawnPrefab,rb.transform.position,Quaternion.identity,GameManager.instance.levelTransform);
                    rb.transform.localPosition = Vector3.Scale(rb.transform.localPosition, new Vector3(1.0f,1.0f,0.0f));
                }

                if(rb != null)
                {
                    rb.isKinematic = false;
                    hand.grabOffset = firstColliderFound.transform.position - hand.rigidbody.transform.position;
                    hand.grippedObject = rb;
                    FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                    joint.enablePreprocessing = false;
                    joint.connectedBody = hand.rigidbody;
                    hand.state = HandState.ObjectGrab;
                    hand.rigidbody.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }
        }
        if (hand.state == HandState.ObjectGrab && Input.GetAxis(hand.gripAxis) < 0.25f)
        {
            Rigidbody rb = hand.grippedObject;
            FixedJoint joint = rb.GetComponent<FixedJoint>();
            Destroy(joint);
            
            if (rb != null)
            {
                rb.isKinematic = true;
                //rb.velocity = (hand.rigidbody.transform.position - hand.previousPostion) / Time.deltaTime;
            }

            hand.state = HandState.Empty;
            hand.rigidbody.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    void WorldMovementLogic(HandInfo hand, HandInfo oppositeHand)
    {
        if(!hand.skip && hand.state != HandState.ObjectGrab)
        {
            if (Input.GetAxis(hand.gripAxis) >= 0.25f)
            {
                rigidbody.velocity = Vector3.zero;
                transform.position += hand.previousPostion - hand.rigidbody.transform.position;
                hand.state = HandState.MovementGrab;

                if(oppositeHand.state == HandState.MovementGrab)
                {
                    oppositeHand.skip = true;
                    oppositeHand.state = HandState.Empty;
                }
            }
            if (hand.state == HandState.MovementGrab && Input.GetAxis(hand.gripAxis) < 0.25f)
            {
                rigidbody.velocity = (hand.previousPostion - hand.rigidbody.transform.position) / Time.deltaTime;
                hand.state = HandState.Empty;
            }
        }
    }

    void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
}
