using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveCannonFromGrabIncrement(Vector3 lasGrabPos, Vector3 newGrabPos){
		Vector3 grabIncrementVec = newGrabPos - lasGrabPos;
		Vector3 lastGrabToCenterVec = transform.position - lasGrabPos;
		Vector3 newGrabToCenterVec = transform.position - lasGrabPos;
		float angle =  Vector3.Angle(lasGrabPos - transform.position, newGrabPos - transform.position);
		transform.RotateAround(Vector3.up, angle*Mathf.Deg2Rad);
	}
}
