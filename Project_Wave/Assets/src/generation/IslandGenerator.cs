using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandGenerator : MonoBehaviour {

	public int x;
	public int minIslands;
	public int maxIslands;
	private List<GameObject> SceneObjects = new List<GameObject>();
	public List<GameObject> Objects;

	void Awake()
	{
		int rnd = Random.Range (minIslands, maxIslands);
		for (int i = 0; i < rnd; i++) {
			SceneObjects.Add( Objects [Random.Range(0, Objects.Count)] );
		}

		// generete number of islands
		//int size = (int)(Random.value * (float)(this.maxIslands - this.minIslands)) + this.minIslands;
		// iterate throught each island
		for (int i = 0; i < SceneObjects.Count; i++)
		{
			// generate random positions
			float tx = Random.value * 50 + x;
			float ty = Random.value * 10 - 5;
			// select model to instantiate
			//int index = (int)Random.Range(0, islands.Count - 1);
			//int index = 0;
			// create new island object
			GameObject obj = Instantiate(SceneObjects[i], new Vector3(tx, ty, 0), new Quaternion(), this.transform);
		}
	}

}
