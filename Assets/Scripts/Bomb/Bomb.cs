using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float throwForceMultiplier;
	private Rigidbody bombRigidBody;
	private Vector3 lastPosSpeed;
	private Vector3 lastAngSpeed;
	private ParticleSystem wickFireParticles;
	private ParticleSystem explosionParticles;
	private GameObject deadlyArea;
	private GameObject wickTrigger;

	// Use this for initialization
	void Start () {
		bombRigidBody = GetComponent<Rigidbody>();
		wickFireParticles = transform.Find("Wick Fire").gameObject.GetComponent<ParticleSystem>();
		explosionParticles = transform.Find("Explosion Particles").gameObject.GetComponent<ParticleSystem>();
		deadlyArea = transform.Find("Deadly Area").gameObject;
		wickTrigger = transform.Find("Wick Trigger").gameObject;

		wickTrigger.SetActive(false);
		deadlyArea.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveBomb(Vector3 grabPosition, Quaternion grabRotation)
    {
		// Deactivate rigid body while grabbing
		GetComponent<Collider>().enabled = false;
		// Activate wick trigger (disabled when not grabbing to avoid accidental bomb trigering) 
		wickTrigger.SetActive(true);
		// Set object to kinematic
		bombRigidBody.isKinematic = true;
		// Set pos / rot to hand pos / rot
		bombRigidBody.MovePosition(grabPosition);
		bombRigidBody.MoveRotation(grabRotation);
		// Update current speed
		lastPosSpeed = bombRigidBody.velocity;
		lastAngSpeed =  bombRigidBody.angularVelocity;
    }

	public void ReleaseBomb(Vector3 grabPosition, Quaternion grabRotation)
	{
		// Re-Activate rigid body when releasing it
		GetComponent<Collider>().enabled = true;
		// Disable wick trigger to avoid accidental bomb trigering
		wickTrigger.SetActive(false);
		// Set object to rigidbody
		bombRigidBody.isKinematic = false;
		bombRigidBody.AddForce(lastPosSpeed * throwForceMultiplier);
		bombRigidBody.AddTorque(lastAngSpeed * throwForceMultiplier);
	}

	public void Fire(){
		// Start wick particles animation
		wickFireParticles.Play();
		// In 6 sec, explode
		StartCoroutine(timer());
	}

	 IEnumerator timer(){
		yield return new WaitForSeconds(6);
		Explode();
	 }

	 public void Explode(){
		explosionParticles.Play();
		deadlyArea.SetActive(true);
		StartCoroutine(selfDestroy());
	 }

	 IEnumerator selfDestroy() {
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);
	}
}
