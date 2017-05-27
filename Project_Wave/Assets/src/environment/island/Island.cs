using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

	public float m_food;
	public int m_parts;

	// Use this for initialization
	void Start () {
		m_food = Random.value * 20;
		m_parts = Mathf.RoundToInt( Random.value * 10);
	}
	
	// Update is called once per frame
	void Update () {
		
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
			if (c.transform.position.x > this.transform.position.x) {
				m_food -= c.GetComponent<Enemy> ().GetDamage ();
				m_parts--;
				if (m_food < 0)
					m_food = 0;
				if (m_parts < 0)
					m_parts = 0;
				Destroy (c.gameObject);
			}
		}

	}
}
