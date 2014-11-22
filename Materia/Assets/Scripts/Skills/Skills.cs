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
//		Debug.Log("INFINITY");
		secondSkillLock = false;
		time = 1;
		power = 0.0f;
		skillOn = false;
//		Debug.Log("AND BEYOND");
	}
	void Awake()
	{
//		Debug.Log("Fallopian");
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.getSkillsController();
//		Debug.Log("Tubes");
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
		object item = Activator.CreateInstance(Type.GetType(skillGUIName));
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
		get	{	return SkillDamage;		}
		set	{	SkillDamage = value;	}
	}

//	public string getSkillName()			{	return skillName;			}
//	public string getSkillType()			{	return skillType;			}
//	public string getSkillDescription()		{	return skillDescription;	}
//	public float  getSkillDamage()			{	return skillDamage;			}
//
//	protected void setSkillName(string sN)				{	skillName = sN;			}
//	protected void setSkillType(string sT)				{	skillType = sT;			}
//	protected void setSkillDescription(string sD)		{	skillDescription = sD;	}
//	public void setSkillDamage(float dmg)				{	skillDamage = dmg;		}
	protected void setSkillProjectile(string fileName)	{	skillProjectile = Resources.Load("skills/" + fileName) as GameObject; }
}
