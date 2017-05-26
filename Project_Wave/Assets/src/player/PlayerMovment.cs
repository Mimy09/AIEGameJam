using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {
	public GameObject Scene;

	public Vector3 vec_Scene;
	public Quaternion m_rotation;

	public float speed = 1;

	void Awake()
	{
		m_rotation = transform.rotation;
	}

	void Update () {
		


		//this.transform.rotation = m_rotation;

		//Vector3 dir = -transform.forward;
		//vec_Scene = Scene.transform.position + new Vector3(dir.x, 0, 0);
		//Scene.transform.position = vec_Scene;

		transform.position += new Vector3 (0, transform.right.y * Time.deltaTime * 0.5f * speed, 0);
		Scene.transform.Translate (new Vector3 (-transform.right.x, 0, 0) * Time.deltaTime * 0.5f * speed);


		if (Input.GetKey (KeyCode.A))
			transform.Rotate (Vector3.forward, Time.deltaTime * 50);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (-Vector3.forward, Time.deltaTime * 50);



		if (Input.GetKey (KeyCode.W))
			speed = Mathf.Lerp (speed, 2, Time.deltaTime / 3);
		else if (Input.GetKey (KeyCode.S))
			speed = Mathf.Lerp (speed, 0.5f, Time.deltaTime / 3);
		else
			speed = Mathf.Lerp (speed, 1, Time.deltaTime / 2);
	}
}
