using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldObject {
	// Prefab of the object
	public GameObject prefab;
	// Priority of the prefab
	public Priority priority;
	public int min;
	public int max;
	public float food;
	// Object Priority enum class
	public enum Priority { Low, Medium, High }; 
}