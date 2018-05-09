using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour {
	public Rigidbody cannonBall;
	public float targetInfluenceStrength;
	public int cannonBallMaxSpeed;
	public int cannonBallMinSpeed;
	public float reloadTime;
	private float lastFireTime;
	private ParticleSystem cannonFireParticles;
	private ParticleSystem smokeParticles;
	private AudioSource cannonSound;
	// Use this for initialization
	void Start () {
		lastFireTime = Time.time;
		cannonFireParticles = transform.Find("Cannon Fire").gameObject.GetComponent<ParticleSystem>();
        smokeParticles = transform.Find("White Smoke").gameObject.GetComponent<ParticleSystem>();
		cannonSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FireAt(Vector3 targetPos) {
		// Force a reload time
		if (Time.time - lastFireTime > reloadTime) {
			lastFireTime = Time.time;
			// Fx
			cannonFireParticles.Play();
        	smokeParticles.Play();
        	cannonSound.Play();
			// Fire cannonball prefab
			Vector3 cannonPosition = transform.Find("Cannon Fire").transform.position;
			Rigidbody cannonBallClone = (Rigidbody) Instantiate(cannonBall, 
                               cannonPosition, transform.rotation);
			Vector3 targetInfluence = (targetPos - transform.position).normalized;
			int cannonBallSpeed = Random.Range(cannonBallMinSpeed,cannonBallMaxSpeed);
			print(cannonBallSpeed);
        	cannonBallClone.velocity = (transform.forward * cannonBallSpeed) + (targetInfluence * targetInfluenceStrength) ;
		}
	}


}
