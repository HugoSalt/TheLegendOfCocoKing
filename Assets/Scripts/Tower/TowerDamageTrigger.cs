﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamageTrigger : MonoBehaviour {

	public Tower towerScript;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "DEADLY_COLLIDER"){

			towerScript.TakeDamage();

		}
		
	}

}
