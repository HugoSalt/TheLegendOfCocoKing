using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandle : MonoBehaviour {

	private Bomb bombScript;
    private bool startedGrabbing;

	// Use this for initialization
	void Start () {
		bombScript = transform.parent.GetComponent<Bomb>();
        startedGrabbing = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == "HAND_INTERACTOR") {

			Vector3 currentGrabPos = other.gameObject.transform.position;
			Quaternion currentGrabRot =  other.gameObject.transform.rotation;

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing()) {
				bombScript.MoveBomb(currentGrabPos, currentGrabRot, other.gameObject);
                startedGrabbing = true;
            } else if (startedGrabbing) {
				bombScript.ReleaseBomb(currentGrabPos, currentGrabRot, other.gameObject);
                startedGrabbing = false;
            }

		}
		
	}
}
