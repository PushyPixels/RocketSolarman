using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrabMovement : MonoBehaviour
{
    public enum HandState { Empty, MovementGrab, ObjectGrab }

    public Transform leftHand;
    public Transform rightHand;
    public LayerMask grabMovementLayerMask = -1;
    public LayerMask grabbableObjectLayerMask = -1;

    [Header("Required Component References")]
    public new Rigidbody rigidbody;
    private Vector3 previousPositionLeft;
    private Vector3 previousPositionRight;
    private HandState leftHandState;
    private HandState rightHandState;
    private Vector3 leftHandGrabOffset;
    private Vector3 righHandGrabOffset;
    private Rigidbody leftHandGrippedObject;
    private Rigidbody rightHandGrippedObject;
    private bool gripHandled;

	// Update is called once per frame
	void Update ()
    {
        if(leftHandState != HandState.ObjectGrab && Physics.CheckSphere(previousPositionLeft, leftHandState == HandState.MovementGrab ? 0.1f*transform.localScale.x : 0.05f*transform.localScale.x, grabMovementLayerMask))
        {
            if (Input.GetAxis("Oculus_LGrip") >= 0.25f)
            {
                rigidbody.velocity = Vector3.zero;
                transform.position += previousPositionLeft - leftHand.position;
                leftHandState = HandState.MovementGrab;
                gripHandled = true;
            }
            if (leftHandState == HandState.MovementGrab && Input.GetAxis("Oculus_LGrip") < 0.25f)
            {
                rigidbody.velocity = (previousPositionLeft - leftHand.position) / Time.deltaTime;
                leftHandState = HandState.Empty;
            }
        }
        if(!gripHandled && rightHandState != HandState.ObjectGrab && Physics.CheckSphere(previousPositionRight, rightHandState == HandState.MovementGrab ? 0.1f*transform.localScale.x : 0.05f*transform.localScale.x, grabMovementLayerMask))
        {
            if (Input.GetAxis("Oculus_RGrip") >= 0.25f)
            {
                rigidbody.velocity = Vector3.zero;
                transform.position += previousPositionRight - rightHand.position;
                rightHandState = HandState.MovementGrab;
            }
            if (rightHandState == HandState.MovementGrab && Input.GetAxis("Oculus_RGrip") < 0.25f)
            {
                rigidbody.velocity = (previousPositionRight - rightHand.position) / Time.deltaTime;
                rightHandState = HandState.Empty;
            }
        }
        Collider[] leftHandColliders = Physics.OverlapSphere(previousPositionLeft, 0.05f*transform.localScale.x, grabbableObjectLayerMask);
        if (Input.GetAxis("Oculus_LGrip") >= 0.25f)
        {
            if (leftHandColliders.Length > 0 && leftHandState != HandState.MovementGrab)
            {
                Collider firstColliderFound = leftHandColliders[0];
                Rigidbody rb = firstColliderFound.GetComponent<Rigidbody>();

                if (leftHandState == HandState.Empty && rb != null)
                {
                    righHandGrabOffset = firstColliderFound.transform.position - leftHand.position;
                    leftHandGrippedObject = rb;
                    FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = leftHand.GetComponent<Rigidbody>();
                    leftHandState = HandState.ObjectGrab;
                }
            }
        }
        if (leftHandState == HandState.ObjectGrab && Input.GetAxis("Oculus_LGrip") < 0.25f)
        {
            Rigidbody rb = leftHandGrippedObject;
            FixedJoint joint = rb.GetComponent<FixedJoint>();
            Destroy(joint);

            if (rb != null)
            {
                rb.velocity = (leftHand.position - previousPositionLeft) / Time.deltaTime;
            }

            leftHandState = HandState.Empty;
        }

        Collider[] rightHandColliders = Physics.OverlapSphere(previousPositionRight, 0.05f*transform.localScale.x, grabbableObjectLayerMask);
        if (Input.GetAxis("Oculus_RGrip") >= 0.25f)
        {
            if (rightHandColliders.Length > 0 && rightHandState != HandState.MovementGrab)
            {
                Collider firstColliderFound = rightHandColliders[0];
                Rigidbody rb = firstColliderFound.GetComponent<Rigidbody>();

                if (rightHandState == HandState.Empty && rb != null)
                {
                    righHandGrabOffset = firstColliderFound.transform.position - rightHand.position;
                    rightHandGrippedObject = rb;
                    FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = rightHand.GetComponent<Rigidbody>();
                    rightHandState = HandState.ObjectGrab;
                }
            }
        }
        if (rightHandState == HandState.ObjectGrab && Input.GetAxis("Oculus_RGrip") < 0.25f)
        {
            //firstColliderFound.transform.parent = null;
            Rigidbody rb = rightHandGrippedObject;
            FixedJoint joint = rb.GetComponent<FixedJoint>();
            Destroy(joint);

            if (rb != null)
            {
                rb.velocity = (rightHand.position - previousPositionRight) / Time.deltaTime;
            }

            rightHandState = HandState.Empty;
        }

        previousPositionLeft = leftHand.position;
        previousPositionRight = rightHand.position;
        gripHandled = false;
    }

    void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
}
