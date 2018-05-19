using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{

}
public class Bomb : MonoBehaviour {

	public float throwForceMultiplier;
	private Rigidbody bombRigidBody;
	private ParticleSystem wickFireParticles;
	private ParticleSystem explosionParticles;
	private GameObject deadlyArea;
	private GameObject wickTrigger;
    private FixedJoint fixedJoint;

    private Vector3 lastPosition;
    private Quaternion lastAngle;
    private float timerValue;

    private AudioSource explosionSound;
    private AudioSource wickSound;

    private Vector3[] lastPos; // contains all the previous positions of the bombs, a few milliseconds before the current time
    public int lastPosSize;
    private int firstPositionIndex; // the index of lastPos where the oldest position is stored
    public float frequencyPosSample; // how much time between position sample
    private float lastSampleTime; // record the time where the last position sample was done
    //private float firstSampleTime; // record the time where the oldest position sample was done

    // Use this for initialization
    void Start () {
		bombRigidBody = GetComponent<Rigidbody>();
		wickFireParticles = transform.Find("Wick Fire").gameObject.GetComponent<ParticleSystem>();
		explosionParticles = transform.Find("Explosion Particles").gameObject.GetComponent<ParticleSystem>();
		deadlyArea = transform.Find("Deadly Area").gameObject;
		wickTrigger = transform.Find("Wick Trigger").gameObject;

        explosionSound = GetComponents<AudioSource>()[0];
        wickSound = GetComponents<AudioSource>()[1];

        wickTrigger.SetActive(false);
		deadlyArea.SetActive(false);

        /*lastPosition = transform.position;
        lastAngle = transform.rotation;*/

        lastPos = new Vector3[lastPosSize];
        firstPositionIndex = 0;
        lastSampleTime = Time.time;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		/*if (Time.time - timerValue > 0.02f)
         {
            timerValue = Time.time;
            lastPosition = transform.position;
            lastAngle = transform.rotation;
        }*/

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

	public void MoveBomb(Vector3 grabPosition, Quaternion grabRotation, GameObject HandInteractor)
    {
		// Deactivate rigid body while grabbing
		//GetComponent<Collider>().enabled = false;
		// Activate wick trigger (disabled when not grabbing to avoid accidental bomb trigering) 
		wickTrigger.SetActive(true);
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
        }
    }

	public void ReleaseBomb(Vector3 grabPosition, Quaternion grabRotation, GameObject HandInteractor)
	{
		// Re-Activate rigid body when releasing it
		//GetComponent<Collider>().enabled = true;
		// Disable wick trigger to avoid accidental bomb trigering
		wickTrigger.SetActive(false);
        // Set object to rigidbody
        //bombRigidBody.isKinematic = false;
        Destroy(fixedJoint);
        Vector3 lastPosSpeed = transform.position - lastPos[firstPositionIndex];
        //Quaternion lastAngleSpeed = transform.rotation - lastAngle;
        bombRigidBody.AddForce(throwForceMultiplier * lastPosSpeed);
        //bombRigidBody.AddTorque(lastAngSpeed * throwForceMultiplier);
  

    }

	public void Fire(){
		// Start wick particles animation
		wickFireParticles.Play();
        wickSound.Play();
        // In 6 sec, explode
        StartCoroutine(timer());
	}

	 IEnumerator timer(){
		yield return new WaitForSeconds(6);
		Explode();
	 }

	 public void Explode(){
		explosionParticles.Play();
        explosionSound.Play();
        wickSound.Stop();
        deadlyArea.SetActive(true);
		StartCoroutine(selfDestroy());
	 }

	 IEnumerator selfDestroy() {
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
        if (other.gameObject.tag == "ENEMY_COLLIDER")
        {
            Explode();
        }
    }
    }
