﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region Public Fields

	// Set in Editor
	public KeyCode LeftKey;
	public KeyCode RightKey;
	public KeyCode JumpKey;
	public KeyCode PowerUpKey;
	public Global globals;

	#endregion

	#region Public Properties

	public bool IsJumping { get; private set; }
	public bool IsStunned { get; private set; }

	#endregion

	#region Monobehavior

	// Use this for initialization
	void Start () 
	{
		IsJumping = false;
		IsStunned = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

		// Handle Input
		InputHandler();
	}

	void FixedUpdate()
	{

		if (Input.GetKeyDown(JumpKey))
		{
			if (!IsJumping)
			{
				rigidbody.AddForce(Vector3.up * 500f);
				//IsJumping = true;
			}
		}
	}

	#endregion

	#region Private Methods

	private void InputHandler()
	{
		if (Input.GetKey(LeftKey))
		{
			transform.Translate(globals.LeftTranslate);
		}
		else if (Input.GetKey(RightKey))
		{
			transform.Translate(globals.RightTranslate);
		}
		

		
		if (Input.GetKeyDown(PowerUpKey))
	    {
			Debug.Log ("POW");
		}
	}

	private void Death()
	{
		// Deathy things
	}

	private void OnKillPlaneCollision()
	{
		// Killy things
	}

	#endregion
}
