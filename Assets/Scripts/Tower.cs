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
	
	// Use this for initialization
	void Start () {
		// All children to kinematic
		foreach (Transform child in transform)
             child.GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Destroy(){
		// Destroy enemy cannons gameobjects
		Destroy(CannonToDestroy1);
		Destroy(CannonToDestroy2);
		Destroy(CannonToDestroy3);
		Destroy(CannonToDestroy4);
		Destroy(CannonToDestroy5);
		Destroy(CannonToDestroy6);
		Destroy(CannonToDestroy7);
		Destroy(CannonToDestroy8);
		// Play FX
		transform.Find("CFX2_RockHit").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (1)").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (2)").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (3)").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (4)").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (5)").GetComponent<ParticleSystem>().Play();
		transform.Find("CFX2_RockHit (6)").GetComponent<ParticleSystem>().Play();
		// Collapse the tower
		foreach (Transform child in transform)
             child.GetComponent<Rigidbody>().isKinematic = false;
	}
}
