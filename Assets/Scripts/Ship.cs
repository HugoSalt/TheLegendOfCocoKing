using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private float speed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		// Y axis (vertical) oscillation because of ocean waves
		//this.MovePosition.Translate(new Vector3(0, 1.0f, 0) * Mathf.Sin(Mathf.PI * Time.deltaTime));	
	}
}
