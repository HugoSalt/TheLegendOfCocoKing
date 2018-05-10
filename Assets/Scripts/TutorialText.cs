using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour {

	private Shader textShader;
	private float alpha;
	public float timer;
	private Renderer rend;

	void Start () {
		textShader = GetComponent<Shader>();
		rend = GetComponent<Renderer>();
		alpha = 1.0f;
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if(timer < 0) {

			//Set the main Color of the Material to green
        	rend.material.SetColor("_Color", new Color(1, 1, 1, alpha));

			alpha -= 0.01f;
		}
		
		if(alpha < 0) {
			Destroy(this.gameObject);
		}
	}
}
