using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageCollider : MonoBehaviour {

	private Ship shipScript;
	// Use this for initialization
	void Start () {
		shipScript = transform.parent.parent.gameObject.GetComponent<Ship>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "DEADLY_COLLIDER") {
			shipScript.TakeDamage();
			if(other.gameObject.isStatic) {
				shipScript.MoveBackwards();
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "EDGE_COLLIDER") {
			shipScript.AvoidEdgeLimit();
		}
	}
}
