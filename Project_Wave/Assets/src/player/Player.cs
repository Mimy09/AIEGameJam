using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

[System.Serializable]
public struct PlayerStats{
	public float m_health;
	public float m_armor;
	public float m_speed;
	public float m_food;
}

[RequireComponent(typeof(PlayerMovment))]
public class Player : MonoBehaviour {
	PlayerMovment m_pm;
	public PlayerStats m_playerStats;

	void Start () {
		m_pm = this.GetComponent<PlayerMovment> ();

		// Default player stats
		m_playerStats.m_health = 100;
		m_playerStats.m_food = 100;
		m_playerStats.m_armor = 10;
		m_playerStats.m_speed = 2;
	}

	void Update () {
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState) {
			m_playerStats.m_food -= 1 * Time.deltaTime;
			GameStateManager.SetState (new GameRunningState (m_playerStats));
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "Island") {
			transform.Rotate (new Vector3 (0, 0, 180));
			GameStateManager.SetState (new IslandGUIState (c.GetComponent<Island>().m_food, c.GetComponent<Island>().m_parts));

			m_playerStats.m_food += c.GetComponent<Island>().m_food;
			m_playerStats.m_speed += c.GetComponent<Island>().m_parts;
			c.GetComponent<Island> ().m_food = 0;
			c.GetComponent<Island> ().m_parts = 0;
		}
		if (c.tag == "Enemy") {
			m_playerStats.m_health -= c.GetComponent<Enemy> ().GetDamage();
		}
	}
}
