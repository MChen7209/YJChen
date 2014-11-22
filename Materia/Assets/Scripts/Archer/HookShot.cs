	using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour 
{
	private GameObject attachedGameObject;
	private GameObject parentObject;
	private Animator anim;
	private bool currentDirectionIsRight;
	//Either auto attack on distance, or aim.
	// Use this for initialization
	void Start () 
	{
		parentObject = transform.parent.gameObject;
		anim = transform.parent.gameObject.GetComponent<Animator> ();
		currentDirectionIsRight = anim.GetBool ("facingRight");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if( anim.GetBool("facingRight") && !currentDirectionIsRight)
		{
			//			Vector3 theScale = transform.localScale;
			//			theScale.x *= -1;
			//			transform.localScale = theScale;
			transform.rotation = new Quaternion (0, 0, 0, 0);
			currentDirectionIsRight = !currentDirectionIsRight;
		}
		else if( !anim.GetBool ("facingRight") && currentDirectionIsRight)
		{
			//			Vector3 theScale = transform.localScale;
			//			theScale.x *= -1;
			//			transform.localScale = theScale;
			Debug.Log("I have change direction");
			transform.rotation = new Quaternion(0, 0, 0, 0);
			currentDirectionIsRight = !currentDirectionIsRight;
		}
	}

//	public void OnTriggerStay2D(Collider2D target)
//	{
//		transform.hingeJoint.connectedBody = target;
//	}
	public void setupRope(GameObject target)
	{
		//change the legnth to fit.
		//Connect it to the current RigidBody2D that is "Hookshottable" tagged
		//Pull the distance from the code which OnTriggerStay2D from the attack code.
		attachedGameObject = target;
//		parentObject.GetComponent<HingeJoint2D> ().enabled = true;
//		gameObject.GetComponent<HingeJoint2D> ().enabled = true;
//		transform.rigidbody2D.hingeJoint.connectedBody = attachedGameObject.rigidbody2D;
		Vector3 checkScale = transform.localScale;

		//Check rotation
		//Don't need to check rotation just constrain the movement to if our character is facing right.

		gameObject.GetComponent<HingeJoint2D>().connectedBody = attachedGameObject.rigidbody2D;
	}

	public void removeRope()
	{
		GetComponent<HingeJoint2D> ().connectedBody = null;
		gameObject.SetActive (false);
	}
}
