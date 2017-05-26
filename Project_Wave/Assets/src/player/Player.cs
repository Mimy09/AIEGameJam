using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	PlayerMovment m_pm;

	void Start () {
		m_pm = this.GetComponent<PlayerMovment> ();
	}

	void Update () {
		//transform.Translate (Vector2.right * 2 * Time.deltaTime);
		//this.transform.rotation = m_pm.m_rotation;

		//int PlayerRotation = this.transform.rotation.z - 90;
		//int sceneSpeed = PlayerRotation / 90;


	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "Enemy") {
			transform.Rotate (new Vector3 (0, 0, 180));
		}
	}
}
