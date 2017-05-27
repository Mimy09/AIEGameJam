using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]float m_damage;

	// Use this for initialization
	void Start () {
		m_damage = Random.value * 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetDamage() { return m_damage; }

	void OnTriggerStay2D(Collider2D c){
		if (c.tag == "Enemy") {
			if (c.transform.position.x > this.transform.position.x) {
				m_damage += c.GetComponent<Enemy> ().GetDamage ();
				Destroy (c.gameObject);
			}
		}
	}
}
