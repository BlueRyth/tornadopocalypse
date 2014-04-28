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
		
	}

	#endregion

	#region Private Methods

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
