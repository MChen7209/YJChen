using UnityEngine;
using System.Collections;

public class FireBallSkill : Skills 
{
//	public GameObject fireBallProjectile;
//	private FireballScript fbScriptConnector;
	private float fireBallDamage;

	public FireBallSkill()
	{
//		Debug.Log("SUPERIOUS");
		initialize("FireBallSkill", "attack", "Wizard", "Simple ball of fire that may be able to light up objects and maybe some enemies", 1f);
//		Debug.Log("INFERIOUS");
	}

	void Start()
	{
		skillOwner = god.getCharacter(skillType).CharacterGameObject;
		Debug.Log("Owner Tag: " + skillOwner.tag);
		anim = skillOwner.gameObject.GetComponent<Animator>();
		Debug.Log (anim.GetBool("Holding"));
		setSkillProjectile("FireBall");
	}

	public override void skillActivate()
	{
		//Fix logic, secondSkillLock, Input.getKey logic.
		if (Input.GetKey ("mouse 0") && !secondSkillLock)
		{
			ButtonDown ();
		}//end if
		// If left mouse button is released
		if (Input.GetButtonUp ("Fire1") && !secondSkillLock)
		{
			ButtonUp ();
		}//end if
	}

	protected override void ButtonDown()
	{
		anim.SetBool ("Holding", true);
//		progressBar.renderer.enabled = true;
		anim.SetBool ("Casting", true);
		if (power <= 100) 
		{
			time -=Time.deltaTime * 1.65f;
//			progressBar.renderer.material.SetFloat("_Cutoff", time);
			power += Time.deltaTime * 70;
		}//end if
	}

	protected override void ButtonUp()
	{
		anim.SetBool("Holding", false);
		time = 1;
		//progressBar.renderer.enabled = false;
		if (!isSkillCooldown) 
		{
			isSkillCooldown = true;
			GameObject createClone = Instantiate (skillProjectile, skillOwner.transform.position, skillOwner.transform.rotation) as GameObject;
			createClone.transform.localEulerAngles = new Vector3(0,90,0);
			createClone.rigidbody2D.velocity = skillOwner.transform.TransformDirection (new Vector3 (20 +power, 0, 0));
			createClone.GetComponentInChildren<FireballScript>().setFireballDamage(fireBallDamage);
			//Debug.Log (power);
			Destroy(createClone, 5); 
			StartCoroutine(simulateCooldown());
		}//end if Fireball logic

		power = 0;
		anim.SetBool ("Casting", false);
	}
}
