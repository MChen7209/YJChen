using UnityEngine;
using System.Collections;

public class IceBallSkill : Skills 
{
	public IceBallSkill(string name, string type, string skillClass, string desc, string damage, string cooldown) : base(name, type, skillClass, desc, float.Parse(damage), float.Parse(cooldown))
	{
		setSkillProjectile("IceBallSkill");
	}
	
	public IceBallSkill(string name, string type, string skillClass, string desc, float damage, float cooldown) : base(name, type, skillClass, desc, damage, cooldown)
	{
		setSkillProjectile("IceBallSkill");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(object.ReferenceEquals(skillProjectile, null))
			setSkillProjectile("FireBallSkill");
		if(anim == null)
			anim = transform.root.GetComponent<Animator>();

		if (Input.GetKey ("mouse 0") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0) && !atkController.SecondSkillLock)
			ButtonDown ();
		if (Input.GetButtonUp ("Fire1") && (anim.GetInteger("Skill").CompareTo(mySlot+1) == 0) && !atkController.SecondSkillLock)
			ButtonUp ();
	}

	protected override void ButtonDown ()
	{
		Debug.Log("Button is down");
		anim.SetBool ("Holding", true);
		anim.SetBool ("Casting", true);
		if (power <= 100) 
		{
			time -=Time.deltaTime * 1.65f;
			power += Time.deltaTime * 70;
		}//end if
	}

	protected override void ButtonUp ()
	{
		anim.SetBool("Holding", false);
		time = 1;
		//progressBar.renderer.enabled = false;
		
		if (!isSkillCooldown) 
		{
			////			isSkillCooldown = true;
			GameObject createClone = Instantiate (skillProjectile, transform.position, transform.rotation) as GameObject;
			createClone.transform.localEulerAngles = new Vector3(0,90,0);
			createClone.rigidbody2D.velocity = transform.TransformDirection (new Vector3 (20 +power, 0, 0));
			Destroy(createClone, 5); 
			StartCoroutine(simulateCooldown());
		}//end if Fireball logic
		
		power = 0;
		anim.SetBool ("Casting", false);
	}

	protected override void doAfterInitialize ()
	{
	}

	public override void skillActivate ()
	{

	}
}
