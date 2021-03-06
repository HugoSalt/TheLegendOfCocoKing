﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{

}
public class Harpoon : MonoBehaviour
{

    public float throwForceMultiplier;
    private Rigidbody harpoonRigidBody;
    private FixedJoint fixedJoint;

    private Vector3 lastPosition;
    private Quaternion lastAngle;
    private float timerValue;

    //public Camera cameraRig;

    private Vector3[] lastPos; // contains all the previous positions of the bombs, a few milliseconds before the current time
    public int lastPosSize;
    private int firstPositionIndex; // the index of lastPos where the oldest position is stored
    public float frequencyPosSample; // how much time between position sample
    private float lastSampleTime; // record the time where the last position sample was done
    //private float firstSampleTime; // record the time where the oldest position sample was done
    private bool isThrown;
    public float forceToConsiderThrow;

    // Use this for initialization
    void Start()
    {
        harpoonRigidBody = GetComponent<Rigidbody>();

        lastPos = new Vector3[lastPosSize];
        firstPositionIndex = 0;
        lastSampleTime = Time.time;
        isThrown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isThrown)
        {
            int lastIndex = firstPositionIndex - 1;
            if (lastIndex < 0)
            {
                lastIndex = lastPosSize - 1;
            }
            transform.rotation = Quaternion.LookRotation(transform.position - lastPos[lastIndex]);
        }
        if (Time.time - lastSampleTime > frequencyPosSample)
        {
            lastSampleTime = Time.time;
            // We erase the oldest position by the current position
            lastPos[firstPositionIndex] = transform.position;
            // We set the oldest position index to the oldest position among the remaining ones
            firstPositionIndex += 1;
            if (firstPositionIndex >= lastPosSize)
            {
                firstPositionIndex = 0;
            }
            lastPos[firstPositionIndex] = transform.position;
        }
    }

    public void MoveHarpoon(Vector3 grabPosition, Quaternion grabRotation, GameObject HandInteractor)
    {
        // Deactivate rigid body while grabbing
        //GetComponent<Collider>().enabled = false;
        // Activate wick trigger (disabled when not grabbing to avoid accidental bomb trigering) 
        // Set object to kinematic
        //bombRigidBody.isKinematic = true;
        // Set pos / rot to hand pos / rot
        //bombRigidBody.MovePosition(grabPosition);
        //bombRigidBody.MoveRotation(grabRotation);
        // Update current speed
        //lastPosSpeed = transform.position - lastPosition;
        //lastAngSpeed =  bombRigidBody.angularVelocity;

        if (!fixedJoint)
        {
            fixedJoint = this.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = HandInteractor.GetComponent<Rigidbody>();
            //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cameraRig.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    public void ReleaseHarpoon(Vector3 grabPosition, Quaternion grabRotation, GameObject HandInteractor)
    {
        // Re-Activate rigid body when releasing it
        //GetComponent<Collider>().enabled = true;
        // Disable wick trigger to avoid accidental bomb trigering
        // Set object to rigidbody
        //bombRigidBody.isKinematic = false;
        Destroy(fixedJoint);
        Vector3 lastPosSpeed = transform.position - lastPos[firstPositionIndex];
        //Quaternion lastAngleSpeed = transform.rotation - lastAngle;
        harpoonRigidBody.AddForce(throwForceMultiplier * lastPosSpeed);
        //bombRigidBody.AddTorque(lastAngSpeed * throwForceMultiplier);
        if (lastPosSpeed.magnitude > forceToConsiderThrow)
        {
            isThrown = true;
        }
        else
        {
            isThrown = false;
        }


    }

    IEnumerator selfDestroy()

    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshRenderer>()) child.GetComponent<MeshRenderer>().enabled = false;
        }
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MYSHIP_COLLIDER")
        {
            isThrown = false;
        }
    }
}
