using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public GameObject CannonToDestroy1;
	public GameObject CannonToDestroy2;
	public GameObject CannonToDestroy3;
	public GameObject CannonToDestroy4;
	public GameObject CannonToDestroy5;
	public GameObject CannonToDestroy6;
	public GameObject CannonToDestroy7;
	public GameObject CannonToDestroy8;
	public GameObject ColliderToDestroy1;
	public GameObject ColliderToDestroy2;
	public GameObject ColliderToDestroy3;
	public int health;
	
	// Use this for initialization
	void Start () {
		// All children to kinematic
		foreach (Transform child in transform)
            if (child.GetComponent<Rigidbody>()) child.GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DestroyTower() {
		// Play FX
		foreach (Transform child in transform.Find("FX"))
			child.GetComponent<ParticleSystem>().Play();
		// Collapse the tower
		foreach (Transform child in transform)
            if (child.GetComponent<Rigidbody>()) child.GetComponent<Rigidbody>().isKinematic = false;
		// Destroy enemy cannons gameobjects
		Destroy(CannonToDestroy1);
		Destroy(CannonToDestroy2);
		Destroy(CannonToDestroy3);
		Destroy(CannonToDestroy4);
		Destroy(CannonToDestroy5);
		Destroy(CannonToDestroy6);
		Destroy(CannonToDestroy7);
		Destroy(CannonToDestroy8);
		Destroy(ColliderToDestroy1);
		Destroy(ColliderToDestroy2);
		Destroy(ColliderToDestroy3);
	}

	public void TakeDamage() {
		health -= 1;
		if (health == 0) DestroyTower();
	}

}
