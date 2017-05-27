// Unity Requirements
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour {

	// Variables
	public int chunkSize = 5;
	public int minObjects = 3;
	public int maxObjects = 5;
	public Vector3 chunkOffset = new Vector3(0, 0, 0);
	public GameObject chunkPrefab = null;
	public List<GameObject> chunkObjects = new List<GameObject>();
	public List<WorldObject> worldObjects = new List<WorldObject>();

	void Load()
	{
	}

	void Awake()
	{
		if (this.chunkPrefab == null)
			throw new System.Exception ("WorldGenerator :: chunk prefab is empty!");
		if (this.worldObjects.Count == 0)
			throw new System.Exception ("WorldGenerator :: object prefab is empty!");

		GenerateWorld ();
	}

	void DeleteWorld()
	{
	}

	void DeleteChunk(GameObject obj)
	{
		chunkObjects.Remove (obj);
		GenerateChunk ();
	}

	void GenerateChunk()
	{
		// create chunk object
		int chunkIndex = this.chunkObjects.Count;
		// calculate chunk position
		Vector3 offset = chunkOffset;
		if (chunkIndex > 0) offset.x = this.chunkObjects [chunkIndex - 1].transform.position.x + 3;
		this.chunkObjects.Add(GameObject.Instantiate(this.chunkPrefab, offset, new Quaternion(), this.transform));
		// generate the objects insde the chunk
		GenerateObjects(this.chunkObjects[chunkIndex]);
	}

	void GenerateObjects(GameObject chunk)
	{
		
	}

	void GenerateWorld()
	{
		for (int i = 0; i < this.chunkSize; i++)
			GenerateChunk ();
	}
}
