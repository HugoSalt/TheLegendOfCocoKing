using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectArea : MonoBehaviour {

	private EnemyCannon enemyCannonScript;
	// Use this for initialization
	void Start () {
		enemyCannonScript = transform.parent.GetComponent<EnemyCannon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == "MYSHIP_COLLIDER"){

			Vector3 targetPos = other.gameObject.transform.position ;
			enemyCannonScript.FireAt(targetPos);

		}
		
	}

}
