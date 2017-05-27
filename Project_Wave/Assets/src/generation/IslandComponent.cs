using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandComponent : MonoBehaviour {


	void Update () {
		if (this.transform.position.x < -50) {
			Destroy (this.gameObject);
		}
	}
}
