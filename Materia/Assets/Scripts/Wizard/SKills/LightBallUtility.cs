using UnityEngine;
using System.Collections;

public class LightBallUtility : Skills 
{
	public GameObject lightBall;
	private GameObject tempLightBall;
	bool tempLightBallOn;

	public LightBallUtility(string name, string type, string skillClass, string desc, string damage, string cooldown) : base(name,type,skillClass,desc,float.Parse(damage),float.Parse (cooldown))
	{
		setSkillProjectile("LightBall");
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(object.ReferenceEquals(skillProjectile, null))
			setSkillProjectile("FireBallSkill");

		if (Input.GetButton ("Fire2") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0))
		{
			if(!tempLightBallOn)
			{	
				tempLightBall = Instantiate ( lightBall, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity) as GameObject;
				tempLightBallOn = true;
				secondSkillLock = true;
			}
			Debug.Log("Lightball Position: " + lightBall.transform.position);
			Debug.Log("Wand      Position: " + transform.position);
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

	public override void skillActivate ()
	{

	}
	private void ThrowLightBall(Vector3 player, Vector3 mousePosition)
	{
		//Doesnt need player, but too lazy to remove right now.
		Debug.Log ("Throwing Light Ball");
		GameObject lightClone = Instantiate (lightBall, transform.position, Quaternion.identity) as GameObject;
		lightClone.GetComponent<LightBallScript> ().setVectors (new Vector3 (transform.root.transform.position.x, transform.root.transform.position.y, -5f), new Vector3(mousePosition.x, mousePosition.y, -5f));
	}
}
