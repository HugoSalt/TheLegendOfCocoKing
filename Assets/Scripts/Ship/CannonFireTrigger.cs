using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireTrigger : MonoBehaviour {

	private Cannon cannonScript; 

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

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing) {
				
				cannonScript.Fire();
			
			}

		}
		
	}
}
