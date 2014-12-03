using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	[HideInInspector]
	bool facingRight = true;

	[SerializeField]
	public float maxSpeed;				// The fastest the player can travel in the x axis.

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce;
	private float climbSpeed = 30f;

	bool doubleJump = false;	//Used up double jump.

	private Transform charLocation;

	private bool isClimbing;
	private bool jumpLock;

	private bool isArcher;

	void Awake()
	{
		anim = GetComponent<Animator> ();

		isClimbing = false;
		jumpLock = false;

		charLocation = transform;
		Camera myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		
		Ray MouseLikeDoomSword = myCam.ScreenPointToRay (Input.mousePosition);

		if ((MouseLikeDoomSword.origin.x > charLocation.position.x) && !facingRight)
			FlipDirection ();
		if ((MouseLikeDoomSword.origin.x < charLocation.position.x) && facingRight)
			FlipDirection ();
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Grounded", grounded);
		anim.SetBool("Walking", false);
		if (grounded)
			doubleJump = false;

		if(anim.GetBool("skillLock"))
			return;

		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
		
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));

		if(anim.GetBool("Hooked"))
		{
			rigidbody2D.velocity = new Vector2 (move * (maxSpeed * 2), rigidbody2D.velocity.y);
		}
		else
		{
			if (move < 0 && facingRight)
			{
				anim.SetBool("Walking", true);
				rigidbody2D.velocity = new Vector2 (move * maxSpeed / 2, rigidbody2D.velocity.y);
			}
			else if(move > 0 && !facingRight)
			{
				anim.SetBool("Walking", true);
				rigidbody2D.velocity = new Vector2 (move * maxSpeed/2, rigidbody2D.velocity.y);
			}
			else
			{
				if(anim.GetBool ("Casting"))
				{
					if( move < 0 && facingRight)
						anim.SetBool("Walking", true);
					else if (move > 0 && !facingRight)
						anim.SetBool("Walking", true);

					rigidbody2D.velocity = new Vector2 (move * maxSpeed/2, rigidbody2D.velocity.y);
				}
				else
				{
					rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
				}
			}
		}
		
		charLocation = transform;
		Camera myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		
		Ray MouseLikeDoomSword = myCam.ScreenPointToRay (Input.mousePosition);
		
		//Debug.Log ("Pos: " + MouseLikeDoomSword.origin.x);
		//Debug.Log ("Char: " + charLocation.position.x);
		
		if ((MouseLikeDoomSword.origin.x > charLocation.position.x) && !facingRight)
			FlipDirection ();
		if ((MouseLikeDoomSword.origin.x < charLocation.position.x) && facingRight)
			FlipDirection ();
	}

	void Update()
	{
		if(anim.GetBool ("skillLock"))
			return;

		if (Input.GetKey (KeyCode.D) && Input.GetButtonDown ("Jump") && isClimbing && jumpLock)
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce ( new Vector2(0, jumpForce));
		}
		
		if (Input.GetKey (KeyCode.A) && Input.GetButtonDown ("Jump") && isClimbing && jumpLock)
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce ( new Vector2(0, jumpForce));
		}

		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump"))
		{
			anim.SetBool ("Grounded", false);
			rigidbody2D.AddForce ( new Vector2(0, jumpForce));

			if(!doubleJump && !grounded)
				doubleJump = true;	//Used up double jump.
		}

		if(Input.GetKey(KeyCode.W) && jumpLock)
		{
			if (isClimbing)
			{
				rigidbody2D.isKinematic = true;
				anim.SetBool("isClimbing", true);
				
				transform.Translate(0,climbSpeed*Time.deltaTime,0);
			}//end if climbing
		}//end if climbing up
		
		if (Input.GetKey (KeyCode.S) && jumpLock)
		{
			if (isClimbing)
			{
				rigidbody2D.isKinematic = true;
				anim.SetBool("isClimbing", true);
				
				transform.Translate(0,-climbSpeed*Time.deltaTime,0);
			}//end if climbing
		}//end if climbing down
	}

	void FlipDirection()
	{
		facingRight = !facingRight;
		anim.SetBool ("facingRight", facingRight);

		transform.Rotate (0, 180, 0);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if (col.gameObject.CompareTag ("Ladder"))
		{
			jumpLock = true;
			isClimbing = true;
		}//end if
		
	}

	void OnTriggerExit2D(Collider2D col)
	{
		anim.SetBool ("skillLock", false);
		if (col.gameObject.CompareTag ("Ladder")){	
			rigidbody2D.isKinematic = false;
			
			isClimbing = false;
			jumpLock = false;
		}//end if
	}
}
