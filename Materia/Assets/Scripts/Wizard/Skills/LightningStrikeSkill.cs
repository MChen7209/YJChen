using UnityEngine;
using System.Collections;

public class LightningStrikeSkill : Skills 
{
	private float lightningStrikeDamage;
	
	public LightningStrikeSkill(string name, string type, string skillClass, string desc, string damage, string cooldown) : base(name, type, skillClass, desc, float.Parse(damage), float.Parse(cooldown))
	{
		setSkillProjectile("LightningStrikeSkill");
	}
	
	public LightningStrikeSkill(string name, string type, string skillClass, string desc, float damage, float cooldown) : base(name, type, skillClass, desc, damage, cooldown)
	{
		setSkillProjectile("LightningStrikeSkill");
	}

	void Awake()
	{
//		Debug.Log(skillProjectile.tag);
//		skillProjectile.particleSystem.renderer.sortingLayerName = "CastleFront";
	}

	void Update()
	{
		//		Debug.Log("isSkillCooldown: " + isSkillCooldown);
//		searchUnlocked();
//		Debug.Log(skillName + " " + skillType + " " + skillClass + " " + skillDescription + " " +skillDamage);
		if(object.ReferenceEquals(skillProjectile, null))
			setSkillProjectile("LightningStrikeSkill");
		if(anim == null)
			//			anim = skillOwner.gameObject.GetComponent<Animator>();
			anim = transform.root.GetComponent<Animator>();
//		Debug.Log("My Spot: " + mySlot);
		//Fix logic, secondSkillLock, Input.getKey logic.
		Debug.Log(anim.GetInteger("Skill"));
//		Debug.Log(atkController.SecondSkillLock);
		if (Input.GetKey ("mouse 0") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0) && !atkController.SecondSkillLock)
			ButtonDown ();
		// If left mouse button is released
		if (Input.GetButtonUp ("Fire1") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0) && !atkController.SecondSkillLock)
			ButtonUp ();
	}

	protected override void doAfterInitialize ()
	{
		skillProjectile.particleSystem.renderer.sortingLayerName = "CastleFront";
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
			isSkillCooldown = true;
			GameObject createClone = Instantiate (skillProjectile, transform.position, transform.rotation) as GameObject;
			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
			Destroy(createClone, 5); 
			StartCoroutine(simulateCooldown());
		}//end if Fireball logic
		
		power = 0;
		anim.SetBool ("Casting", false);
	}
}
