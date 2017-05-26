using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandGenerator : MonoBehaviour {

	public int minIslands = 3;
	public int maxIslands = 5;
	public List<GameObject> islands;

	void Awake()
	{
		// generete number of islands
		int size = (int)(Random.value * (float)(this.maxIslands - this.minIslands)) + this.minIslands;
		// iterate throught each island
		for (int i = 0; i < size; i++)
		{
			// generate random positions
			float tx = (Random.value - 0.5f) * 10;
			float ty = (Random.value - 0.5f) * 10;
			// select model to instantiate
			//int index = (int)Random.Range(0, islands.Count - 1);
			int index = 0;
			// create new island object
			GameObject obj = Instantiate(islands[index], new Vector3(tx, ty, 0), new Quaternion(), this.transform);
		}	
	}

}
