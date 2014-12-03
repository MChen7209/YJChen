using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Remoting;

public class SkillsController : MonoBehaviour 
{
	//Administration
	private bool secondSkillLock;
	List<Skills> skillsList;

	// Use this for initialization
	public SkillsController()
	{
		skillsList = new List<Skills>();
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
//						Debug.Log(sName);
						object skillScript = System.Activator.CreateInstance(Type.GetType(sName), skillInfo);
						skillsList.Add(skillScript as Skills);
//						skillsList.ForEach(e => Debug.Log(e.SkillName));
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

	public List<Skills> AllSkillsList
	{
		get	{ return skillsList;	}
	}

	public void unlockSkill(string skillName)			{	skillsList.Find (e=>e.SkillName.Equals(skillName)).SkillUnlocked = true;	}

	public Skills getSkill(string skillName)			{	return skillsList.Find (e => e.SkillName.Equals(skillName)); }
}