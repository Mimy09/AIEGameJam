using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public enum EnemyType
	{
		Other,
		PissedPirates,
		PassivePirates,
	};

	public EnemyType type;
	public float m_damage;

	private Transform player;

	// Use this for initialization
	void Start () {
		this.player = GameObject.FindGameObjectWithTag ("Player").transform;
		m_damage = Random.value * 20;
	}

	void Update()
	{
		if (GameStateManager.GetState () == GameState.GAME_STATE.IslandGUIState || GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState)
		{
			switch (this.type) {
				case EnemyType.PissedPirates:
					PirateMove ();
					PirateFollow ();
					break;
				case EnemyType.PassivePirates:
					PirateMove ();
					break;
			}
		}

		bool checkAxis = Vector3.Dot(transform.up, Vector3.up) > 0;
		GetComponent<SpriteRenderer> ().flipY = !checkAxis;
	}

	void PirateMove()
	{
		transform.position += transform.right * Time.deltaTime / 4;
	}
	void PirateFollow()
	{
		Vector3 dir = player.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion newRot = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (this.transform.rotation, newRot, Time.deltaTime / 2);




		//float rotZ = (transform.eulerAngles.z < 0 ? -transform.eulerAngles.z : transform.eulerAngles.z) % 360;
		//Quaternion rot = Quaternion.LookRotation ((player.right + player.up) - (this.transform.right + this.transform.up));
		//Quaternion.Slerp (this.transform.rotation, rot, Time.deltaTime * 200);
	}

	public float GetDamage() { return m_damage; }

	void OnTriggerStay2D(Collider2D c){
		if (c.tag == "Enemy") {
			if (this.type == EnemyType.PissedPirates) return;
			if (c.GetComponent<Enemy> ().type == EnemyType.PissedPirates) return;
			if (c.transform.position.x > this.transform.position.x) {
				m_damage += c.GetComponent<Enemy> ().GetDamage ();
				Destroy (c.gameObject);
			}
		}
	}
}
