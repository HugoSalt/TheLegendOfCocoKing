using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractor : MonoBehaviour {

	public bool IsGrabbing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsGrabbing) {
			GetComponent<Renderer>().material.color = Color.yellow;
		} else {
			GetComponent<Renderer>().material.color = Color.white;
		}
	}
}
