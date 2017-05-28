using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

	public float m_food;
	public int m_parts;

	// Use this for initialization
	void Start () {
		m_food = Random.value * 40;
		m_parts = Mathf.RoundToInt( Random.value * 10);
	}

	void OnTriggerStay2D(Collider2D c){
		if (c.tag == "Island") {
			if (c.transform.position.x > this.transform.position.x) {
				m_food += c.GetComponent<Island> ().m_food;
				m_parts += c.GetComponent<Island> ().m_parts;
				Destroy (c.gameObject);
			}
		}
		if (c.tag == "Enemy") {
			m_food -= c.GetComponent<Enemy> ().GetDamage ();
			m_parts--;
			if (m_food < 0) {
				m_food = 0;
			}
			if (m_parts < 0) {
				m_parts = 0;
			}
			if (m_parts <= 0 && m_food <= 0) {
				foreach (Transform child in this.transform) {
					child.gameObject.SetActive (true);
				}
			}
			Destroy (c.gameObject);
		}

	}
}
