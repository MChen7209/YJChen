using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class UnifiedSuperClass : MonoBehaviour 
{
	//Administration
	ChangeCharacter changeCharacter;
	WeaponController weaponList;
	SkillsController skillsController;
	int equippedCharactersCount;

	int characterLimit;
	int skillLimit;

	//Status Effects
	public GameObject frozenThrone;								//The game object used as the graphic to freeze enemies.

	//Characters
	List<Character> characters;									//List of the overall character controller.
	List<Character> unlockedCharacters;						//List of all unlocked Characters
	List<Character> allCharacters;								//List of all characters in the game.

	//Weapons
	List<Weapon> weaponInventory;

	public int CharacterCount
	{
		get	{	return characters.Count;	}
	}

	public int UnlockedCharacterCount
	{
		get	{	return unlockedCharacters.Count;	}
	}

	public int AllCharacterCount
	{
		get	{	return allCharacters.Count;	}
	}

	public List<Character> Characters
	{
		get {	return characters;	}
	}

	public int CharacterLimit
	{
		get	{	return characterLimit;	}
		set	{	characterLimit = value;	}
	}

	public int EquippedCharacterCount
	{
		get	{	return equippedCharactersCount;		}
		set	{	equippedCharactersCount = value;	}
	}

	public int SkillLimit
	{
		get	{	return skillLimit;	}
		set	{	skillLimit = value;	}
	}

	public SkillsController SkillsController
	{
		get	{	return skillsController;	}
	}
	void Awake () 
	{
		DontDestroyOnLoad(gameObject);
		skillsController = new SkillsController();
		allCharacters = new List<Character>();
		unlockedCharacters = new List<Character>();
		characters = new List<Character> ();
		weaponList = new WeaponController();
		skillsController = new SkillsController();
		changeCharacter = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeCharacter>();
		characterLimit = 3;
		skillLimit = 3;
		equippedCharactersCount = 0;
		loadAdmin();
//		levelControl (); 
	}

	void Update()
	{
//		Debug.Log(characters[0].CharacterName);
//		Debug.Log(characters[0].CharacterGameObject.tag);
	}

	public void levelControl(string[] characters, int[] hp/*, scene information*/)
	{
		Debug.Log ("Not yet implemented");
	}

	public void loadAdmin()
	{
		loadWeaponList("WeaponList.txt");
		loadSkillList("SkillList.txt");
		loadAllCharacters("characters/characters.txt");
	}
	public void levelControl(string level)
	{
		characters.ForEach(e => e.activatePlayerGameObject(new Vector3(0,0,0)));
		characters.ForEach( e=>	{
			e.CharacterGameObject.GetComponentInChildren<AttackController>().loadSkills(e.SkillsList);
		});

		characters.ForEach(e => DontDestroyOnLoad(e.CharacterGameObject));
		Application.LoadLevel(level);
//		characters.ForEach(e => e.SkillsList.ForEach( c => c.SkillOwner = e.CharacterGameObject));
		characters.ForEach( e => e.HealthController.connectToScript(e));
		characters.ForEach(e => e.CharacterGameObject.transform.position =  new Vector3(44,21,0)); //GameObject.FindGameObjectWithTag("StartLocation").transform.position);

		characters.ForEach(e => { if(characters.IndexOf(e) != 0) e.setGameObjectActive(false); });

		characters.ForEach( e=> Debug.Log(e.CharacterName + ": " + characters.IndexOf(e)));


		for(int i=0; i<characters.Count; i++)
		{
			if(i !=0)
				characters[i].setGameObjectActive(false);
		}
	}

	public void loadAllCharacters(string fileName)
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
						string[] information = input.Split(',');
//						Debug.Log (information[0] + " " + information[1] + " " + information[2] + " " + information[3] + " " + information[4]);
						object newCharacter = System.Activator.CreateInstance(Type.GetType(information[1]), information);
						allCharacters.Add ((Character)newCharacter);

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

	public void loadWeaponList(string fileName)
	{
		weaponList.initialize(fileName);
	}
	
	public void loadSkillList(string fileName)
	{
		skillsController.initialize(fileName);
	}

	public void unlockCharacter(string targetClass)
	{
		unlockCharacter (allCharacters.Find(e => e.CharacterClass.CompareTo(targetClass) == 0) as Character);
	}

	public void unlockCharacter(Character target)
	{
		unlockedCharacters.Add (target);
	}

	public void unlockSkill(string skill)
	{
		skillsController.unlockSkill(skill);
	}

	public void addCharacter(Character target)
	{
		if(characters.Count < characterLimit) 
			characters.Add (target);
	}
	
	public void removeCharacter(Character target)
	{
		characters.Remove (target);
	}

	public void updateCharacterList()
	{
		characters = new List<Character>();

		unlockedCharacters.ForEach(e => {
			if(e.Selected)
				characters.Add(e);
		});
	}

	public void updateCharacterEquippedSkills()
	{
		List<Skills> tempSkillList = new List<Skills>();

		unlockedCharacters.ForEach(e => {
				getSkillsbyClass(e.CharacterClass).ForEach( s => {
					if(s.SkillEquipped)
						tempSkillList.Add(s);
				});
			e.SkillsList = tempSkillList;
			e.SkillsList.ForEach(s => s.SkillSlot = e.SkillsList.IndexOf(s));
			tempSkillList = new List<Skills>();
		});
	}

	public void addCharacter(string loadCharacter)
	{
		characters.Add (allCharacters.Find (e => e.CharacterClass.CompareTo(loadCharacter) == 0));
	}

	public void removeCharacter(string removeCharacter)
	{
		characters.Remove(characters.Find (e => e.CharacterClass.CompareTo(removeCharacter) == 0));
	}

	public void addWeapon(Character target, string weapon)
	{
		target.CharacterWeapon = weaponList.getWeapon(weapon);
		weaponInventory.Remove(target.CharacterWeapon);
	}

	public void removeWeapon(Character target, string weapon)
	{
		weaponInventory.Add(target.CharacterWeapon);
		target.CharacterWeapon = new Weapon();
	}

	public void replaceWeapon(Character target, string weapon)
	{
		weaponInventory.Add(target.CharacterWeapon);
		target.CharacterWeapon = weaponList.getWeapon(weapon);
	}

	public void addSkill(Character target, string skill)
	{
		if(target.SkillsList.Count < skillLimit)
			target.addSkill(skillsController.getSkill(skill));
	}

	public void removeSkill(Character target, string skill)
	{
			target.removeSkill(skill);
	}

	public void swapCharacterUponDeath(GameObject target)
	{
		if(getAllCharactersAlive().Count == 0)
		{
			Debug.Log ("Game Over");
			StartCoroutine("ReloadGame");
		}
		else
		{
			int whoDied = getCharacterIndex(target);
			if(--whoDied < 0)
				whoDied = characters.Count - 1;
			changeCharacter.changeCharacterAfterDeath(characters[whoDied], whoDied);
		}
	}

	int getCharacterIndex(GameObject target)
	{
		for(int i=0; i<characters.Count; i++)
		{
			if(characters[i].CharacterGameObject == target)
			{
				return i;
			}
		}
		return -1;
	}

	public List<Character> getCurrentCharacters()
	{
		return characters;
	}

	public List<Character> getUnlockedCharacters()
	{
		return unlockedCharacters;
	}

	public Character getCharacter(string characterClass)
	{
		return characters.Find (c => c.CharacterClass.CompareTo(characterClass) == 0);
	}

	public Character getCharacterFromSlot(int character)
	{
		if(CharacterCount > 0)
			return characters[character];
		else
			return null;
	}

	public void setAliveWithGodPowers(GameObject character, bool alive)
	{
		if(alive)
		{
			PlayerHealthController tempHealthController = getPlayerHealthController(character);
			tempHealthController.Alive = true;
			tempHealthController.HP = tempHealthController.MaxHP;
		}
		else if (!alive)
		{
			PlayerHealthController tempHealthController = getPlayerHealthController(character);
			tempHealthController.Alive = false;
			tempHealthController.HP = 0f;
		}
	}

	public PlayerHealthController getPlayerHealthController(GameObject target)
	{
		return target.GetComponentInChildren<PlayerHealth>().getConnection();
	}

	public void setAllCharacterAlive(string command)
	{
		if(command.CompareTo("Full") == 0)
			characters.ForEach(e => { e.HealthController.Alive = true; e.HealthController.HP = e.HealthController.MaxHP; } );
		else if (command.CompareTo("None") == 0)
			characters.ForEach(e => { e.HealthController.Alive = false; e.HealthController.HP = 0f; } );
	}

	public List<Character> getAllCharactersAlive()
	{
		List<Character> currentLiving = new List<Character>();

		characters.ForEach( e => { if(e.HealthController.Alive) currentLiving.Add (e); });
		return currentLiving;
	}


	public bool isAlive(int characterSlot)
	{
		return characters[characterSlot].HealthController.Alive;
	}

	public bool isAlive(GameObject charac)
	{
		return characters.Find(e => e.CharacterGameObject.tag.Contains (charac.tag));
	}

	public void setCheckPoint()
	{
	}

	public Transform getCheckPoint()
	{
		return null;
	}



	public List<Skills> getSkillsbyClass(string characterClass)	{	return skillsController.AllSkillsList.FindAll(e => e.SkillClass.CompareTo(characterClass) == 0); }

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}
}
