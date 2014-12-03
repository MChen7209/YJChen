using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightBallUtility : Skills 
{
	public GameObject lightBall;
	private GameObject tempLightBall;
	bool tempLightBallOn;
	private List<GameObject> lightBallArray = new List<GameObject>();

//	private int _lightBallCount = 0;
	private int _lightBallLimit;
	private int _lightBallDuration;

	public LightBallUtility(string name, string type, string skillClass, string desc, string damage, string cooldown) : base(name,type,skillClass,desc,float.Parse(damage),float.Parse (cooldown))
	{
		setSkillProjectile("LightBall");
	}

	// Use this for initialization
	void Start () 
	{
//		AtkControllerScript = transform.root.GetComponentInChildren<AttackController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(object.ReferenceEquals(skillProjectile, null))
			setSkillProjectile("LightBall");
		if(anim == null)
			anim = transform.root.GetComponent<Animator>();
		if(object.ReferenceEquals(lightBallArray, null))
		{
		   lightBallArray = new List<GameObject>(); 
			Debug.Log("new lightballarray");
		}

		if(lightBallArray.Count > 0 && object.ReferenceEquals(lightBallArray[0],null))
		   lightBallArray.RemoveAt(0);

		if (Input.GetButton ("Fire2") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0))
		{
			if(!tempLightBallOn)
			{	
				tempLightBall = Instantiate ( skillProjectile, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity) as GameObject;
				tempLightBallOn = true;
				secondSkillLock = true;
				atkController.SecondSkillLock = true;
			}
//			Debug.Log("Lightball Position: " + skillProjectile.transform.position);
//			Debug.Log("Wand      Position: " + transform.position);
			tempLightBall.transform.position = (Vector2)transform.position;
			//					tempLightBall.transform.position = transform.position;
			//					tempLightBall.transform.position.x = negativeFiveSetter;
			
			if(Input.GetButtonDown ("Fire1"))
			{
				Debug.Log ("Throw Light");
				ThrowLightBall((Vector3)transform.position, (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
		}

		if(Input.GetButtonUp ("Fire2"))
		{
			Destroy(tempLightBall);
			tempLightBallOn = false;
			secondSkillLock = false;
			atkController.SecondSkillLock = false;
		}
	}

	protected override void ButtonDown ()
	{
		throw new System.NotImplementedException ();
	}	
	protected override void ButtonUp ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void doAfterInitialize ()
	{

	}

	public override void skillActivate ()
	{

	}

	private void ThrowLightBall(Vector3 player, Vector3 mousePosition)
	{
		//Doesnt need player, but too lazy to remove right now.
		if(lightBallArray.Count > 3)//_lightBallLimit)
		{
			Destroy (lightBallArray[0]);
			lightBallArray.RemoveAt(0);
		}

		if(lightBallArray.Count > 0 && object.ReferenceEquals(lightBallArray[0],null))
			lightBallArray.RemoveAt(0);

		GameObject lightClone = Instantiate (skillProjectile, transform.position, Quaternion.identity) as GameObject;
		lightClone.GetComponent<LightBallScript> ().setVectors (new Vector3 (transform.root.transform.position.x, transform.root.transform.position.y, -5f), new Vector3(mousePosition.x, mousePosition.y, -5f));
		lightBallArray.Add (lightClone);
		Destroy(lightClone, 10);
	}

	public int LightBallLimit
	{
		get	{	return _lightBallLimit;		}
		set	{	_lightBallLimit = value;	}
	}
}
