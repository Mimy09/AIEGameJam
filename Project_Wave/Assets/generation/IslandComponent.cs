using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameVisible()
	{
		
	}
	void OnBecameInvisible()
	{
		Destroy (this.gameObject);
	}

}
