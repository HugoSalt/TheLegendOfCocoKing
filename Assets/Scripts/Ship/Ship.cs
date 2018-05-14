using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private Rigidbody shipRigidBody;
	public float forwardForce;

	// Defines how the oscillate when moving
	public float oscillationAmp;
	public float oscillationFreq;
	private Rigidbody wheelRigidBody;
	public float turnMultiplier;
	public float health;
	private bool isSinking;

	// 
	public float automaticVelocity;
	public float collisionIntensity;

	public 

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
		if (isSinking) {
			transform.Translate(- Vector3.up * 0.001f);
		} else {
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

	public void TakeDamage() {
		health -= 1;
		if (health == 0) DestroyShip();
		foreach (Transform child in transform.Find("CollisionFX"))
			child.GetComponent<ParticleSystem>().Play();
		GetComponents<AudioSource>()[1].Play();	
	}

	private void DestroyShip() {
		isSinking = true;
		foreach (Transform child in transform.Find("SinkingFX"))
			child.GetComponent<ParticleSystem>().Play();		
	}

	public void AvoidEdgeLimit() {
		shipRigidBody.AddTorque(automaticVelocity * turnMultiplier * transform.up);
	}

	public void MoveBackwards() {
		shipRigidBody.AddForce(-forwardForce * transform.forward * collisionIntensity);
	}


}
