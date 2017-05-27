using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandComponent : MonoBehaviour {
	void Update () {
		if (this.transform.position.x + this.GetComponentInParent<Transform>().position.x < -60) {

			WorldGenerator worldGen = this.GetComponentInParent<WorldGenerator> () as WorldGenerator;
			worldGen.DeleteChunk (this.gameObject);
			Destroy (this.gameObject);
		}
	}
}
