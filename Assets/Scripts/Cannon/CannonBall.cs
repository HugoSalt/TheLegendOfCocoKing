﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

	public GameObject splashEffect;
	public GameObject hitShipEffect;
	public GameObject otherCollisionsEffect;
	// Use this for initialization
	private AudioSource splashSound;
	private AudioSource hitShipSound;
	private AudioSource otherCollisionsSound;
	private ParticleSystem whiteSmoke;
	void Start () {
		splashSound = GetComponents<AudioSource>()[0];
		hitShipSound = GetComponents<AudioSource>()[1];
		otherCollisionsSound = GetComponents<AudioSource>()[2];
		whiteSmoke = transform.Find("WhiteSmoke").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "OCEAN_COLLIDER"){
			// No smoke in water
			whiteSmoke.Stop();
			// Emit a "splash" particle effect
			GameObject splashParticles = (GameObject) Instantiate(splashEffect, 
                               transform.position,  Quaternion.Euler(new Vector3(90, 0, 0)));
			splashParticles.transform.localScale = new Vector3(2,2,2);
			// Play splash sound
			splashSound.Play();
		}
		else if (other.gameObject.tag == "MYSHIP_COLLIDER") {
			GameObject hitShipParticles = (GameObject) Instantiate(hitShipEffect, 
                               transform.position,  Quaternion.Euler(new Vector3(90, 0, 0)));
			hitShipParticles.transform.localScale = new Vector3(2,2,2);
			hitShipSound.Play();
		}
		else if (other.gameObject.tag != "DETECT_AREA") {
			GameObject otherCollisionsParticles = (GameObject) Instantiate(otherCollisionsEffect, 
                               transform.position,  Quaternion.Euler(new Vector3(90, 0, 0)));
			otherCollisionsParticles.transform.localScale = new Vector3(2,2,2);
			otherCollisionsSound.Play();
		}
		// Destroy this cannon ball after a few seconds to let the sounds finish playing
		StartCoroutine(selfDestroy());
	}

	IEnumerator selfDestroy() {
		yield return new WaitForSeconds(4);
		Destroy(this.gameObject);
	}
}
