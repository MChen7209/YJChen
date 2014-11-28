using UnityEngine;
using System.Collections;

public class FireBallSkill : Skills 
{
	private float fireBallDamage;
	
	public FireBallSkill(string name, string type, string skillClass, string desc, string damage, string cooldown) : base(name, type, skillClass, desc, float.Parse(damage), float.Parse(cooldown))
	{
		setSkillProjectile("FireBallSkill");
//		Debug.Log(skillProjectile.tag);
	}

	public FireBallSkill(string name, string type, string skillClass, string desc, float damage, float cooldown) : base(name, type, skillClass, desc, damage, cooldown)
	{
		setSkillProjectile("FireBallSkill");
	}

	void Update()
	{
//		Debug.Log("isSkillCooldown: " + isSkillCooldown);
		if(object.ReferenceEquals(skillProjectile, null))
			setSkillProjectile("FireBallSkill");
		if(anim == null)
//			anim = skillOwner.gameObject.GetComponent<Animator>();
			anim = transform.root.GetComponent<Animator>();
		//Fix logic, secondSkillLock, Input.getKey logic.
		if (Input.GetKey ("mouse 0") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0))
			ButtonDown ();
		// If left mouse button is released
		if (Input.GetButtonUp ("Fire1") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0))
			ButtonUp ();
	}

	public override void skillActivate()
	{

	}

	protected override void ButtonDown()
	{
		Debug.Log("Button is down");
		//		Debug.Log("Power: " + power);StartCoroutine(simulateCooldown());
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
		Debug.Log("Button is up");
		Debug.Log("isSkillCooldown: " + isSkillCooldown);
		anim.SetBool("Holding", false);
		time = 1;
		//progressBar.renderer.enabled = false;

		if (!isSkillCooldown) 
		{
////			isSkillCooldown = true;
//			Debug.Log(skillProjectile.tag);
			GameObject createClone = Instantiate (skillProjectile, transform.position, transform.rotation) as GameObject;
			createClone.transform.localEulerAngles = new Vector3(0,90,0);
			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
			createClone.GetComponentInChildren<FireballScript>().setFireballDamage(fireBallDamage);

			Debug.Log("createClone tag: " + createClone.tag);
			Debug.Log("createClone Position: " + createClone.transform.position);
			//Debug.Log (power);
			Destroy(createClone, 5); 
			StartCoroutine(simulateCooldown());
		}//end if Fireball logic

		power = 0;
		anim.SetBool ("Casting", false);
	}
}
