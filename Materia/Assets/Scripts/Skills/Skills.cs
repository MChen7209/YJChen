using UnityEngine;
using System.Collections;
using System;

public abstract class Skills : MonoBehaviour 
{
	//Administration
	protected UnifiedSuperClass god;
	protected SkillsController skillsController;
	protected Animator anim;
	protected bool secondSkillLock;
	protected float time;
	protected GameObject progressBar;
	protected bool skillOn;

	protected GameObject skillOwner;

	//Skill Details
	protected string skillName;
	protected string skillType;
	protected string skillClass;
	protected string skillDescription;
	protected float skillDamage;

	//Skill Components
	protected GameObject skillProjectile;
	protected bool isSkillCooldown;
	protected float skillCooldown;
	protected float skillWait;
	protected string skillGUIName;

	protected float power;
	protected Vector3 mousePos;

	// Use this for initialization
	public Skills()
	{
		secondSkillLock = false;
		time = 1;
		power = 0.0f;
		skillOn = false;
		anim = null;
	}

	public Skills(string name, string type, string skillClass, string desc, float damage)
	{
		this.skillName	= name;
		this.skillType	= type;
		this.skillClass = skillClass;
		this.skillDescription = desc;
		this.skillDamage = damage;
	}

	void Awake()
	{
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.getSkillsController();
	}

	//protected abstract IEnumerator simulateCooldown();
	public abstract void skillActivate();
	protected abstract void ButtonDown();
	protected abstract void ButtonUp();

	protected void initialize(string name, string type, string skillClass, string description, float damage)
	{
		this.skillName = name;
		this.skillType = type;
		this.skillClass = skillClass;
		this.skillDescription = description;
		this.skillDamage = damage;
	}

	protected IEnumerator simulateCooldown()
	{
//		object item = Activator.CreateInstance(Type.GetType(skillGUIName));
		skillWait = skillCooldown;
		for (var x = 1; x < skillCooldown; x++) 
		{
			skillWait--;
//			skillOwner.transform.FindChild(skillGUIName).GetComponent<System.Activator.CreateInstance(Type.GetType (skillGUIName))>().
//			transform.parent.FindChild ("FireGUI").GetComponent<(Skills)skillGUIName>().startCooldown(fireBallWait);
			yield return new WaitForSeconds(1);
		}//end for
		transform.parent.FindChild ("FireGUI").GetComponent<FireGUI>().endCooldown();
		isSkillCooldown = false;
		skillWait = 0;
	}

	public GameObject SkillOwner
	{
		get	{	return skillOwner;	}
		set	{	skillOwner = value;	}
	}

	public string SkillName
	{
		get	{	return skillName;	}
		set	{	skillName = value;	}
	}

	public string SkillType
	{
		get	{	return skillType;	}
		set	{	skillType = value;	}
	}

	public string SkillDescription
	{
		get	{	return skillName;	}
		set	{	skillName = value;	}
	}

	public float SkillDamage
	{
		get	{	return skillDamage;		}
		set	{	skillDamage = value;	}
	}
	protected void setSkillProjectile(string fileName)	{	skillProjectile = Resources.Load("skills/" + fileName) as GameObject; }
}
