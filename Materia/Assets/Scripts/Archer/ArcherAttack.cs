using UnityEngine;
using System.Collections;

public class ArcherAttack : MonoBehaviour 
{
	private UnifiedSuperClass god;
	public GameObject bow;

	private Animator anim;
	private bool hooked;

	private int currentSkill;

	public GameObject ArrowProjectile;

	public float power = 0.0F;
	private Vector3 mousePos;

	private float downTime; //internal time from when the key is pressed
	private bool buttonDown = false;

	private Vector3 newWorldPos;

	private bool ArcherSkill1OnCooldown;
	public float ArcherSkill1Cooldown;
	private float ArrowWait;

	// Use this for initialization
	void Awake()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass>();
	}
	void Start()
	{
		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetInteger ("Skill", 1);
		currentSkill = anim.GetInteger ("Skill");
		hooked = false;
		anim.SetBool ("Holding", false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hooked)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				hooked = false;
				transform.parent.FindChild ("HookShotRope").GetComponent<HookShot>().removeRope ();
				transform.parent.GetComponent<HingeJoint2D>().enabled = false;
				transform.parent.FindChild("HookShotRope").gameObject.SetActive(false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha1))
			anim.SetInteger ("Skill", 1);

		if (Input.GetKeyDown (KeyCode.Alpha2))
			anim.SetInteger ("Skill", 2);

		if (Input.GetKeyDown (KeyCode.Alpha3))
			anim.SetInteger ("Skill", 3);

		if (Input.GetKeyDown (KeyCode.Alpha4))
			anim.SetInteger ("Skill", 4);

		if (Input.GetKeyDown (KeyCode.Alpha5))
			anim.SetInteger ("Skill", 5);

		if (Input.GetMouseButtonDown (0))
		{
			anim.SetBool("Holding", true);
			anim.SetTrigger("Attacking");
		}
		//guys test code to make raycast line
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 60;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayCastHit;
		if (Physics.Raycast(ray, out rayCastHit, 1000f)) //
		{
			newWorldPos = AdjustMousePositionInWorld(rayCastHit.point);
			//Debug.Log(newWorldPos);
		}//end if
		Vector3 rayDirection = (newWorldPos - transform.position) * 20;
		Debug.DrawRay(transform.position, rayDirection, Color.green);
		
		Debug.DrawLine(transform.position, newWorldPos, Color.red);
		//end guys test code
		
		//my code
		//Calculate Angle for shooting
		mousePos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - mousePos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		
		// If left mouse button is pressed or held down
		if (Input.GetKey ("mouse 0"))
		{
			ButtonDown();
		}//end if
		// If left mouse button is released
		if (Input.GetButtonUp("Fire1") && buttonDown == true)
		{
			ButtonUp();
		}//end if


	}

	private void ButtonDown()
	{
		anim.SetBool ("Holding", true);
		buttonDown = true;
		if (power <= 100) {
			power += Time.deltaTime * 70;
		}//end if
	}
	
	private void ButtonUp()
	{
		if (currentSkill == 1 && ArcherSkill1OnCooldown == false) 
		{
			bow.animation.Play("Take 001");
			anim.SetBool("Preparing", false);
			anim.SetBool("Holding", false);
			ArcherSkill1OnCooldown = true;
			GameObject crateClone = Instantiate (ArrowProjectile, transform.position, transform.rotation) as GameObject;
			crateClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
			Debug.Log (power);
			Destroy(crateClone, 5); 
			StartCoroutine(simulateArrowCooldown());
		}//end if Fireball logic
		
		power = 0;
		buttonDown = false;	
	}
	
	private Vector3 AdjustMousePositionInWorld(Vector3 hitPoint)
	{
		float z_CameraPlayerDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 newMousePositionInWorld = hitPoint - Camera.main.transform.position;
		newMousePositionInWorld /= newMousePositionInWorld.z;
		newMousePositionInWorld *= z_CameraPlayerDistance;
		return Camera.main.transform.position + newMousePositionInWorld;
	}
	
	private IEnumerator simulateArrowCooldown(){
		ArrowWait = ArcherSkill1Cooldown;
		for (var x = 1; x < ArcherSkill1Cooldown; x++) {
			ArrowWait--;
			yield return new WaitForSeconds(1);
		}//end for
		ArcherSkill1OnCooldown = false;
		ArrowWait = 0;
	}



}
