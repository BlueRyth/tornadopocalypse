using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region Public Fields

	public KeyCode LeftKey;
	public KeyCode RightKey;
	public KeyCode JumpKey;
	public KeyCode PowerUpKey;

	#endregion

	#region Monobehavior

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		// Handle Input
		InputHandler();
	}

	#endregion

	#region Private Methods

	private void InputHandler()
	{
		if (Input.GetKeyDown(LeftKey))
		{

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
