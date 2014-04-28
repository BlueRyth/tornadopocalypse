using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region Public Fields

	// Set in Editor
	public KeyCode LeftKey;
	public KeyCode RightKey;
	public KeyCode JumpKey;
	public KeyCode PowerUpKey;

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
		// Always translate to the right
		transform.Translate(Vector3.right);

		// Handle Input
		InputHandler();
	}

	#endregion

	#region Private Methods

	private void InputHandler()
	{
		if (Input.GetKeyDown(LeftKey))
		{
			//transform.Translate
		}
		else if (Input.GetKeyDown(RightKey))
		{

		}
		
		if (Input.GetKeyDown(JumpKey))
		{
		}
		
		if (Input.GetKeyDown(PowerUpKey))
	    {
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
