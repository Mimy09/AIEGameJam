using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class WaveAI : MonoBehaviour {

	// Public variables
	public float speed = 0.001f;
	public GameObject player;
	// Private variables
	private float initSpeed;
	private Vector3 initPosition;
	private Transform playerTransform;
	private PlayerMovment playerScript;

	void Awake()
	{
		// get player components
		this.initSpeed = this.speed;
		this.initPosition = this.transform.position;
		this.playerTransform = player.transform;
		this.playerScript = player.GetComponent<PlayerMovment>() as PlayerMovment;
	}

	void FixedUpdate () {
		GAME_STATE stage = GameStateManager.GetState ();
		switch (stage)
		{
			case GAME_STATE.IslandGUIState:
				speed += 0.0001f;
				transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
				break;
			case GAME_STATE.GameRunningState:
				speed += 0.0001f;
				MoveTsunami ();
				break;
		}
	}

	void MoveTsunami()
	{
		Vector3 playerDir = this.playerTransform.right * this.playerScript.speed;
		playerDir.y = 0;
		playerDir.x -= speed;
		transform.position -= playerDir * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print (collider.tag);

		if (collider.tag == "Player")
		{
			SceneManager.LoadScene ("Test");
		}
	}
}
