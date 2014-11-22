using UnityEngine;
using System.Collections;

public class LightBallUtility : Skills 
{
	public GameObject lightBall;
	private GameObject tempLightBall;
	bool tempLightBallOn;

	public LightBallUtility()
	{
		initialize("LightBallSkill", "utility", "Wizard", "The right path can be found even in the darkest of places, when one only remembers to turn on the light.", 0f);
		skillOwner = god.getCharacter(skillType).CharacterGameObject;
		anim = skillOwner.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

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
		if(!tempLightBallOn)
		{	
			tempLightBall = Instantiate ( lightBall, new Vector3(skillOwner.transform.position.x, skillOwner.transform.position.y, -5), Quaternion.identity) as GameObject;
			tempLightBallOn = true;
//				secondSkillLock = true;
		}
		//			Debug.Log("Lightball Position: " + lightBall.transform.position);
		//			Debug.Log("Wand      Position: " + skillOwner.transform.position);
		tempLightBall.transform.position = (Vector2)transform.position;
		
		if(Input.GetButtonDown ("Fire1"))
		{
			Debug.Log ("Throw Light");
			ThrowLightBall((Vector3)transform.position, (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		if(Input.GetButtonUp ("Fire2"))
		{
			Destroy(tempLightBall);
			tempLightBallOn = false;
//			secondSkillLock = false;
		}	
	}
	private void ThrowLightBall(Vector3 player, Vector3 mousePosition)
	{
		//Doesnt need player, but too lazy to remove right now.
		Debug.Log ("Throwing Light Ball");
		GameObject lightClone = Instantiate (lightBall, transform.position, Quaternion.identity) as GameObject;
		lightClone.GetComponent<LightBallScript> ().setVectors (new Vector3 (skillOwner.transform.position.x, skillOwner.transform.position.y, -5f), new Vector3(mousePosition.x, mousePosition.y, -5f));
	}
}
