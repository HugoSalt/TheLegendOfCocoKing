using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipDamageCollider : MonoBehaviour {

	private EnemyShip shipScript;
	// Use this for initialization
	void Start () {
		shipScript = transform.parent.gameObject.GetComponent<EnemyShip>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "DEADLY_COLLIDER") {
			shipScript.TakeDamage();
		}	
	}
}
