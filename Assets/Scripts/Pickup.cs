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
		Debug.Log(collider.gameObject.tag);
		
		if (collider.gameObject.tag == globals.tag_Player)
		{
			GameObject.Destroy(gameObject);
		}
	}
}
