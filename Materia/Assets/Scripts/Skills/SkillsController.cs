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
//						string sType = skillInfo[1];
//						string sClass = skillInfo[2];
//						string sDesc = skillInfo[3];
//						float sDamage = float.Parse(skillInfo[4]);

						object skillScript = System.Activator.CreateInstance(Type.GetType(sName), skillInfo);
						skillsList.Add(skillScript as Skills);
//						skillsList.Find (e => e.SkillName.CompareTo(sName) == 0).SkillDamage = sDamage;
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

	public Skills getSkill(string skillName)			{	return skillsList.Find (e => e.SkillName.CompareTo(skillName) == 0); }
}