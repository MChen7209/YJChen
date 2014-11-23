using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Character : MonoBehaviour 
{
	//Character administiration
	PlayerHealthController healthController;
	bool selected;

	//Character Descriptions
	string _characterName;
	string _characterClass;
	string _characterDescription;
	GameObject charaGameObject;
	GameObject characterPrefab;

	//Character Weapon
	Weapon _characterWeapon;
	string[] applicableWeapons;

	//Character skill
	List<Skills> characterSkills;


	public Character(string name, string type, string description, float health, float armor)
	{

		_characterName = name;
		_characterClass = type;
		_characterDescription = description;
		selected = false;
		healthController = new PlayerHealthController(health, armor);
		setPlayerPrefab();
		characterSkills = new List<Skills>();
		_characterWeapon = new Weapon();
		//Debug.Log (CharacterName + " " + CharacterClass + " " + CharacterDescription + " " + HealthController.HP + " " + HealthController.Armor);
//		Debug.Log (_characterName + " " + _characterClass + " " + _characterDescription + " " + healthController.HP + " " + healthController.Armor);
	}

	public Weapon CharacterWeapon
	{
		get	{ 	return _characterWeapon; 	}
		set { 	_characterWeapon = value;	}
	}

	public string CharacterName
	{
		get	{ 	return _characterName; 	}
		set { 	_characterName = value;	}
	}

	public string CharacterClass
	{
		get	{ 	return _characterClass; 	}
		set { 	_characterClass = value;	}
	}

	public string CharacterDescription
	{
		get	{ 	return _characterDescription; 	}
		set { 	_characterDescription = value;	}
	}

	public GameObject CharacterGameObject
	{
		get	{	return charaGameObject;	}
	}

	public bool Selected
	{
		get	{	return selected;	}
		set	{	selected = value;	}
	}

	public PlayerHealthController HealthController
	{
		get	{	return healthController;	}
	}

	public List<Skills> SkillsList
	{
		get	{	return characterSkills;	}
	}

	public void addSkill(Skills skill)				{	characterSkills.Add(skill);	skill.SkillOwner = CharacterGameObject;}
	public void removeSkill(string skill)			{	characterSkills.Remove (characterSkills.Find (e => e.SkillName.CompareTo(skill) == 0));	}
	public Skills getSkill(int slot)				{	return characterSkills[slot];		}
	public List<Skills> getAllSkill(Skills skill)	{	return characterSkills;				}
	public GameObject getGameObject()				{	return characterPrefab;				}
	public void setGameObjectActive(bool state)		{	characterPrefab.SetActive(state);	}

	public void setPlayerPrefab()
	{
		characterPrefab = Resources.Load("prefabs/characters/" + _characterClass) as GameObject;
	}

	public void activatePlayerGameObject(Vector3 location)
	{
		charaGameObject = Instantiate(characterPrefab, location, Quaternion.identity) as GameObject;
	}

	public void setWeaponLimits()
	{
		string fileName = "WeaponLimits.txt";
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
						string[] information = input.Split('=');
						string targetChara = information[0];

						if(targetChara.CompareTo(_characterClass) == 0)
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
}
