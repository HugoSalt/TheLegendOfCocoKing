using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float grabMultiplier;
    private GameObject rightWheel;
    private GameObject leftWheel;
    private float lastRot;
	private float lastFireTime;
    public Rigidbody cannonBall;
    public float cannonBallFireSpeed;
    private ParticleSystem wickFireParticles;
    private ParticleSystem cannonFireParticles;
    // Use this for initialization
    void Start()
    {
        rightWheel = transform.Find("Cannon Meshes").Find("wheel_right").gameObject;
        leftWheel = transform.Find("Cannon Meshes").Find("wheel_left").gameObject;
		lastFireTime = 0.0f;
        wickFireParticles = transform.Find("Wick Fire").gameObject.GetComponent<ParticleSystem>();
        cannonFireParticles = transform.Find("Cannon Fire").gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCannon(Vector3 currentGrabPos, Vector3 handleCenter)
    {
        // Move cannon using handle
        GetComponent<Rigidbody>().AddForceAtPosition(grabMultiplier * (currentGrabPos - handleCenter), handleCenter);
        // Rotate wheels
        rightWheel.transform.Rotate(new Vector3((transform.rotation.y - lastRot) * 600, 0, 0));
        leftWheel.transform.Rotate(new Vector3(-(transform.rotation.y - lastRot) * 600, 0, 0));
        lastRot = transform.rotation.y;
        Debug.DrawRay(rightWheel.transform.position, rightWheel.transform.right, Color.red);
    }

    public void Fire()
    {
		// Check at least 2 sec elapsed since last fire
		if (Time.time - lastFireTime > 2) {
			lastFireTime = Time.time;
			wickFireParticles.Play();
        	// Wait 2sec and fire canonball
        	StartCoroutine(throwCannonBall());
		}
    }

    IEnumerator throwCannonBall()
    {
		yield return new WaitForSeconds(2);
        cannonFireParticles.Play();
        Rigidbody cannonBallClone = (Rigidbody) Instantiate(cannonBall, 
                               transform.TransformPoint(new Vector3(0,1,6)), transform.rotation);
        cannonBallClone.velocity = transform.forward * cannonBallFireSpeed;
        // After 6sec destroy the cannonball
        yield return new WaitForSeconds(6);
        Destroy(cannonBallClone.gameObject);
    }
}
