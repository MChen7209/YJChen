using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnifiedSuperClass : MonoBehaviour 
{
	//Administration
//	List<bool> characterIsAlive;
	ChangeCharacter changeCharacter;

	int characterLimit;

	//Status Effects
	public GameObject frozenThrone;								//The game object used as the graphic to freeze enemies.

	//Characters
	List<GameObject> characters;								//List of character game objects.
	GameObject wizard;											//The wizard game object.
	GameObject warrior;											//The warrior game object.
	GameObject archer;											//The archer game object.
	
	List<PlayerHealth> healthOfCharacters;			//PlayerHealth copies of each script

	//Weapons
//	GameObject wizardWeapon;
//	GameObject warriorWeapon;
//	GameObject archerWeapon;

	//Skills
//	FireballScript fireBallScript;								//Script reference to the fireballscript
//
//	LightningNode lightningNodeScript;							//Script reference to the lightningNodeScript
//	LightningBolt lightingBoltScript;							//Script reference to LightningBoltScript
//	LightningStrike lightningStrikeScript;						//Script refernece to lightningStrikeScript
//
//	IceBallScript iceBallScript;								//Script reference to iceBallSCript
//	IceBlawkOpaque iceBlawkOpaqueScript;
//	IceRadiusLimit iceRadiusLimitScript;

	// Use this for initialization
	void Awake () 
	{
		characters = new List<GameObject> ();
		//characterIsAlive = new List<bool> ();
		healthOfCharacters = new List<PlayerHealth>();
		changeCharacter = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeCharacter>();
		levelControl (); 
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

		//characters.ForEach(e => characterIsAlive.Add(true));
//		characterIsAlive.ForEach(e => Debug.Log(e));
//		Debug.Log("I AM HERE????: " + characterIsAlive.Count);
//		healthOfCharacters.Add (wizard.GetComponentInChildren<PlayerHealth>());
//		healthOfCharacters.Add (warrior.GetComponentInChildren<PlayerHealth>());
//		healthOfCharacters.Add (archer.GetComponentInChildren<PlayerHealth>());

//		foreach( GameObject element in characters)
//		{
//			string.Concat(whosAlive, element.tag + " "); 
//		}

		characters[1].SetActive(false);
		characters[2].SetActive(false);
	}

	public void saveHealth(GameObject target)
	{
//		healthOfCharacters.ForEach(e => 
//			{ 
//				if(e.CompareTag(target.tag))
//				{
//					e = Instantiate(target.GetComponentInChildren<PlayerHealth>()) as PlayerHealth;
//				}
//			}							
//		);
		healthOfCharacters[getCharacterIndex(target)] = Instantiate(target.GetComponentInChildren<PlayerHealth>()) as PlayerHealth;
	}

	public PlayerHealth loadHealth(GameObject target)
	{
//		healthOfCharacters.ForEach(e => 
//			{ 
//				if(e.CompareTag(target.tag))
//				{
//					return e.GetComponentInChildren<PlayerHealth>();
//				}
//			}							
//		);
//
//		foreach(

		return Instantiate(healthOfCharacters[getCharacterIndex(target)]) as PlayerHealth;
	}

	public void swapCharacterUponDeath(GameObject target)
	{
		if(getAllCharactersAlive () != "")
		{
			Debug.Log ("Game Over");
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

	public List<GameObject> getCharacters()
	{
		return characters;
	}

	public GameObject getCharacter(string character)
	{
		if(character.CompareTo("Wizard") == 0)
			return wizard;
		if(character.CompareTo("Archer") == 0)
			return archer;
		if(character.CompareTo("Warrior") == 0)
			return warrior;

		return null;
//
//		if(characters.ForEach(e => {if( e.CompareTag(character) == 0) )
	}

	public GameObject getCharacter(int character)
	{
		return characters[character];
	}

	public void setAliveWithGodPowers(GameObject character, bool alive)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		//GameObject tempCharacterIndex = characters.IndexOf(character);
		if(alive)
		{
			character.GetComponentInChildren<PlayerHealth>().setAlive(true);
			character.GetComponentInChildren<PlayerHealth>().setHP ("Full");
		}
		else if (!alive)
		{
			//character.GetComponentInChildren<PlayerHealth>().setAlive(false);
			character.GetComponentInChildren<PlayerHealth>().setHP ("None");
		}
	}
//
//	public void setAlive(GameObject character, bool alive)
//	{
//		if(alive)
//		{
//			healthOfCharacters.ForEach( e =>
//			                           {
//				if( e.CompareTag(character.tag) == 0)
//				{
//					e.setAlive(false);
//				}
//				);
//		}
//
//	}

	public void setAllCharacterAlive(string command)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		if(command.CompareTo("AllFull") == 0)
		{
			foreach( GameObject element in characters)
			{
				element.GetComponentInChildren<PlayerHealth>().setAlive(true);
				element.GetComponentInChildren<PlayerHealth>().setHP ("Full");
			}
		}
		else if (command.CompareTo("AllNone") == 0)
		{
			foreach( GameObject element in characters)
			{
				element.GetComponentInChildren<PlayerHealth>().setAlive(false);
				element.GetComponentInChildren<PlayerHealth>().setHP ("None");
			}
		}
	}

	public string getAllCharactersAlive()
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
//		return healthOfCharacters[characterSlot].GetComponentInChildren<PlayerHealth>().isAlive();
//		Debug.Log(characterSlot);
		return healthOfCharacters[characterSlot].isAlive ();
	}

	public bool isAlive(GameObject charac)
	{
		//		return healthOfCharacters[characterSlot].GetComponentInChildren<PlayerHealth>().isAlive();
//		Debug.Log(characterSlot);
		return characters.Find(e => e.tag.Contains (charac.tag));
	}

	public void setCheckPoint()
	{
	}

	public Transform getCheckPoint()
	{
		return null;
	}
}
