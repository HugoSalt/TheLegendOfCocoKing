using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHandle : MonoBehaviour {

	private Vector3 lastGrabPos;
	private Rigidbody wheelRigidBody;
	public float wheelGrabForceMultiply;
	// Use this for initialization
	void Start () {
		wheelRigidBody = transform.parent.parent.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {
		
		if (other.gameObject.tag == "HAND_INTERACTOR"){

			if (other.gameObject.GetComponent<HandInteractor>().IsGrabbing) {

				Vector3 currentGrabPos = other.gameObject.transform.position ;
				Vector3 handleCenter = transform.position;
				// Apply Force on wheel (ship will read on its own
				// the wheel's torque velocity)
				wheelRigidBody.AddForceAtPosition(handleCenter,(handleCenter-currentGrabPos)*wheelGrabForceMultiply);
				Debug.DrawLine(currentGrabPos, handleCenter, Color.red);

			}

		}
		
	}

}
