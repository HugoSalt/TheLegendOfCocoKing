using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour {

	private TextMesh textMesh;
	private float alpha;
	public float timer;

	void Start () {
		textMesh = GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if(timer < 0) {
			textMesh.color = new Color(1, 1, 1, alpha);
			alpha -= 0.01f;
		}
		
		if(alpha < 0) {
			Destroy(this.gameObject);
		}
	}
}
