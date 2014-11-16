using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnifiedSuperClass : MonoBehaviour 
{
	//Administration
	ChangeCharacter changeCharacter;

	int characterLimit;

	//Status Effects
	public GameObject frozenThrone;								//The game object used as the graphic to freeze enemies.

	//Characters
	List<GameObject> characters;								//List of character game objects.
	GameObject wizard;											//The wizard game object.
	GameObject warrior;											//The warrior game object.
	GameObject archer;											//The archer game object.
	
	List<PlayerHealthController> healthOfCharacters;			//PlayerHealthController copies of each script

	//Weapons
	List<Weapon> characterWeapons;

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
		healthOfCharacters = new List<PlayerHealthController>();
		characterWeapons = new List<Weapon>();
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

		characters.Add (archer);
		characters.Add (warrior);
		characters.Add (wizard);

		characterWeapons.Add (gameObject.AddComponent<WoodenBow>());
		characterWeapons.Add (gameObject.AddComponent<WoodenSword>());
		characterWeapons.Add (gameObject.AddComponent<WoodenStaff>());

		loadWeapons();

		characters.ForEach (e => healthOfCharacters.Add (gameObject.AddComponent<PlayerHealthController>()));
		for(int i=0; i<characters.Count; i++)
		{
			healthOfCharacters[i].connectToScript(characters[i]);
		}

//		healthOfCharacters.ForEach(e => 
//		{
//			Debug.Log("Current Character: " + healthOfCharacters.IndexOf(e));
//			Debug.Log("MaxHP" + e.getMaxHp());
//			Debug.Log("Armor" + e.getArmor());
//		}
//		);

		characters[1].SetActive(false);
		characters[2].SetActive(false);
	}

//	public void saveHealth(GameObject target)
//	{
////		healthOfCharacters.ForEach(e => 
////			{ 
////				if(e.CompareTag(target.tag))
////				{
////					e = Instantiate(target.GetComponentInChildren<PlayerHealth>()) as PlayerHealth;
////				}
////			}							
//////		);
////
////		healthOfCharacters[getCharacterIndex(target)] = target.GetComponentInChildren<PlayerHealth>();
////
////		PlayerHealth targetScript = target.GetComponentInChildren<PlayerHealth>();
////		int targetIndex = getCharacterIndex (target);
////
////		healthOfCharacters[targetIndex].
//
//		healthOfCharacters[characters.IndexOf (target)].s
//	}

//	public PlayerHealth loadHealth(GameObject target)
//	{
////		healthOfCharacters.ForEach(e => 
////			{ 
////				if(e.CompareTag(target.tag))
////				{
////					return e.GetComponentInChildren<PlayerHealth>();
////				}
////			}							
////		);
////
////		foreach(
//
//		return Instantiate(healthOfCharacters[getCharacterIndex(target)]) as PlayerHealth;
//	}

	public void loadWeapons()
	{

	}

	public void swapCharacterUponDeath(GameObject target)
	{
//		Debug.Log("getAllCharactersAlive: " + getAllCharactersAlive());
		if(getAllCharactersAlive () == "")
		{
//			Debug.Log ("Game Over");
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
//		Debug.Log("Character in getCharacter: " + character);
//		foreach(GameObject element in characters)
//		{
//			Debug.Log("Character index: " + characters.IndexOf (element));
//		}
		return characters[character];
	}

	public void setAliveWithGodPowers(GameObject character, bool alive)
	{
		//set alive the character in string character
		//If it is set to all, resurrect all characters that are not alive.
		//GameObject tempCharacterIndex = characters.IndexOf(character);
		if(alive)
		{
			PlayerHealthController tempHealthController = getPlayerHealthController(character);
			tempHealthController.setAlive(true);
			tempHealthController.setHP (tempHealthController.getMaxHp());
		}
		else if (!alive)
		{
//			Debug.Log("Killing character: " + character.tag);
			PlayerHealthController tempHealthController = getPlayerHealthController(character);
			tempHealthController.setAlive(false);
			tempHealthController.setHP (0);
		}
	}

	public PlayerHealthController getPlayerHealthController(GameObject target)
	{
//		Debug.Log("getPlayerHealthController: " + target.tag);
		return target.GetComponentInChildren<PlayerHealth>().getConnection();
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
//			foreach( GameObject element in characters)
//			{
////				element.GetComponentInChildren<PlayerHealth>().setAlive(true);
////				element.GetComponentInChildren<PlayerHealth>().setHP ("Full");
//			}

			healthOfCharacters.ForEach(e => { e.setAlive(true); e.setHP(e.getMaxHp()); } );
		}
		else if (command.CompareTo("AllNone") == 0)
		{
//			foreach( GameObject element in characters)
//			{
//				element.GetComponentInChildren<PlayerHealth>().setAlive(false);
//				element.GetComponentInChildren<PlayerHealth>().setHP ("None");
//			}
			healthOfCharacters.ForEach(e => { e.setAlive(false); e.setHP(0); } );
		}
	}

	public string getAllCharactersAlive()
	{
		string currentLiving = "";

//		foreach(GameObject element in characters)
//		{
//			if(healthOfCharacters[characters.IndexOf(element)].isAlive())
//				string.Concat (currentLiving, element.tag + " ");
//		}

		for(int i=0; i<healthOfCharacters.Count; i++)
		{
			if(healthOfCharacters[i].isAlive ())
			{
//				Debug.Log("Character: " + characters[i].tag + " isAlive: " + healthOfCharacters[i].isAlive());
//				string.Concat (currentLiving, characters[i].tag + " ");
				currentLiving += characters[i].tag + " ";
//				Debug.Log("CurrentLiving: " + currentLiving);
			}
		}
		return currentLiving;
	}

	public bool isAlive(int characterSlot)
	{
//		return healthOfCharacters[characterSlot].GetComponentInChildren<PlayerHealth>().isAlive();
//		Debug.Log(characterSlot);
//		Debug.Log(healthOfCharacters.Count);
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

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}
}
