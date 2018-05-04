using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private Rigidbody shipRigidBody;
	public float forwardForce;
	public float oscillationAmp;
	public float oscillationFreq;
	private Rigidbody wheelRigidBody;
	public float turnMultiplier;

	// Use this for initialization
	void Start () {
		wheelRigidBody = transform.Find("Ship Wheel").
				transform.Find("Wheel").GetComponent<Rigidbody>();
		shipRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		// Y axis (vertical) oscillation because of ocean waves
		transform.Translate( Vector3.up
				* oscillationAmp * Mathf.Sin(2*Mathf.PI*oscillationFreq*Time.time) );
		// Constant forward force
		shipRigidBody.AddForce(forwardForce * transform.forward);
        // Get wheel rotation speed
        float angVelocity = Vector3.Dot(wheelRigidBody.angularVelocity, transform.forward);
        // Apply torque to boat
        shipRigidBody.AddTorque(angVelocity * turnMultiplier * transform.up);
    }
}
