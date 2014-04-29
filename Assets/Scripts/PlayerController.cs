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

    enum PlayerLaneState
    {
        Default,
        One,
        UpShift,
        Two,
        DownShift
    }

	#endregion

	#region Public Fields

	// Set in Editor
    public KeyCode UpKey;
    public KeyCode DownKey;
	public KeyCode LeftKey; 
	public KeyCode RightKey;
	public KeyCode JumpKey;
	public KeyCode PowerUpKey;
	public Global globals;
	public float PlayerSpeed = 10f;
    public float ShiftSpeed = 1f;

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
        LaneState = PlayerLaneState.One;
		IsStunned = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Only allow movement if not stunned
		if (!IsStunned)
			transform.Translate(MovementHandler());
		else
			StunHandler();

		// Button for interactions / powerups
		if (Input.GetKeyDown(PowerUpKey))
		{
			Debug.Log ("POW");
		}
	}

	#endregion

	private const float minYPos = 0f;

	private PlayerJumpState JumpState;
    private PlayerLaneState LaneState;
	private float currentJumpHeight;
	private float stunTimer;
	private float jumpTimer;

	#region Private Methods

	private Vector3 MovementHandler()
	{
		Vector3 movement = Vector3.zero;
		
		if (Input.GetKey(LeftKey))
		{
			movement -= new Vector3(PlayerSpeed * Time.deltaTime, 0f, 0f);
		}
		else if (Input.GetKey(RightKey))
		{
			movement += new Vector3(PlayerSpeed * Time.deltaTime, 0f, 0f);
		}

        movement += LaneHandler();

		movement += JumpHandler();

		return movement;
	}

    private Vector3 LaneHandler()
    {
        if (Input.GetKey(UpKey) && (LaneState == PlayerLaneState.One || LaneState == PlayerLaneState.UpShift))
        {
            LaneState = PlayerLaneState.UpShift;
        }
        if (Input.GetKey(DownKey) && (LaneState == PlayerLaneState.Two || LaneState == PlayerLaneState.DownShift))
        {
            LaneState = PlayerLaneState.DownShift;
        }

        if (LaneState == PlayerLaneState.UpShift && transform.position.z >= 1.0f)
        {
            LaneState = PlayerLaneState.Two;
        }
        if (LaneState == PlayerLaneState.DownShift && transform.position.z <= 0.0f)
        {
            LaneState = PlayerLaneState.One;
        }

        Vector3 move = Vector3.zero;
        if(LaneState == PlayerLaneState.DownShift)
        {
            move.z -= ShiftSpeed * Time.deltaTime;
        }
        else if (LaneState == PlayerLaneState.UpShift)
        {
            move.z += ShiftSpeed * Time.deltaTime;
        }
        return move;
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
				else
				{
					currentJumpHeight += deltaJump;
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
				renderer.material.color = Color.white;
				stunTimer = 0f;
			}
		}
	}
	
	private void Death()
	{
		Debug.Log ("Thanks Obama");
		GameObject.Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider collision)
	{
		// Death!
		if (collision.gameObject.tag == globals.tag_KillPlane)
			OnKillPlaneCollision();
		
		if (collision.gameObject.tag == globals.tag_Obstacle)
			OnObstacleCollision();
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == globals.tag_Ground)
		{
			currentJumpHeight = 0f;
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
		renderer.material.color = Color.red;
	}

	#endregion
}
