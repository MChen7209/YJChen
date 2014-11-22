using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class SkillsController : MonoBehaviour 
{
	//Administration
	private bool secondSkillLock;

	Dictionary<string, float> skillsDamageList;
	List<Skills> skillsList;

	// Use this for initialization
	public SkillsController()
	{
		skillsDamageList = new Dictionary<string, float>();
		skillsList = new List<Skills>();
	}

	void Awake () 
	{
//		skillsDamageList = new Dictionary<string, float>();
//		skillsList = new List<Skills>();
	}

	public void initialize(string fileName)
	{
		try
		{
			StreamReader textReader = new StreamReader(fileName);
			string input = "";
			
			using(textReader)
			{
				do
				{
					input = textReader.ReadLine();
					if(input != null)
					{
						string[] skillInfo = input.Split(',');
						string sName = skillInfo[0];
						float sDamage = float.Parse(skillInfo[1]);
						skillsDamageList.Add (sName, sDamage);
						Debug.Log (skillsDamageList.ContainsKey(sName));
						skillsList.Add(System.Activator.CreateInstance(Type.GetType(sName)) as Skills);
						skillsList.Find (e => e.SkillName.CompareTo(sName) == 0).SkillDamage = sDamage;
						Debug.Log (skillsList[0].SkillName + skillsList[0].SkillDamage);
					}
				}
				while(input != null);
			}
		}
		catch (IOException e)
		{
			Debug.Log(e.ToString());
		}
	}

	private void adminPrintAll()
	{
		Debug.Log(skillsDamageList.Count);
		Debug.Log(skillsDamageList.ToString());
	}

	public float getSkillDamage(string skillName)		{	adminPrintAll(); return skillsDamageList[skillName];		}

	public Skills getSkill(string skillName)			{	return skillsList.Find (e => e.SkillName.CompareTo(skillName) == 0); }
}