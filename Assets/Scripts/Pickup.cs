using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public Global globals;

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{		
		if (collider.gameObject.tag == globals.tag_Player)
		{
			GameObject.Destroy(gameObject);
		}
	}
}
