using UnityEngine;
using System.Collections;

public class IceBlawkOpaque : MonoBehaviour 
{
	bool onSkillCircle;
	void Start()
	{
		Camera myCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		onSkillCircle = false;
	}

	void Update()
	{


	}

	public void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.collider2D.tag != transform.parent.gameObject.tag)
		{
			//Debug.Log(target.gameObject.tag);
			onSkillCircle = false;
		}
//		Debug.Log (target.gameObject.tag);
		onSkillCircle = true;
	}

	public bool getOnSkillCircle()
	{
		return onSkillCircle;
	}
}
