using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using GameState;


[System.Serializable]
public struct PlayerStats{
	public float m_health;
	public float m_armor;
	public float m_speed;
	public float m_food;
	public float m_parts;
}

[RequireComponent(typeof(PlayerMovment))]
public class Player : MonoBehaviour {
	PlayerMovment m_pm;
	public PlayerStats m_playerStats;
	private PlayerStats m_plyUpgrades;

	public float itemTimer;
	public PlayerStats m_islandStats;

	bool checkAxis;

	void Start () {
		m_pm = this.GetComponent<PlayerMovment> ();

		// Default player stats
		m_playerStats.m_health = 100;
		m_playerStats.m_food = 100;
		m_playerStats.m_armor = 10;
		m_playerStats.m_speed = 2;
		m_playerStats.m_parts = 0;

		m_plyUpgrades.m_health = 1;
		m_plyUpgrades.m_armor = 1;
		m_plyUpgrades.m_speed = 1;

		itemTimer = 0;
	}

	void Update () {
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState) {
			if (m_playerStats.m_food > 0)
				m_playerStats.m_food -= 1 * Time.deltaTime;
			else {
				m_playerStats.m_food = 0;
				m_playerStats.m_health -= 2 * Time.deltaTime;
			}
			if (m_playerStats.m_armor < 0) m_playerStats.m_armor = 0;

			GameStateManager.SetState (new GameRunningState (m_playerStats));

			if (m_playerStats.m_health <= 0)
				SceneManager.LoadScene ("Test");

			if (itemTimer > 0) {
				itemTimer -= Time.deltaTime;
			}
		}

		checkAxis = Vector3.Dot(transform.up, Vector3.up) > 0;
		gameObject.GetComponentInChildren<SpriteRenderer> ().flipY = !checkAxis;
	}

	void OnGUI(){
		GUI.skin.label.fontSize = 24;
		GUI.skin.label.normal.textColor = Color.black;
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState) {
			if (itemTimer > 0) {
				GUI.DrawTexture(new Rect (Screen.width / 2 - 100, Screen.height - 400, 200, 70), Resources.Load("textures/UI/islandseconds") as Texture2D, ScaleMode.StretchToFill);
				GUI.Label (new Rect (Screen.width / 2 - 60, Screen.height - 400, 200, 70), "+Food (" + (int)m_islandStats.m_food + ")\n+Parts(" + (int)m_islandStats.m_parts + ")");
			}
		}
		if (GameStateManager.GetState () == GameState.GAME_STATE.PlayerUpgradeState) {
			GUI.Box (new Rect (100, 100, Screen.width - 200, Screen.height - 200), "");
			GUI.skin.box.fontSize = 72;
			GUI.Box (new Rect (150, 150, Screen.width - 300, 100), "UPGRADES");
			GUI.skin.box.fontSize = 32;
			GUI.Box (new Rect (150, 300, Screen.width - 300, Screen.height - 450), "");

			if (GUI.Button (new Rect (175, 325, 200, 50), "HEALTH++ ("+m_plyUpgrades.m_health+")")) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_health) {
					m_playerStats.m_parts -= m_plyUpgrades.m_health;
					m_plyUpgrades.m_health++;
					m_playerStats.m_health += 25;
				}
			}
			if (GUI.Button (new Rect (175, 400, 200, 50), "ARMOR++ ("+m_plyUpgrades.m_armor+")")) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_armor) {
					m_playerStats.m_parts -= m_plyUpgrades.m_armor;
					m_plyUpgrades.m_armor++;
					m_playerStats.m_armor += 2;
				}
			}
			if (GUI.Button (new Rect (175, 475, 200, 50), "SPEED++ ("+m_plyUpgrades.m_speed+")")) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_speed) {
					m_playerStats.m_parts -= m_plyUpgrades.m_speed;
					m_plyUpgrades.m_speed++;
					m_playerStats.m_speed += 0.2f;
				}
			}
			GUI.skin.label.fontSize = 32;
			GUI.Label (new Rect (400, 325, 600, 50), "Current Health: "+((int)m_playerStats.m_health).ToString() + " + 25");
			GUI.Label (new Rect (400, 400, 600, 50), "Current Armor: "+((int)m_playerStats.m_armor).ToString() + " + 2");
			GUI.Label (new Rect (400, 475, 600, 50), "Current Speed: "+m_playerStats.m_speed.ToString() + " + 0.2");

			GUI.Label (new Rect (175, 550, 400, 70), "Current Parts: "+m_playerStats.m_parts.ToString());

			if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 150, 100, 50), "Continue")) {
				GameStateManager.SetState (new GameRunningState ());
			}

		}
	}

	void ShowText(float food, float parts){
		itemTimer = 3;
		m_islandStats.m_food = food;
		m_islandStats.m_parts = parts;
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "Island") {
			Vector3 normal = Vector3.Normalize(transform.position - c.transform.position);
			Vector3 reflect = Vector3.Reflect (transform.right, normal);
			float angle = Mathf.Atan2 (reflect.y, reflect.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			//transform.Rotate (new Vector3(0, 0, 180));

			GameStateManager.SetState (new IslandGUIState (c.GetComponent<Island>().m_food, c.GetComponent<Island>().m_parts, m_playerStats.m_speed));

			m_playerStats.m_food += c.GetComponent<Island>().m_food;
			m_playerStats.m_parts += c.GetComponent<Island>().m_parts;
			ShowText (c.GetComponent<Island> ().m_food, c.GetComponent<Island> ().m_parts);
			c.GetComponent<Island> ().m_food = 0;
			c.GetComponent<Island> ().m_parts = 0;
		}
		if (c.tag == "Enemy") {
			if (m_playerStats.m_armor > 0) {
				m_playerStats.m_health -= c.GetComponent<Enemy> ().GetDamage () / (m_playerStats.m_armor / 3);
				m_playerStats.m_armor -= c.GetComponent<Enemy> ().GetDamage () / 3;
			} else {
				m_playerStats.m_health -= c.GetComponent<Enemy> ().GetDamage ();
			}
		}
	}
}
