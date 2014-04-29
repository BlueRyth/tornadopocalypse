using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region Internal Declaration

	enum PlayerJumpState
	{
		Grounded,
		Jumping,
		Falling
	}

	#endregion

	#region Public Fields

	// Set in Editor
	public KeyCode LeftKey;
	public KeyCode RightKey;
	public KeyCode JumpKey;
	public KeyCode PowerUpKey;
	public Global globals;
	public CharacterController controller;

	#endregion

	#region Public Properties

	public bool IsStunned { get; private set; }
	public bool IsDying   { get; private set; }

	#endregion

	#region Monobehavior

	// Use this for initialization
	void Start () 
	{
		JumpState = PlayerJumpState.Grounded;
		IsStunned = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Only allow movement if not stunned
		if (!IsStunned)
			controller.Move(MovementHandler());

		// Button for interactions / powerups
		if (Input.GetKeyDown(PowerUpKey))
		{
			Debug.Log ("POW");
		}
	}

	#endregion

	private const float minYPos = 0f;

	private PlayerJumpState JumpState;
	private float currentJumpHeight;
	private float stunTimer;

	#region Private Methods

	private Vector3 MovementHandler()
	{
		Vector3 movement = Vector3.zero;
		
		if (Input.GetKey(LeftKey))
		{
			movement += globals.LeftTranslate;
		}
		else if (Input.GetKey(RightKey))
		{
			movement += globals.RightTranslate;
		}

		movement += JumpHandler();
		movement += new Vector3(0f, globals.gravity, 0f);

		return movement;
	}

	private Vector3 JumpHandler()
	{
		if (JumpState == PlayerJumpState.Grounded)
		{
			// Start Jump if Grounded and Jump Key Down
			if (Input.GetKeyDown(JumpKey))
			{
				JumpState = PlayerJumpState.Jumping;
			}
		}
		else if (JumpState == PlayerJumpState.Jumping)
		{
			// Jump till key released or max jump reached
			if (Input.GetKey (JumpKey))
		    {
				float deltaJump = globals.JumpDelta * Time.deltaTime;
				float testHeight = currentJumpHeight + deltaJump;
				if (testHeight >= globals.MaxJumpHeight)
				{
					deltaJump = globals.MaxJumpHeight - currentJumpHeight;
					currentJumpHeight = 0f;
					JumpState = PlayerJumpState.Falling;
				}
				return new Vector3(0f, deltaJump, 0f);
			}
			else
			{
				currentJumpHeight = 0f;
				JumpState = PlayerJumpState.Falling;
			}
		}

		return Vector3.zero;
	}
	
	private void StunHandler()
	{
		if (IsStunned)
		{
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0f)
			{
				IsStunned = false;
				stunTimer = 0f;
			}
		}
	}
	
	private void Death()
	{
		Debug.Log ("THANKS OBAMA");
		GameObject.Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider collision)
	{
		// Death!
		if (collision.gameObject.tag == globals.tag_KillPlane)
			OnKillPlaneCollision();
		
		if (collision.gameObject.tag == globals.tag_Environment)
			OnObstacleCollision();
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.tag == globals.tag_Ground)
		{
			JumpState = PlayerJumpState.Grounded;
		}
	}

	private void OnKillPlaneCollision()
	{
		// Killy things
		Death ();
	}
	private void OnObstacleCollision()
	{
		// Hit by Obstacle
		IsStunned = true;
		JumpState = PlayerJumpState.Falling;
		stunTimer = globals.stunLength;
	}

	#endregion
}
