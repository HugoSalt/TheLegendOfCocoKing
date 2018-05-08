using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<HandInteractor>().setGrab(true);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0,0.1f,0.1f);
		if (Time.time > 6){
			GetComponent<HandInteractor>().setGrab(false);
		}
	}
}
