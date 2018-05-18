using System.Collections;
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

    public Camera cameraRig;

    // Use this for initialization
    void Start()
    {
        harpoonRigidBody = GetComponent<Rigidbody>();

        lastPosition = transform.position;
        lastAngle = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - timerValue > 0.02f)
        {
            timerValue = Time.time;
            lastPosition = transform.position;
            lastAngle = transform.rotation;
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
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cameraRig.transform.eulerAngles.y, transform.eulerAngles.z);
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
        Vector3 lastPosSpeed = transform.position - lastPosition;
        //Quaternion lastAngleSpeed = transform.rotation - lastAngle;
        harpoonRigidBody.AddForce(throwForceMultiplier * lastPosSpeed);
        //bombRigidBody.AddTorque(lastAngSpeed * throwForceMultiplier);


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
}
