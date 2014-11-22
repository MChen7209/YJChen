using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour 
{
	//Administration
	private Animator anim;
	private bool secondSkillLock;
	private UnifiedSuperClass god;
	private SkillsController skillsController;


	private List<Skills> skills;
	private List<Skills> utility;

//	public GameObject fireBallProjectile;
//	private bool isFireBallCooldown;
//	public float fireBallCooldown;
//	private float fireBallWait;
//	public GameObject lightBall;
//	private GameObject tempLightBall;
//	private bool tempLightBallOn;
//
//	public GameObject lightningNodeProjectile;
//	private bool isLightningCooldown;
//	public float lightningCooldown;
//	private float lightningWait;
//	private bool linked;					//Kinesis
//	private GameObject kinesisConnection;
//	public GameObject kinesisConnector;
//
//	public GameObject IceBallProjectile;
//	public GameObject IceBlawk;
//	private GameObject IceRadius;
//	private GameObject iceBlawkOpaqueTarget;
//	private int iceBlockLimit;
//	private bool isIceBallCooldown;
//	public float iceBallCooldown;
//	private float iceBallWait;
//	public int iceBlawkDuration;
//	private IceBlawkOpaque iceBlawkLinkScript;
	
	public float power = 0.0F;
	private Vector3 mousePos;

	private float time;
	public GameObject progressBar;
	
	private bool buttonDown = false;
	
	//guys test code to make ray cast line
	private Vector3 newWorldPos;
	
	// Cooldowns
	private int currentSkill;
	private Renderer renderer;

	void Start()
	{
//		IceRadius = transform.parent.transform.FindChild("IceRadius").gameObject;
//		iceBlawkOpaqueTarget = IceRadius.transform.FindChild ("IceBlock").gameObject;
//		iceBlawkLinkScript = IceRadius.GetComponentInChildren<IceBlawkOpaque> ();
//		IceRadius.SetActive (false);
		secondSkillLock = false;
		time = 1;
//		progressBar.renderer.enabled = false;
//		tempLightBallOn = false;
	}
	// Use this for initialization
	void Awake () 
	{
		skills = new List<Skills>();
		utility = new List<Skills>();
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.getSkillsController();

		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetInteger ("Skill", 1);
		currentSkill = anim.GetInteger ("Skill");
//		loadSkills();
//		linked = false;
		//Might have to lock change characters when using skills
	}

	public void loadSkills(string skillName, string type)
	{
		if(type == "attack")
			skills.Add(skillsController.getSkill("FireBallSkill"));
		if(type == "utility")
		utility.Add(skillsController.getSkill ("FlameLampSkill"));
	}

	// Update is called once per frame
	void Update () 
	{
		currentSkill = anim.GetInteger ("Skill");

		if (Input.GetKeyDown (KeyCode.Alpha1))
			anim.SetInteger ("Skill", 1);

		if (Input.GetKeyDown (KeyCode.Alpha2))
			anim.SetInteger ("Skill", 2);

		if (Input.GetKeyDown (KeyCode.Alpha3))
			anim.SetInteger ("Skill", 3);

		if (Input.GetKeyDown (KeyCode.Alpha4))
			anim.SetInteger ("Skill", 4);

		if (anim.GetBool ("skillLock") == true)
			return;


//		if(Input.GetMouseButton(0) && !secondSkillLock)
//		{
////			anim.SetBool("Holding", true);
////			anim.SetTrigger("Attacking");
//			Debug.Log ("current skill: " + currentSkill);
//			Debug.Log ("Skills: " + skills.Count);
		if(!secondSkillLock && (skills.Count > 0))
			skills[currentSkill-1].skillActivate();
//		}

		if(utility.Count >0)
			utility[currentSkill-1].skillActivate();

//		if(Input.GetMouseButtonDown (1))
//		{
//			secondSkillLock = true;
			//utility[currentSkill-1].skillActivate ();
//		}
//		//guys test code to make raycast line
//		Vector3 mousePos = Input.mousePosition;
//		mousePos.z = 60;
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit rayCastHit;
//		if (Physics.Raycast (ray, out rayCastHit, 1000f)) //
//		{
//				newWorldPos = AdjustMousePositionInWorld (rayCastHit.point);
//		}//end if
//		Vector3 rayDirection = (newWorldPos - transform.position) * 20;
//		//end guys test code
//
//		//my code
//		//Calculate Angle for shooting
//		mousePos = Camera.main.WorldToScreenPoint (transform.position);
//		Vector3 dir = Input.mousePosition - mousePos;
//		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
//		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
//
//		// If left mouse button is pressed or held down
//		if (Input.GetKey ("mouse 0") && !secondSkillLock)
//		{
//				ButtonDown ();
//		}//end if
//		// If left mouse button is released
//		if (Input.GetButtonUp ("Fire1") == true && !secondSkillLock)
//		{
//				ButtonUp ();
//		}//end if
//
//		if (currentSkill == 1)
//		{
//			if (Input.GetButton ("Fire2"))
//			{
//				if(!tempLightBallOn)
//				{	
//					tempLightBall = Instantiate ( lightBall, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity) as GameObject;
//					tempLightBallOn = true;
//					secondSkillLock = true;
//				}
//				Debug.Log("Lightball Position: " + lightBall.transform.position);
//				Debug.Log("Wand      Position: " + transform.position);
//				tempLightBall.transform.position = (Vector2)transform.position;
//
//				if(Input.GetButtonDown ("Fire1"))
//				{
//					Debug.Log ("Throw Light");
//					ThrowLightBall((Vector3)transform.position, (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition));
//				}
//			}
//			if(Input.GetButtonUp ("Fire2"))
//			{
//				Destroy(tempLightBall);
//				tempLightBallOn = false;
//				secondSkillLock = false;
//			}	
//		}
//		if (currentSkill == 2)
//		{
//			if (Input.GetButton ("Fire2"))
//			{
//				Camera myCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
//				
//				Debug.Log("Linked before if Statement: " + linked);
//				if (linked)
//					kinesisConnection.transform.position = new Vector3 (myCam.ScreenToWorldPoint (Input.mousePosition).x, myCam.ScreenToWorldPoint (Input.mousePosition).y, myCam.ScreenToWorldPoint (Input.mousePosition).z + 10);
//				else
//				{
//					if (Physics2D.OverlapCircle (myCam.ScreenToWorldPoint (Input.mousePosition), 1f).gameObject.tag == "Kinesisable")
//					{
//							float testLocation = 10;
//							kinesisConnection = Physics2D.OverlapCircle (myCam.ScreenToWorldPoint (Input.mousePosition), 1f).gameObject;
//							GameObject kinesisConnectoring = Instantiate(kinesisConnector, Vector3.zero, Quaternion.identity) as GameObject;
//							kinesisConnectoring.transform.parent = kinesisConnection.transform;
//							kinesisConnectoring.transform.localPosition = Vector3.zero;
//							kinesisConnection.transform.position = myCam.ScreenToWorldPoint (Input.mousePosition);
//							kinesisConnection.transform.Translate (0, 0, 10);
//							linked = true;
//					}
//				}
//			}
//			if (Input.GetButtonUp ("Fire2"))
//			{
//					//May have a bug if person changes skills without releasing key up
//					Debug.Log ("linked: " + linked);
//					Destroy(kinesisConnection.transform.FindChild ("KinesisConnector(Clone)").gameObject);
//					linked = false;
//					Debug.Log (" changed into linked: " + linked);
//			}
//		}
//		if (currentSkill == 3)
//		{
//			if (Input.GetButton ("Fire2"))
//			{
//				IceRadius.SetActive (true);
//				secondSkillLock = true;
//
//				Camera myCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
//				Vector2 mousePosHS = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
//				Vector2 direction = mousePosHS - (Vector2)transform.parent.position;
//				RaycastHit2D hsHit = Physics2D.Raycast (transform.parent.position, direction, 100f, 1<<14);
//
//				if (hsHit && (Vector2.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), IceRadius.transform.position) > 17f))
//				{
//					iceBlawkOpaqueTarget.transform.position = hsHit.point;
//					if (Input.GetKeyDown ("mouse 0") && iceBlockLimit <= 3)
//					{
//						GameObject realIceBlawk = Instantiate (IceBlawk, iceBlawkOpaqueTarget.transform.position, iceBlawkOpaqueTarget.transform.rotation) as GameObject;
//						Destroy (realIceBlawk, iceBlawkDuration);
//					}
//				}
//				else
//				{
//					iceBlawkOpaqueTarget.transform.position = new Vector3 (myCam.ScreenToWorldPoint (Input.mousePosition).x, myCam.ScreenToWorldPoint (Input.mousePosition).y, myCam.ScreenToWorldPoint (Input.mousePosition).z + 10);
//					if (Input.GetKeyDown ("mouse 0") && iceBlockLimit <= 3)
//					{
//						GameObject realIceBlawk = Instantiate (IceBlawk, iceBlawkOpaqueTarget.transform.position, iceBlawkOpaqueTarget.transform.rotation) as GameObject;
//						Destroy (realIceBlawk, iceBlawkDuration);
//					}
//				}
//			}
//			if (Input.GetButtonUp ("Fire2"))
//			{
//				secondSkillLock = false;
//				IceRadius.SetActive (false);
//			}
//		}
	}

//	private void ButtonDown()
//	{
//		anim.SetBool ("Holding", true);
//		progressBar.renderer.enabled = true;
//		anim.SetBool ("Casting", true);
//		if (power <= 100) 
//		{
//			time -=Time.deltaTime * 1.65f;
//			progressBar.renderer.material.SetFloat("_Cutoff", time);
//			power += Time.deltaTime * 70;
//		}//end if
//	}
//
//	private void ButtonUp()
//	{
//		anim.SetBool("Preparing", false);
//		anim.SetBool("Holding", false);
//		time = 1;
//		progressBar.renderer.enabled = false;
//		if (currentSkill == 1 && isFireBallCooldown == false) 
//		{
//			isFireBallCooldown = true;
//			GameObject createClone = Instantiate (fireBallProjectile, transform.position, transform.rotation) as GameObject;
//			createClone.transform.localEulerAngles = new Vector3(0,90,0);
//			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
//
////			ParticleSystem.Particle[] createCloneParticles = new ParticleSystem.Particle[particleSystem.particleCount];
////			this.particleSystem.GetParticles(createCloneParticles);
////
////			for(int i=0; i < createCloneParticles.Length; i++)
//
//			//Debug.Log (power);
//			Destroy(createClone, 5); 
//			StartCoroutine(simulateFireBallCooldown());
//		}//end if Fireball logic
//		
//		if (currentSkill == 2 && isLightningCooldown == false) 
//		{
//			isLightningCooldown = true;
//			GameObject createClone = Instantiate (lightningNodeProjectile, transform.position, transform.rotation) as GameObject;
//			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
//			//Debug.Log (power);
//			Destroy(createClone, 5); 
//			
//			StartCoroutine(simulateLighteningCooldown());
//		}//end if Lightening logic
//
//		if (currentSkill == 3 && isIceBallCooldown == false) 
//		{
//			isIceBallCooldown = true;
//			GameObject createClone = Instantiate (IceBallProjectile, transform.position, transform.rotation) as GameObject;
//			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
//			//Debug.Log (power);
//			Destroy(createClone, 5); 
//			
//			StartCoroutine(simulateIceBallCooldown());
//		}//end if Lightening logic
//		
//		power = 0;
////		buttonDown = false;
//		anim.SetBool ("Casting", false);
//		
//	}
//
//	private void ThrowLightBall(Vector3 player, Vector3 mousePosition)
//	{
//		//Doesnt need player, but too lazy to remove right now.
//		Debug.Log ("Throwing Light Ball");
//		GameObject lightClone = Instantiate (lightBall, transform.position, Quaternion.identity) as GameObject;
//		lightClone.GetComponent<LightBallScript> ().setVectors (new Vector3 (transform.position.x, transform.position.y, -5f), new Vector3(mousePosition.x, mousePosition.y, -5f));
//	}
//	private Vector3 AdjustMousePositionInWorld(Vector3 hitPoint)
//	{
//		float z_CameraPlayerDistance = transform.position.z - Camera.main.transform.position.z;
//		Vector3 newMousePositionInWorld = hitPoint - Camera.main.transform.position;
//		newMousePositionInWorld /= newMousePositionInWorld.z;
//		newMousePositionInWorld *= z_CameraPlayerDistance;
//		return Camera.main.transform.position + newMousePositionInWorld;
//	}
//	
//	private IEnumerator simulateFireBallCooldown(){
//		fireBallWait = fireBallCooldown;
//		for (var x = 1; x < fireBallCooldown; x++) {
//			fireBallWait--;
//			transform.parent.FindChild ("FireGUI").GetComponent<FireGUI>().startCooldown(fireBallWait);
//			yield return new WaitForSeconds(1);
//		}//end for
//		transform.parent.FindChild ("FireGUI").GetComponent<FireGUI>().endCooldown();
//		isFireBallCooldown = false;
//		fireBallWait = 0;
//	}
//	
//	private IEnumerator simulateLighteningCooldown(){
//		lightningWait = lightningCooldown;
//		for (var x = 1; x < lightningCooldown; x++) {
//			lightningWait--;
//			transform.parent.FindChild ("LightningGUI").GetComponent<LightningGUI>().startCooldown(lightningWait);
//			yield return new  WaitForSeconds(1);
//		}//end for
//		transform.parent.FindChild ("LightningGUI").GetComponent<LightningGUI>().endCooldown();
//		isLightningCooldown = false;
//		lightningWait = 0;
//		
//	}
//
//	private IEnumerator simulateIceBallCooldown()
//	{
//		iceBallWait = iceBallCooldown;
//		for (var x = 1; x < iceBallCooldown; x++)
//		{
//			iceBallWait--;
//			transform.parent.FindChild ("IceGUI").GetComponent<IceGUI>().startCooldown(iceBallWait);
//			yield return new WaitForSeconds(1);
//		}
//		isIceBallCooldown = false;
//		transform.parent.FindChild ("IceGUI").GetComponent<IceGUI>().endCooldown();
//		iceBallWait = 0;
//			
//	}

	public void setSecondSkillLock(bool state)	{	secondSkillLock = state;	}
}
