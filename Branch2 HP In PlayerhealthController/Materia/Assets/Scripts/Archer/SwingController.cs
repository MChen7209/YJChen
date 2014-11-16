using UnityEngine;
using System.Collections;

public class SwingController : MonoBehaviour
{
	public GameObject anchor;
	private HingeJoint2D joint;
	private HingeJoint2D anchorJoint;
	private LineRenderer lr;
	private bool hooked;
	private Animator anim;
	private GameObject anchorObject;
	private bool pulledHook;
	private float maxRopeLimit;
	// Use this for initialization
	void Start ()
	{
		maxRopeLimit = 14.142f;
		lr = this.GetComponent<LineRenderer> ();
		anim = this.GetComponent<Animator>();
		hooked = false;
		pulledHook = false;

	}
	
	// Use this for initialization
	void Update ()
	{
		anchorObject = GameObject.FindGameObjectWithTag ("Anchor").gameObject;
		if (hooked)
		{
			if(pulledHook && Input.GetButton ("Jump"))
			{
				Destroy (joint);
				Destroy (anchorJoint);
				//Hide line
				lr.enabled = false;
				hooked = false;
				anim.SetBool ("Hooked", hooked);
				pulledHook = false;
				transform.rigidbody2D.velocity = new Vector2 ( transform.position.x + 10, transform.position.y +10);
			}
			if(Input.GetMouseButtonDown(0))
			{
				anchor.GetComponent<HingeJoint2D>().anchor = Vector3.zero;
				pulledHook = true;
			}

			if(Input.GetKey (KeyCode.W))
			{
				Debug.Log ("Should be going up hookshot");
				Vector2 tempAnchor = anchor.GetComponent<HingeJoint2D>().anchor;

				if(tempAnchor.x < 1 && tempAnchor.x > -1 && tempAnchor.y < 1 && tempAnchor.y > -1)
					tempAnchor = new Vector2 (0,0);
				else
				{
					if(tempAnchor.x > 0)
						tempAnchor.x = tempAnchor.x-0.5f;
					if(tempAnchor.x < 0)
						tempAnchor.x = tempAnchor.x+0.5f;
					if(tempAnchor.y > 0)
						tempAnchor.y = tempAnchor.y-0.5f;
					if(tempAnchor.y < 0)
						tempAnchor.y = tempAnchor.y+0.5f;
				}
				anchor.GetComponent<HingeJoint2D>().anchor = new Vector2 (tempAnchor.x, tempAnchor.y);
			}
			else if(Input.GetKey (KeyCode.S))
			{
				Debug.Log ("Should be going down hookshot");
				Vector2 tempAnchor = anchor.GetComponent<HingeJoint2D>().anchor;
					//This needs to be modified to max length of the hook shot.
//					if(tempAnchor.x > 0)
//						tempAnchor.x = tempAnchor.x-0.5f;
					if(tempAnchor.x < maxRopeLimit)
						tempAnchor.x = tempAnchor.x+0.5f;
//					if(tempAnchor.y > 0)
//						tempAnchor.y = tempAnchor.y-0.5f;
					if(tempAnchor.y < maxRopeLimit)
						tempAnchor.y = tempAnchor.y+0.5f;

				anchor.GetComponent<HingeJoint2D>().anchor = new Vector2 (tempAnchor.x, tempAnchor.y);
			}

			lr.SetColors(Color.black,Color.black);
			lr.SetPosition(0,transform.position);
			lr.SetPosition(1,anchor.GetComponent<HingeJoint2D>().connectedBody.position);
			lr.enabled = true;
		}
		else
		{
			anchorObject.transform.position = transform.transform.position;
			anchor.transform.rotation = Quaternion.identity;
		}
		
		//On left Click
		if (Input.GetMouseButtonDown (1) && anim.GetInteger("Skill") == 1) 
		{
			//Get the Clicked Position
			Vector3 clickedPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//We don't want that Z pos because this is in 2D
			clickedPosition.z = 0;
			//Shoot a ray out towards that position
			Vector2 mousePosHS = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 direction = mousePosHS - (Vector2)transform.position;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, direction, 20f, 1<<12);
			if (hit.collider != null) 
			{
				//				Vector3 currentLocation = transform.position;
				//				Quaternion currentRotation = transform.rotation;
				
				//move the anchor to the correct position
				//anchor.transform.position = anchoredPosition.position; //new Vector3 (hit.collider.transform, hit.point.y, 0);
				//anchor.transform.position = hit.collider.transform.position;
//				Debug.Log ("Anchor Hit x: " + hit.point.x + " Hit y: " +hit.point.y );
				//zero out any rotation
				anchor.transform.rotation = Quaternion.identity;
//				Debug.Log("Anchor positions vs Actual");
				//Create HingeJoints
				joint = gameObject.AddComponent<HingeJoint2D> ();
				joint.anchor = Vector3.zero;
				//				transform.GetComponent<HingeJoint2D>().connectedBody = anchor.rigidbody2D;
				//joint.connectedBody = anchor.rigidbody2D;
				//joint.connectedAnchor = anchor.transform.position;
				
//				Debug.Log ("Anchor distance  | x: " + (hit.collider.transform.position.x - transform.position.x) + " y: " + (hit.collider.transform.position.y - transform.position.y));
//				Debug.Log ("Player transform | x: " + transform.position.x + " y: " + transform.position.y);
//				Debug.Log ("Anchor Location  | x: " + anchor.transform.position.x + " y: " + anchor.transform.position.y);
//				Debug.Log ("Object Anchored  | x: " + hit.collider.transform.position.x + " y: " + hit.collider.transform.position.y);
				joint.connectedBody = anchor.rigidbody2D;
				anchor.transform.rotation = Quaternion.identity;//--!
				anchorJoint = anchor.AddComponent<HingeJoint2D> ();
				//				anchor.transform.position = transform.position;
				anchorJoint.connectedBody = hit.collider.gameObject.rigidbody2D;
				anchor.transform.rotation = Quaternion.identity; //--!
				anchorJoint.anchor = new Vector2((hit.collider.transform.position.x - transform.position.x), (hit.collider.transform.position.y - transform.position.y));
//				Debug.Log ("After Connected  | x: " + anchorJoint.anchor.x + " y: " + anchorJoint.anchor.y);
//				Debug.Log ("After Char Pos   | x: " + transform.position);
//				Debug.Log ("Hit: " + hit.collider.gameObject.rigidbody2D);
//				//anchorJoint.connectedBody = anchor.rigidbody2D;//hit.collider.gameObject.rigidbody2D;
//				Debug.Log(anchorJoint.transform.tag);
				//show line
				hooked = true;
				//				transform.position = currentLocation;
				//				transform.rotation = currentRotation;
				anim.SetBool ("Hooked", hooked);
			}
			//On left Click release
		} 
		else if (Input.GetMouseButtonUp (1)) 
		{
			//Destroy HingeJoints
			Destroy (joint);
			Destroy (anchorJoint);
			
			//Hide line
			lr.enabled = false;
			hooked = false;
			anim.SetBool ("Hooked", hooked);
		}
	}
}



