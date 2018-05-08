using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWick : MonoBehaviour {

	private Bomb bombScript;

	// Use this for initialization
	void Start () {
		bombScript = transform.parent.GetComponent<Bomb>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {
		
		if (other.gameObject.tag == "HAND_INTERACTOR") {

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing()) {
				
				bombScript.Fire();
			
			}

		}
		
	}

}
