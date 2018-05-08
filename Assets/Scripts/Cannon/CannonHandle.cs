using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHandle : MonoBehaviour {

	private Cannon cannonScript;
	private Vector3 lastGrabPos;
	// Use this for initialization
	void Start () {
		// Get Parent Cannon Script
		cannonScript = transform.parent.parent.gameObject.GetComponent<Cannon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == "HAND_INTERACTOR"){

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing()) {

				Vector3 currentGrabPos = other.gameObject.transform.position ;
				Vector3 handleCenter = transform.position;
				cannonScript.MoveCannon(currentGrabPos, handleCenter);
				Debug.DrawLine(currentGrabPos, handleCenter, Color.red);

			}

		}
		
	}
}
