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
	protected bool skillEquipped;
	protected bool skillUnlocked;
	protected int mySlot;
	protected AttackController atkController;
	
//	protected GameObject skillOwner;

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
		skillEquipped = false;
		anim = null;
		isSkillCooldown = false;
	}

	public Skills(string name, string type, string classSkill, string desc, float damage, float cooldown)
	{
		skillName	= name;
		skillType	= type;
		skillClass = classSkill;
		skillDescription = desc;
		skillDamage = damage;
		skillCooldown = cooldown;

		secondSkillLock = false;
		time = 1;
		power = 0.0f;
		skillUnlocked = false;
		skillEquipped = false;
		anim = null;
		isSkillCooldown = false;
	}

	void Awake()
	{
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.SkillsController;
		atkController = transform.root.GetComponentInChildren<AttackController>();
//		Debug.Log(atkController.SecondSkillLock);
	}

	protected void searchUnlocked()
	{
//		Debug.Log(skillName);
		Debug.Log(skillName + " " + skillType + " " + skillClass + " " + skillDescription + " " +skillDamage);
		Debug.Log(skillName + ": " +mySlot);
		Debug.Log(skillName + ": " + skillUnlocked);
	}

	//protected abstract IEnumerator simulateCooldown();
	public abstract void skillActivate();
	protected abstract void ButtonDown();
	protected abstract void ButtonUp();

//	public void initialize(string name, string type, string skillClass, string description, float damage)
//	{
////		this.skillName = name;
////		this.skillType = type;
////		this.skillClass = skillClass;
////		this.skillDescription = description;
////		this.skillDamage = damage;
//		skillName	= name;
//		skillType	= type;
//		skillClass = skillClass;
//		skillDescription = description;
//		skillDamage = damage;
//
//		
//		secondSkillLock = false;
//		time = 1;
//		power = 0.0f;
//		skillUnlocked = false;
//		skillEquipped = false;
//		anim = null;
//		isSkillCooldown = false;
//	}

	public void initialize(Skills other)
	{
		//		this.skillName = name;
		//		this.skillType = type;
		//		this.skillClass = skillClass;
		//		this.skillDescription = description;
		//		this.skillDamage = damage;
		skillName	= other.skillName;
		skillType	= other.skillType;
		skillClass = other.skillClass;
		skillDescription = other.skillDescription;
		skillDamage = other.skillDamage;
		mySlot = other.mySlot;
		skillCooldown = other.skillCooldown;
		skillProjectile = other.skillProjectile;
		
		secondSkillLock = false;
		time = 1;
		power = 0.0f;
		skillUnlocked = false;
		skillEquipped = false;
		anim = null;
		isSkillCooldown = false;
	}

	protected abstract void doAfterInitialize();

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
		//transform.parent.FindChild ("FireGUI").GetComponent<FireGUI>().endCooldown();
		isSkillCooldown = false;
		skillWait = 0;
	}

//	public GameObject SkillOwner
//	{
//		get	{	return skillOwner;	}
//		set	{	skillOwner = value;	}
//	}

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

	public string SkillClass
	{
		get	{	return skillClass;	}
		set	{	skillClass = value;	}
	}

	public bool SkillEquipped
	{
		get	{	return skillEquipped;	}
		set	{	skillEquipped = value;	}
	}

	public bool SkillUnlocked
	{
		get	{	return skillUnlocked;	}
		set	{	skillUnlocked = value;	}
	}

	public int SkillSlot
	{
		get {	return mySlot;	}
		set	{	mySlot = value;	}
	}

	public AttackController AtkControllerScript
	{
		get	{	return atkController;	}
		set	{	atkController = value;	}
	}

	protected void setSkillProjectile(/*string targetSkillName*/)	{	skillProjectile = Resources.Load("skills/" + skillName/*targetSkillName*/) as GameObject; }
	protected void setSkillProjectile(string targetSkillName)	{	skillProjectile = Resources.Load("skills/" + targetSkillName) as GameObject;
//		Debug.Log(skillProjectile.tag);
	}
}
