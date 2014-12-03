using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AttackController : MonoBehaviour 
{
	//Administration
	private Animator anim;
	private bool secondSkillLock;
	private UnifiedSuperClass god;
	private SkillsController skillsController;
	bool skillLock;

	private List<Skills> _skills;
	private List<Skills> _utility;

	public float power = 0.0F;
	private Vector3 mousePos;

	private float time;
	public GameObject progressBar;
	
	//guys test code to make ray cast line
	private Vector3 newWorldPos;
	
	// Cooldowns
	private int currentSkill;
	private Renderer renderer;

	void Start()
	{
		secondSkillLock = false;
		skillLock = false;
		time = 1;

		transform.root.FindChild("IceRadius").gameObject.SetActive (false);
//		progressBar.renderer.enabled = false;
//		tempLightBallOn = false;
	}

	void Awake () 
	{
		_skills = new List<Skills>();
		_utility = new List<Skills>();
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.SkillsController;

		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetInteger ("Skill", 1);
		currentSkill = anim.GetInteger ("Skill");
	}

	public void loadSkills(List<Skills> loadSkills)
	{
		_skills = loadSkills;
		_skills.ForEach(e => {
			Skills temp = gameObject.AddComponent(e.SkillName) as Skills;
			temp.initialize(e);
			temp.AtkControllerScript = this;
		});
//		_skills.ForEach(e => gameObject.AddComponent<Skills>());
//		RAWR THIS is THE PROBLEM//		_skills.ForEach(e => gameObject.AddComponent<Skills>() = e);
	}
	
	void Update () 
	{
		if(skillLock || secondSkillLock)	//This makes it so you can't change skills when using skills.
			return;

		currentSkill = anim.GetInteger ("Skill");

		if (Input.GetKeyDown (KeyCode.Alpha1))
			anim.SetInteger ("Skill", 1);

		if (Input.GetKeyDown (KeyCode.Alpha2))
			anim.SetInteger ("Skill", 2);

		if (Input.GetKeyDown (KeyCode.Alpha3))
			anim.SetInteger ("Skill", 3);

		if (Input.GetKeyDown (KeyCode.Alpha4))
			anim.SetInteger ("Skill", 4);

		if (anim.GetBool ("skillLock") == true)
			return;

////		Debug.Log(_skills.Count);
//		if(!secondSkillLock && (_skills.Count > 0))
//			_skills[currentSkill-1].skillActivate();
//
//		if(_utility.Count > 0)
//			_utility[currentSkill-1].skillActivate();
		mousePos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - mousePos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

	}

	public void setSecondSkillLock(bool state)	{	secondSkillLock = state;	}

	public bool SecondSkillLock
	{
		get	{	return secondSkillLock;		}
		set	{	secondSkillLock = value;	}
	}
}
