using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	PlayerMovment m_pm;

	void Start () {
		m_pm = this.GetComponent<PlayerMovment> ();
	}

	void Update () { }

	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "Enemy") {
			transform.Rotate (new Vector3 (0, 0, 180));
		}
	}
}
