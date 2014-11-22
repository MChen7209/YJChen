using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class UnifiedSuperClass : MonoBehaviour 
{
	//Administration
	ChangeCharacter changeCharacter;
	WeaponController weaponList;
	SkillsController skillsController;

	int characterLimit;
	int skillLimit;

	//Status Effects
	public GameObject frozenThrone;								//The game object used as the graphic to freeze enemies.

	//Characters
	List<Character> characters;									//List of the overall character controller.
	List<Character> allCharacters;
//	GameObject wizard;											//The wizard game object.
//	GameObject warrior;											//The warrior game object.
//	GameObject archer;											//The archer game object.
	
//	List<PlayerHealthController> healthOfCharacters;			//PlayerHealthController copies of each script

	//Weapons
	List<Weapon> characterWeapons;
	List<Weapon> weaponInventory;

	//Skills
	Dictionary<string, List<Skills>> characterSkills;


	// Use this for initialization
	public UnifiedSuperClass()
	{
		skillsController = new SkillsController();
		allCharacters = new List<Character>();
		characterLimit = 3;
		skillLimit = 3;
	}

	void Awake () 
	{

		characters = new List<Character> ();
//		healthOfCharacters = new List<PlayerHealthController>();
		characterWeapons = new List<Weapon>();
		characterSkills = new Dictionary<string, List<Skills>>();
		changeCharacter = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeCharacter>();
		loadAdmin();
		levelControl (); 
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
	public void levelControl()
	{
		//Maybe use hashmap
		//Separate by Character1, Character2, Character3
		//Setup a screen in the menu called, setup team
		//Can do this for weapons and spells as well.
//		wizard = GameObject.FindGameObjectWithTag ("Wizard");
//		warrior = GameObject.FindGameObjectWithTag ("Warrior");
//		archer = GameObject.FindGameObjectWithTag ("Archer");
//
//		characters.Add (archer);
//		characters.Add (warrior);
//		characters.Add (wizard);
		addCharacter ("Wizard");
		addCharacter ("Archer");
		addCharacter ("Warrior");


//		characters.ForEach (e => healthOfCharacters.Add (gameObject.AddComponent<PlayerHealthController>()));
//		for(int i=0; i<characters.Count; i++)
//		{
//			healthOfCharacters[i].connectToScript(characters[i]);
//		}

		characters.ForEach( e => e.HealthController.connectToScript(e));
//		loadSkills (characters.Find(e => e.CompareTag("Wizard")), "FireBallSkill");

		characters.ForEach(e => {	if(characters.IndexOf(e) == 0) 
										e.setGameObjectActive(true);
									else
										e.setGameObjectActive(false); } ); 
//
//		characters[1].setGameObjectActive(false);
//		characters[2].setGameObjectActive(false);
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
						allCharacters.Add ( new Character(information[0], information[1], information[2]));
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
		weaponList = new WeaponController();
		weaponList.initialize(fileName);
		
	}
	
	public void loadSkillList(string fileName)
	{
		skillsController = new SkillsController();
		skillsController.initialize(fileName);
	}

	public void addCharacter(Character target)
	{
		if(characters.Count < characterLimit) 
			characters.Add (target);
	}
	
	public void removeCharacter(Character target)
	{
		//		if(target != null)
		characters.Remove (target);
	}

	public void addCharacter(string loadCharacter)
	{
		characters.Add (allCharacters.Find (e => e.CharacterName.CompareTo(loadCharacter) == 0));
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
//		if(skill != null)
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
			if(characters[i].gameObject == target)
			{
				return i;
			}
		}

		return -1;
	}

	public List<Character> getAllCharacters()
	{
		return characters;
	}

	public Character getCharacter(string characterClass)
	{
		return characters.Find (c => c.CharacterClass.CompareTo(characterClass) == 0);
	}

	public Character getCharacterFromSlot(int character)
	{
		return characters[character];
	}

	public void setAliveWithGodPowers(GameObject character, bool alive)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
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
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		if(command.CompareTo("Full") == 0)
			characters.ForEach(e => { e.HealthController.Alive = true; e.HealthController.HP = e.HealthController.MaxHP; } );
		else if (command.CompareTo("None") == 0)
			characters.ForEach(e => { e.HealthController.Alive = false; e.HealthController.HP = 0f; } );
	}

	public List<Character> getAllCharactersAlive()
	{
		List<Character> currentLiving = new List<Character>();
//		string currentLiving = "";
//		put health controllers in character
//		for(int i=0; i<characters.Count; i++)
//		{
//			if(characters[i].HealthController.Alive)
//				currentLiving.Add(character[i
//		}

		characters.ForEach( e => { if(e.HealthController.Alive) currentLiving.Add (e); });
		return currentLiving;
	}

//	public Character getAllCharactersAlive()
//	{
//		List<Character> temp = new List<Character>();
//
//		for(int i=0; i<healthOfCharacters.Count; i++)
//		{
//
//		}
//	}


	public bool isAlive(int characterSlot)
	{
		return characters[characterSlot].HealthController.Alive;
	}

	public bool isAlive(GameObject charac)
	{
		return characters.Find(e => e.tag.Contains (charac.tag));
	}

	public void setCheckPoint()
	{
	}

	public Transform getCheckPoint()
	{
		return null;
	}

	public SkillsController getSkillsController()
	{
		return skillsController;
	}

	public void equipWeapon(Character targetCharacter, Weapon targetWeapon)
	{
//		if(targetCharacter.CharacterClass == targetWeapon.
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}
}
