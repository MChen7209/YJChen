using UnityEngine;
using System.Collections;

public class WarriorAttack : MonoBehaviour 
{
	private Animator anim;
	public GameObject sword;
	public GameObject sword2;
	public GameObject holsterSword;
	public GameObject shield;


	private int currentSkill;
	private int currentSkillOn;

	//Skill Spinny
	//public GameObject spinny;
	private bool spinnyOnCooldown;
	public float spinnyCooldownTime;
	private float spinnyWait;
	public float spinDuration;
	private float setSpinDuration;
	private bool spinSkillLock;
	//Skill Spinny Close

	//Skill Dash
	private bool dashOnCooldown;
	public float dashCooldownTime;
	private float dashWait;
	public float dashDuration;
	private float setDashDuration;
	private bool dashSkillLock;
	//Skill Dash Close

	//Skill Slash
	private bool slashOnCooldown;
	public float slashCooldownTime;
	private float slashWait;
	//Skill slash Close

	private bool buttonDown;

	// Use this for initialization
	void Awake()
	{
		buttonDown = false;
		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetInteger ("Skill", 1);
		currentSkill = anim.GetInteger ("Skill");
		dashSkillLock = false;
		spinSkillLock = false;
		setDashDuration = dashDuration;
		setSpinDuration = spinDuration;
		holsterSword.SetActive(false);
		shield.SetActive (false);
		//linked = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//currentSkill = anim.GetInteger ("Skill");

		if (Input.GetKeyDown (KeyCode.Alpha1)){
			anim.SetInteger ("Skill", 1);
			currentSkill = 1;
		}

		if (Input.GetKeyDown (KeyCode.Alpha2))
				{
			currentSkill = 2;
						anim.SetInteger ("Skill", 2);
				}

		if (Input.GetKeyDown (KeyCode.Alpha3))
				{
						currentSkill = 3;
						anim.SetInteger ("Skill", 3);
				}

		if (Input.GetKeyDown (KeyCode.Alpha4))
			anim.SetInteger ("Skill", 4);

		if (Input.GetKeyDown (KeyCode.Alpha5))
			anim.SetInteger ("Skill", 5);

		if (dashSkillLock == true)
			return;

//		if(anim.GetBool ("skillLock"))
//		   return;


		if (Input.GetMouseButtonDown (0))
		{
			if(currentSkill == 1)
			{
				// sword swing animation
				anim.Play("Forward Slash");

				slashOnCooldown = true;
				StartCoroutine(simulateSlashCooldown());

			}//end if
			if(currentSkill == 2){
				anim.Play("Holster");
				float length = anim.GetCurrentAnimatorStateInfo(0).length;
				float time = 0;
				while(time < length)
					time += Time.deltaTime;
				holsterSword.SetActive(true);
				sword.SetActive(false);
				sword2.SetActive(false);
				shield.SetActive(true);
			}

			if(currentSkill == 3){

			}
		}//end if

		if (Input.GetKey ("mouse 0"))
		{
			buttonDown = true;
			// Dash
			if(currentSkill == 2 && !dashOnCooldown && dashDuration > 0){

				anim.SetBool("Dashing", true);
				//anim.Play("Dash");
				dashDuration -= Time.deltaTime;
				transform.parent.rigidbody2D.AddForce(transform.parent.right * 500);

			}//end if

			// Spin

			//Debug.Log("currentskill = " + currentSkill + "Spinnyoncooldown = " + spinnyOnCooldown + "spin duration = " + spinDuration);

			if(currentSkill == 3 && !spinnyOnCooldown && spinDuration > 0){
				Debug.Log("Should be setting bool true");
				anim.SetBool("Spinning", true);
				//anim.Play("Spin");
				spinDuration-=Time.deltaTime;

			}//end if
		}//end if fire

		float tempDash = Mathf.Ceil (dashDuration);
		float tempSpin = Mathf.Ceil (spinDuration);

		if (tempDash == 0){
			anim.SetBool("Dashing", false);
			dashSkillLock = true;
			tempDash = setDashDuration;
			dashOnCooldown = true;
			StartCoroutine (simulateDashCooldown ());

		}//end if dash duration

		if (tempSpin == 0){
			anim.SetBool("Spinning", false);
			spinSkillLock = true;
			tempSpin = setSpinDuration;
			spinnyOnCooldown = true;
			StartCoroutine (simulateSpinnyCooldown ());
		}//end if spin duration

		if(Input.GetButtonUp("Fire1") && currentSkill == 2 && buttonDown){
			buttonDown = false;

			sword.SetActive(true);
			sword2.SetActive(true);
			sword.GetComponent<MeleeWeaponTrail>().setEmit(true);
			sword2.GetComponent<SpinTrailScript>().setEmit(true);
			holsterSword.SetActive(false);
			shield.SetActive(false);

			if (!dashSkillLock && !dashOnCooldown)
			{
				anim.SetBool("Dashing", false);
				dashSkillLock = true;
				tempDash = setDashDuration;
				dashOnCooldown = true;
				StartCoroutine (simulateDashCooldown ());
			}//end if dash cd

		}//end button up dash

		if (Input.GetButtonUp ("Fire1") && currentSkill == 3 && buttonDown){
			buttonDown = false;
			if (!spinSkillLock && !spinnyOnCooldown){
				anim.SetBool ("Spinning", false);
				spinSkillLock = true;
				tempSpin = setSpinDuration;
				spinnyOnCooldown = true;
				StartCoroutine (simulateSpinnyCooldown ());
			}//end if spin cd

		}//end if button up spin
	}

	public int getSkill(){
		return currentSkill;
	}

	public float getDuration(){
		return setSpinDuration;
	}

	private IEnumerator simulateSlashCooldown(){
		slashWait = slashCooldownTime;
		for (var x = 1; x < slashCooldownTime; x++) {
			
			slashWait--;
			transform.parent.FindChild ("SlashGUI").GetComponent<SlashGUI>().startCooldown(slashWait);
			yield return new WaitForSeconds(1);
		}//end for
		slashOnCooldown = false;
		transform.parent.FindChild ("SlashGUI").GetComponent<SlashGUI>().endCooldown();
		slashWait = 0;
	}

	private IEnumerator simulateSpinnyCooldown()
	{
		spinnyWait = spinnyCooldownTime;
		for (var x = 1; x < spinnyCooldownTime; x++) 
		{
			spinnyWait--;
			transform.parent.FindChild ("SpinGUI").GetComponent<SpinningGUI>().startCooldown(spinnyWait);
			yield return new WaitForSeconds(1);
		}//end for
		spinnyOnCooldown = false;
		transform.parent.FindChild ("SpinGUI").GetComponent<SpinningGUI>().endCooldown();
		spinnyWait = 0;
		spinDuration = setSpinDuration;
		spinSkillLock = false;
	}

	private IEnumerator simulateDashCooldown()
	{
		dashWait = dashCooldownTime;
		for (var x = 1; x < dashCooldownTime; x++) 
		{
			dashWait--;
			transform.parent.FindChild ("DashGUI").GetComponent<DashGUI>().startCooldown(dashWait);
			yield return new WaitForSeconds(1);
		}//end for
		dashOnCooldown = false;
		transform.parent.FindChild ("DashGUI").GetComponent<DashGUI>().endCooldown();
		dashWait = 0;
		dashDuration = setDashDuration;
		dashSkillLock = false;
	}



}
