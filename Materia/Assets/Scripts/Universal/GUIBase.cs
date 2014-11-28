using UnityEngine;
using System.Collections;

public class GUIBase : MonoBehaviour 
{
	private string _guiTag;
	private Texture2D _skillImage;
	private float _cooldown;
	private string _cooldownDisplay;
	private bool _isCooldown;
	private int _skillSlot;

//	//Skill details
//	protected bool isSkillCooldown;
//	protected float skillCooldown;
//	protected float skillWait;
//	protected string skillGUIName;

	public GUIBase(string GUITag, string skillImage, int skillSlot)
	{
		_guiTag = GUITag;
		_skillImage = Resources.Load ("skillGUI/" + skillImage) as Texture2D;
		_skillSlot = skillSlot;
	}

	// Use this for initialization
	void Start () 
	{
		_isCooldown = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void startCooldown(float theCooldown)
	{
		_cooldown = theCooldown;
		_isCooldown = true;
	}
	
	public void endCooldown()
	{
		_isCooldown = false;
	}

//	protected IEnumerator simulateCooldown()
//	{
//		//		object item = Activator.CreateInstance(Type.GetType(skillGUIName));
//		skillWait = skillCooldown;
//		for (var x = 1; x < skillCooldown; x++) 
//		{
//			skillWait--;
//			//			skillOwner.transform.FindChild(skillGUIName).GetComponent<System.Activator.CreateInstance(Type.GetType (skillGUIName))>().
//			//			transform.parent.FindChild ("FireGUI").GetComponent<(Skills)skillGUIName>().startCooldown(fireBallWait);
//			yield return new WaitForSeconds(1);
//		}//end for
//		//transform.parent.FindChild ("FireGUI").GetComponent<FireGUI>().endCooldown();
//		isSkillCooldown = false;
//		skillWait = 0;
//	}

	void OnGUI()
	{
		if (_isCooldown == false)
		{
			GUILayout.BeginArea (new Rect (20, 7 * Screen.height / 8, 100, 100));
			GUILayout.Label (_skillImage);
			GUILayout.EndArea ();
		}//end if
		else
		{
			_cooldownDisplay = _cooldown.ToString();
			
			GUILayout.BeginArea (new Rect (20, 7 * Screen.height / 8, 100, 100));
			GUI.color = Color.gray; GUILayout.Label (_skillImage);
			GUILayout.EndArea ();
			GUI.color = Color.white;
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = 30;
			myStyle.fontStyle = FontStyle.Bold;
			myStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(37, 7 * Screen.height / 8+10, 100, 100), _cooldownDisplay, myStyle);
		}
		
	}
	
	public string GUITag
	{
		get	{	return _guiTag;		}
		set	{	_guiTag = value;	}
	}
}