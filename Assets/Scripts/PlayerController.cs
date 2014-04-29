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

	enum PlayerStunnedState
	{
		Normal,
		Stunned,
		Recovering
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
	public Renderer MyRenderer;
	public Collider MyCollider;

	public float PlayerSpeed = 10f;
    public float ShiftSpeed = 1f;

	#endregion

	#region Monobehavior

	// Use this for initialization
	void Start () 
	{

		MyRenderer = transform.GetComponentInChildren<Renderer>();
		MyCollider = transform.GetComponentInChildren<Collider>();

		// TODO: Change if the players don't start falling
		jumpState = PlayerJumpState.Falling;
		stunState = PlayerStunnedState.Normal;
		LaneState = PlayerLaneState.One;

		// TODO: Set animation
		MyRenderer.material.color = Color.green;

		animation.Play ("Run");
       
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Only allow movement if not stunned
		if (stunState != PlayerStunnedState.Stunned)
			transform.Translate(MovementHandler(), Space.World);
		else
			transform.Translate(globals.LeftTranslate, Space.World);

		// Update stun timer if we're stunned or recovering
		if (stunState != PlayerStunnedState.Normal)
			ContinueStun();

		// TODO: Button for interactions / powerups
		if (Input.GetKeyDown(PowerUpKey))
		{
			Debug.Log ("POW");
		}
	}

	#endregion

	#region Private Fields

	private const float DefaultPlayerStunTime = 1.0f;
	private const float DefaultPlayerRecoveryTime = 1.0f;
	
	private PlayerJumpState jumpState;
	private PlayerStunnedState stunState;
    private PlayerLaneState LaneState;

	private float currentJumpHeight;
	private float stunTimer;

	#endregion

	#region Private Methods

	// Called from Update
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
		if (jumpState == PlayerJumpState.Grounded)
		{
			// Start Jump if Grounded and Jump Key Down
			if (Input.GetKeyDown(JumpKey))
			{
				jumpState = PlayerJumpState.Jumping;
			}
		}
		else if (jumpState == PlayerJumpState.Jumping)
		{
			// Start falling if jump key released
			if (Input.GetKey (JumpKey))
		    {
				float deltaJump = globals.JumpDelta * Time.deltaTime;
				float testHeight = currentJumpHeight + deltaJump;

				// Start falling if max height
				if (testHeight >= globals.MaxJumpHeight)
				{
					deltaJump = globals.MaxJumpHeight - currentJumpHeight;
					currentJumpHeight = 0f;
					jumpState = PlayerJumpState.Falling;
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
				jumpState = PlayerJumpState.Falling;
			}
		}

		return Vector3.zero;
	}

	private void StartStun(float stunTime)
	{
		// Set stun state, stop jumping
		stunState = PlayerStunnedState.Stunned;
		jumpState = PlayerJumpState.Falling;
		
		// Set stun timer
		stunTimer = stunTime;
		
		// TODO: Set Stun Animation
		MyRenderer.material.color = Color.red;
	}
	
	private void ContinueStun()
	{
		if (stunState == PlayerStunnedState.Stunned)
		{
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0f)
			{
				stunState = PlayerStunnedState.Recovering;
				MyRenderer.material.color = Color.yellow;
				stunTimer = DefaultPlayerRecoveryTime;
			}
		}
		else if (stunState == PlayerStunnedState.Recovering)
		{
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0f)
			{
				stunState = PlayerStunnedState.Normal;
				MyRenderer.material.color = Color.green;
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
			jumpState = PlayerJumpState.Grounded;
		}
	}

	private void OnKillPlaneCollision()
	{
		// Killy things
		Death ();
	}
	private void OnObstacleCollision()
	{
		// Stun if hit by Obstacle and in normal stun state
		if (stunState == PlayerStunnedState.Normal)
		{
			// TODO: accept variable stun times from collision object
			StartStun(DefaultPlayerStunTime);
		}
	}


	
	#endregion
}
