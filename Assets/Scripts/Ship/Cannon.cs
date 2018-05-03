using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	public float grabMultiplier;
	private GameObject rightWheel;
	private GameObject leftWheel;
	private float lastRot;
	// Use this for initialization
	void Start () {
		rightWheel = transform.Find("Cannon Meshes").Find("wheel_right").gameObject;
		leftWheel = transform.Find("Cannon Meshes").Find("wheel_left").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveCannon(Vector3 currentGrabPos, Vector3 handleCenter){
		// Move cannon using handle
		GetComponent<Rigidbody>().AddForceAtPosition(grabMultiplier * (currentGrabPos-handleCenter) , handleCenter);
		// Rotate wheels
		rightWheel.transform.Rotate(new Vector3((transform.rotation.y-lastRot)*600,0,0));
		leftWheel.transform.Rotate(new Vector3(-(transform.rotation.y-lastRot)*600,0,0));
		lastRot = transform.rotation.y;
		Debug.DrawRay(rightWheel.transform.position, rightWheel.transform.right, Color.red);
	}
}
