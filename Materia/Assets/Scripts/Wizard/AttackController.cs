using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour 
{
	//Administration
	private Animator anim;
	private bool secondSkillLock;
	private UnifiedSuperClass god;
	private SkillsController skillsController;

	private List<Skills> skills;
	private List<Skills> utility;

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
		time = 1;
//		progressBar.renderer.enabled = false;
//		tempLightBallOn = false;
	}

	void Awake () 
	{
		skills = new List<Skills>();
		utility = new List<Skills>();
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		skillsController = god.getSkillsController();

		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetInteger ("Skill", 1);
		currentSkill = anim.GetInteger ("Skill");
	}

	public void loadSkills(string skillName, string type)
	{

	}
	
	void Update () 
	{
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

		if(!secondSkillLock && (skills.Count > 0))
			skills[currentSkill-1].skillActivate();

		if(utility.Count >0)
			utility[currentSkill-1].skillActivate();
	}

	public void setSecondSkillLock(bool state)	{	secondSkillLock = state;	}
}
