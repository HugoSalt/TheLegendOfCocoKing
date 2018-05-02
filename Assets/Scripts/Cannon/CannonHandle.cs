using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHandle : MonoBehaviour {

	private Cannon cannonScript;
	private Vector3 lastGrabPos;
	// Use this for initialization
	void Start () {
		cannonScript = transform.parent.parent.gameObject.GetComponent<Cannon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == "HandManipulator"){

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing) {

				Vector3 currentGrabPos = other.gameObject.transform.position ;
				cannonScript.MoveCannonFromGrabIncrement(lastGrabPos, currentGrabPos);
				lastGrabPos = currentGrabPos;

			}

		}
		
	}
}
