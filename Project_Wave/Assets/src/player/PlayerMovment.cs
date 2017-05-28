using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {
	public GameObject Scene;

	private Vector3 vec_Scene;
	private Quaternion m_rotation;

	public float speed = 1;
	private float plySpeed;

	void Awake() {
		m_rotation = transform.rotation;
	}

	void Update () {
		if (GameStateManager.GetState () == GameState.GAME_STATE.GameRunningState) {
			plySpeed = this.GetComponent<Player> ().m_playerStats.m_speed;
			// Sets the players y pos
			transform.position += new Vector3 (0, transform.right.y * Time.deltaTime * 0.5f * speed * plySpeed, 0);
			// Sets the scenes x pos to move right to left on screen
			Scene.transform.Translate (new Vector3 (-transform.right.x, 0, 0) * Time.deltaTime * 0.5f * speed * plySpeed);

			// Turns the player left
			if (Input.GetKey (KeyCode.A))
				transform.Rotate (Vector3.forward, Time.deltaTime * 50);
			// Turns the player right
			if (Input.GetKey (KeyCode.D))
				transform.Rotate (-Vector3.forward, Time.deltaTime * 50);


			// Makes the player speed up to a value of 2x faster then default speed
			if (Input.GetKey (KeyCode.W)) {
				speed = Mathf.Lerp (speed, 2, Time.deltaTime / 3);
			} // Makes the player slow down to a value of 1.5x slower then default speed
			else if (Input.GetKey (KeyCode.S)) {
				speed = Mathf.Lerp (speed, 0.5f, Time.deltaTime / 3);
			} // Make the player return to default speed it no buttons are pressed
			else {
				speed = Mathf.Lerp (speed, 1, Time.deltaTime / 2);
			}

			Vector3 pos = transform.position;
			if (pos.y > 10.65f) pos.y = -10.3f;
			if (pos.y < -10.35f) pos.y = 10.6f;
			if (pos.y <-10.35f) pos.y =  10.6f;
			transform.position = pos;
		}
	}
}
