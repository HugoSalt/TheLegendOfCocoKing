using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractor : MonoBehaviour {
	// TODO make private (public for debug in editor only)
	public bool _isGrabbing;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void setGrab(bool isGrabbing){
		_isGrabbing = isGrabbing;
		if (_isGrabbing) {
			GetComponent<Renderer>().material.color = Color.yellow;
		} else {
			GetComponent<Renderer>().material.color = Color.white;
		}
	}

	public bool IsGrabbing(){
		return _isGrabbing;
	}
	
}
