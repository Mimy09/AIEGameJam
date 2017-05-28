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

	private GUIStyle style;
	private float gametimer;

	private Vector3 tsunami_pos;

	public List< Sprite > people = new List<Sprite>();
	SpriteRenderer[] peoplething;

	bool checkAxis;

	void Start () {
		m_pm = this.GetComponent<PlayerMovment> ();
		peoplething = this.gameObject.GetComponentsInChildren<SpriteRenderer> ();
		peoplething [1].sprite = people [(int)(Random.value * 25)];

		gametimer = 0;
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
		style = new GUIStyle ();
		style.fontSize = 24;
		style.normal.textColor = Color.black;
		style.alignment = TextAnchor.MiddleCenter;

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

			if (m_playerStats.m_health <= 0) GameStateManager.SetState (new EndState ());

			if (itemTimer > 0) {
				itemTimer -= Time.deltaTime;
			}
			tsunami_pos = GameObject.FindGameObjectWithTag ("tsunami").transform.position;

			if (tsunami_pos.x >= -16) GetComponent<AudioManager> ().PlayNextClip (2);
			else GetComponent<AudioManager> ().PlayNextClip (1);

			gametimer += Time.deltaTime;
		}
		if (GameStateManager.GetState () == GameState.GAME_STATE.MenuState) {
			GetComponent<AudioManager> ().PlayNextClip (0);
		}

		checkAxis = Vector3.Dot(transform.up, Vector3.up) > 0;
		gameObject.GetComponentInChildren<SpriteRenderer> ().flipY = !checkAxis;
		peoplething [1].flipY = !checkAxis;
	}

	void OnGUI(){
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState || GameStateManager.GetState () == GameState.GAME_STATE.EndState){
			GUI.DrawTexture(new Rect (Screen.width - 100, 5, 100, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.Label (new Rect (Screen.width - 100, 6, 100, 30), ((int)gametimer).ToString(), style);

		}
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState) {
			if (itemTimer > 0) {
				style.fontSize = 24;
				GUI.color = new Color(1, 1, 1, 0.7f);
				GUI.DrawTexture(new Rect (Screen.width / 2 - 100, 50, 200, 70), Resources.Load("textures/UI/islandseconds") as Texture2D, ScaleMode.StretchToFill);
				GUI.Label (new Rect (Screen.width / 2 - 100, 50, 200, 70), "+Food (" + (int)m_islandStats.m_food + ")\n+Parts(" + (int)m_islandStats.m_parts + ")", style);
				GUI.color = new Color(1, 1, 1, 1);
			}


		}
		if (GameStateManager.GetState () == GameState.GAME_STATE.PlayerUpgradeState) {
			GUI.DrawTexture(new Rect (200, 100, Screen.width - 400, Screen.height - 200), Resources.Load("textures/UI/upgrade") as Texture2D, ScaleMode.StretchToFill);
			//GUI.Box (new Rect (100, 100, Screen.width - 200, Screen.height - 200), "", style);
			//style.fontSize = 72;
			//GUI.Box (new Rect (150, 150, Screen.width - 300, 100), "UPGRADES", style);
			//style.fontSize = 32;
			//GUI.Box (new Rect (150, 300, Screen.width - 300, Screen.height - 450), "", style);

			if (GUI.Button (new Rect (335, 225, 50, 50), ""+m_plyUpgrades.m_health, style)) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_health) {
					m_playerStats.m_parts -= m_plyUpgrades.m_health;
					m_plyUpgrades.m_health++;
					m_playerStats.m_health += 25;
				}
			}
			if (GUI.Button (new Rect (335, 310, 50, 50), ""+m_plyUpgrades.m_armor, style)) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_armor) {
					m_playerStats.m_parts -= m_plyUpgrades.m_armor;
					m_plyUpgrades.m_armor++;
					m_playerStats.m_armor += 2;
				}
			}
			if (GUI.Button (new Rect (335, 390, 50, 50), ""+m_plyUpgrades.m_speed, style)) {
				if (m_playerStats.m_parts >= m_plyUpgrades.m_speed) {
					m_playerStats.m_parts -= m_plyUpgrades.m_speed;
					m_plyUpgrades.m_speed++;
					m_playerStats.m_speed += 0.2f;
				}
			}
			style.fontSize = 24;
			style.alignment = TextAnchor.MiddleLeft;
			GUI.Label (new Rect (400, 225, 600, 50), "Current Health: "+((int)m_playerStats.m_health).ToString() + " + 25", style);
			GUI.Label (new Rect (400, 310, 600, 50), "Current Armor: "+((int)m_playerStats.m_armor).ToString() + " + 2", style);
			GUI.Label (new Rect (400, 385, 600, 50), "Current Speed: "+ m_playerStats.m_speed.ToString("F2") + " + 0.2", style);

			GUI.Label (new Rect (400, 470, 400, 50), "Current Parts: "+ ((int)m_playerStats.m_parts).ToString(), style);
			style.alignment = TextAnchor.MiddleCenter;

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
