using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

	public GameObject splashEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "OCEAN_COLLIDER"){
			// Emit a "splash" particle effect
			GameObject splashParticles = (GameObject) Instantiate(splashEffect, 
                               transform.position,  Quaternion.Euler(new Vector3(90, 0, 0)));
			splashParticles.transform.localScale = new Vector3(2,2,2);
		}
		
	}
}
