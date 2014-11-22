using UnityEngine;
using System.Collections;

public class ArcherController : MonoBehaviour 
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

	bool doubleJump = false;	//Used up double jump.

	private Transform charLocation;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Grounded", grounded);

		if (grounded)
			doubleJump = false;

		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		charLocation = transform;
		Camera myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		Ray MouseLikeDoomSword = myCam.ScreenPointToRay (Input.mousePosition);

		Debug.Log ("Pos: " + MouseLikeDoomSword.origin.x);
		Debug.Log ("Char: " + charLocation.position.x);

		if ((MouseLikeDoomSword.origin.x > charLocation.position.x) && !facingRight)
			FlipDirection ();
		if ((MouseLikeDoomSword.origin.x < charLocation.position.x) && facingRight)
			FlipDirection ();
		
	}

	void Update()
	{
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump"))
		{
			anim.SetBool ("Grounded", false);
			rigidbody2D.AddForce ( new Vector2(0, jumpForce));

			if(!doubleJump && !grounded)
				doubleJump = true;	//Used up double jump.
		}
	}

	void FlipDirection()
	{
		facingRight = !facingRight;
		Vector3 charScale = transform.localScale;
		charScale.x *= -1;
		transform.localScale = charScale;
	}
}
