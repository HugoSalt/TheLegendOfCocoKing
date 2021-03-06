﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDamageCollider : MonoBehaviour {

	private Shark sharkScript;

	// Use this for initialization
	void Start () {
		sharkScript = transform.parent.gameObject.GetComponent<Shark>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "HARPOON_COLLIDER") {
			sharkScript.TakeDamage();
		}	
	}
}
