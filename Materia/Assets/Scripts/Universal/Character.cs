using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Character : MonoBehaviour 
{
	//Character administiration
	PlayerHealthController healthController;

	//Character Descriptions
	string characterName;
	string characterClass;
	string characterDescription;
	GameObject characterPrefab;

	//Character Weapon
	Weapon characterWeapon;
	string[] applicableWeapons;

	//Character skill
	List<Skills> characterSkills;


	public Character(string name, string type, string description)
	{
		this.characterName = name;
		this.characterClass = type;
		this.characterDescription = description;
		characterSkills = new List<Skills>();
		characterWeapon = new Weapon();
		setPlayerPrefab();
	}

	public Weapon CharacterWeapon
	{
		get	{ 	return characterWeapon; 	}
		set { 	characterWeapon = value;	}
	}

	public string CharacterName
	{
		get	{ 	return characterName; 	}
		set { 	characterName = value;	}
	}

	public string CharacterClass
	{
		get	{ 	return characterClass; 	}
		set { 	characterClass = value;	}
	}

	public string CharacterDescription
	{
		get	{ 	return characterDescription; 	}
		set { 	characterDescription = value;	}
	}

	public GameObject CharacterGameObject
	{
		get	{	return characterPrefab;	}
	}

	public PlayerHealthController HealthController
	{
		get	{	return healthController;	}
	}

	public List<Skills> SkillsList
	{
		get	{	return characterSkills;	}
	}

	public void addSkill(Skills skill)				{	characterSkills.Add(skill);			}
	public void removeSkill(string skill)			{	characterSkills.Remove (characterSkills.Find (e => e.SkillName.CompareTo(skill) == 0));	}
	public Skills getSkill(int slot)				{	return characterSkills[slot];		}
	public List<Skills> getAllSkill(Skills skill)	{	return characterSkills;				}
	public GameObject getGameObject()				{	return characterPrefab;				}
	public void setGameObjectActive(bool state)		{	characterPrefab.SetActive(state);	}

//	public void addPlayerAttack()
//	{
//		//This isn't needed because I have set the player attack already in their prefabs.
//	}

	public void setPlayerPrefab()
	{
		characterPrefab = Resources.Load("characters/" + characterClass) as GameObject;
	}

	public void setWeaponLimits()
	{
		string fileName = "WeaponLimits.txt";
		try
		{
			//			Debug.Log("File Name: " + getFileName ());
			StreamReader textReader = new StreamReader(fileName);
			string input = "";
			
			using(textReader)
			{
				do
				{
					input = textReader.ReadLine();
					if(input != null)
					{
						string[] information = input.Split('=');
						string targetChara = information[0];

						if(targetChara.CompareTo(characterClass) == 0)
							applicableWeapons = information[1].Split(',');
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
//
//	public void addPlayerController()
//	{
//		//This isn't needed because I have set the player controller already in their prefabs.
//	}
}
