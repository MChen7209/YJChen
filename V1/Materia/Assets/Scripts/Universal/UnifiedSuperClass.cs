using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnifiedSuperClass : MonoBehaviour 
{
	//Administration
	//GameObject[] whosAlive;

	//Status Effects
	public GameObject frozenThrone;

	//Characters
	List<GameObject> characters;
	GameObject wizard;
	GameObject warrior;
	GameObject archer;

	//Weapons
	GameObject wizardWeapon;
	GameObject warriorWeapon;
	GameObject archerWeapon;

	//Skills
	FireballScript fireBallScript;

	LightningNode lightningNodeScript;
	LightningBolt lightingBoltScript;
	LightningStrike lightningStrikeScript;

	IceBallScript iceBallScript;
	IceBlawkOpaque iceBlawkOpaqueScript;
	IceRadiusLimit iceRadiusLimitScript;

	// Use this for initialization
	void Awake () 
	{
		levelControl (); 
		characters = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void levelControl(string[] characters, int[] hp/*, scene information*/)
	{
		Debug.Log ("Not yet implemented");
	}

	public void levelControl()
	{
		//Maybe use hashmap
		//Separate by Character1, Character2, Character3
		//Setup a screen in the menu called, setup team
		//Can do this for weapons and spells as well.
		wizard = GameObject.FindGameObjectWithTag ("Wizard");
		warrior = GameObject.FindGameObjectWithTag ("Warrior");
		archer = GameObject.FindGameObjectWithTag ("Archer");

		characters.Add (wizard);
		characters.Add (warrior);
		characters.Add (archer);
	}

	public List<GameObject> getCharacters()
	{
		return characters;
	}

	public GameObject getCharacter(string character)
	{
		if(character.CompareTo("Wizard"))
			return wizard;
		if(character.CompareTo("Archer"))
			return archer;
		if(character.CompareTo("Warrior"))
			return warrior;
	}

	public GameObject getCharacter(int character)
	{
		return characters[character];
	}

	public void setAlive(string character, bool alive)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		GameObject tempCharacterIndex = characters.IndexOf(character);
		if(alive)
		{
			tempCharacterIndex.GetComponentInChildren<PlayerHealth>().setAlive(true);
			tempCharacterIndex.GetComponentInChildren<PlayerHealth>().setHP ("Full");
		}
		else if (!alive)
		{
			tempCharacterIndex.GetComponentInChildren<PlayerHealth>().setAlive(false);
			tempCharacterIndex.GetComponentInChildren<PlayerHealth>().setHP ("None");
		}
	}

	public void setAllCharacterAlive(string command)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		if(command.CompareTo("AllFull"))
		{
			foreach( GameObject element in characters)
			{
				element.GetComponentInChildren<PlayerHealth>().setAlive(true);
				element.GetComponentInChildren<PlayerHealth>().setHP ("Full");
			}
		}
		else if (command.CompareTo("AllNone"))
		{
			foreach( GameObject element in characters)
			{
				element.GetComponentInChildren<PlayerHealth>().setAlive(false);
				element.GetComponentInChildren<PlayerHealth>().setHP ("None");
			}
		}
	}

	public string getAlive()
	{
		string currentLiving = "";

		foreach(GameObject element in characters)
		{
			if (element.GetComponentInChildren<PlayerHealth> ().isAlive ())
				string.Concat (currentLiving, element.tag + " ");
		}
		return currentLiving;
	}

	public bool isAlive(int characterSlot)
	{
		return characters[characterSlot].GetComponentInChildren<PlayerHealth>().isAlive();
	}

	public void setCheckPoint()
	{
	}

	public Transform getCheckPoint()
	{
	}
}
