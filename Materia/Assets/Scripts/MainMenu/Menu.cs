using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Menu : MonoBehaviour 
{
	private UnifiedSuperClass god;
	public string currentMenu;
	private bool gameLoaded;

	List<Character> currentCharacters;
	List<Character> unequippedCharacters;

	Character[] cCharacters;
	Character[] uCharacters;

	void Start()
	{
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		currentMenu = "Main";
		gameLoaded = false;
		currentCharacters = god.getCurrentCharacters();
		unequippedCharacters = god.getUnlockedCharacters();

//		cCharacters = new Character[god.CharacterLimit];
//		uCharacters = new Character[god.AllCharacterCount];

//		currentCharacters.ForEach(e => {
//			if(unequippedCharacters.Find (f => f.CharacterName.CompareTo(e.CharacterName) == 0)){
//				Debug.Log(unequippedCharacters.Find (f => f.CharacterName.CompareTo(e.CharacterName) == 0));
//				unequippedCharacters.Remove(unequippedCharacters.Find( f => f.CharacterName.CompareTo(e.CharacterName) == 0));
//
//			}
//		}
//		);

//		currentCharacters.ForEach(e=> Debug.Log(e.CharacterName));
//		unequippedCharacters.ForEach(e=>Debug.Log(e.CharacterName));
	}
	void Update()
	{
//		currentCharacters.ForEach(e=> Debug.Log(e.CharacterName));
//		unequippedCharacters.ForEach(e=>Debug.Log(e.CharacterName));
	}

	void OnGUI()
	{
		if(currentMenu == "Main" && !gameLoaded)
			menuMain();
		else if(currentMenu == "Main" && gameLoaded)
			gameMainMenu();

		if(currentMenu == "New Game")
		{
			setNewGame();
			currentMenu = "Main";
		}
		if(currentMenu == "Load Game")
		{
			loadGame();
			currentMenu = "Main";
		}
		if(currentMenu == "Credits")
			menuCredits();

		if(currentMenu == "Options")
			menuOptions();

		if(currentMenu == "Team")
			menuTeamMain();

		if(currentMenu == "Skills")
			menuTeamSkills();

		if(currentMenu == "Weapons")
			menuTeamWeapons();

		if(currentMenu == "TeamSelect")
			menuTeamSelect();

		if(currentMenu == "Level Select")
			menuLevelSelect();
	}

	public void NavGate(string nextMenu)
	{
		currentMenu = nextMenu;
	}

	private void menuMain()
	{
		if( GUI.Button ( new Rect(10, 10, 200, 50), "New Game"))
		{
			//Set new basic stats to characters.
			NavGate("New Game");
		}

		if( GUI.Button ( new Rect(10, 70, 200, 50), "Load Game"))
		{
			NavGate ("Load Game");
		}

		if( GUI.Button ( new Rect(10, 130, 200, 50), "Options"))
		{
			NavGate ("Options");
		}

		if( GUI.Button (new Rect(10,190,200,50), "Credits"))
		{
			NavGate ("Credits");
		}
	}

	private void gameMainMenu()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Level Select"))
		{
			NavGate ("Level Select");
		}

		if( GUI.Button( new Rect (10,70,200,50), "Save"))
		{
			Debug.Log("Save the game");
		}

		if( GUI.Button( new Rect (10,130,200,50), "Load level"))
		{

		}

		if( GUI.Button( new Rect (10,190,200,50), "Team"))
		{
			NavGate ("Team");
		}

		if( GUI.Button ( new Rect(10,250,200,50), "Options"))
		{
			NavGate ("Options");
		}

		if( GUI.Button (new Rect (10,310,200,50), "Credits"))
		{
			NavGate ("Credits");
		}
	}

	private void menuOptions()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Back"))
		{
			NavGate("Main");
		}
	}

	private void menuCredits()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Back"))
		{
			NavGate("Main");
		}
	}

	private void menuTeamMain()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Configure Team"))
		{
			NavGate ("TeamSelect");
		}

		if( GUI.Button( new Rect (10,70,200,50), "Skills"))
		{
			NavGate ("Skills");
		}

		if( GUI.Button( new Rect (10,130,200,50), "Weapons"))
		{
			NavGate ("Weapons");
		}

		if( GUI.Button( new Rect (10,190,200,50), "Back"))
		{
			NavGate("Main");
		}
	}

	private void menuTeamSelect()
	{
		int xPos = 10;
		int yPos = 10;
		int xSize = 200;
		int ySize = 50;

		int xPos2 = 400;
		int yPos2 = 10;

//		Debug.Log (currentCharacters.Count);
//		for(int i=0; i < currentCharacters.Count; i++)
//		{
//			if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), currentCharacters[i].CharacterName))
//			{
//				unequippedCharacters.Add (currentCharacters[i]);
//				god.removeCharacter(currentCharacters[i]);
//			}
//			yPos+=60;
//		}

//		foreach(Character element in currentCharacters)
//		{
//			if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), element.CharacterName))
//			{
//				unequippedCharacters.Add (element);
//				god.removeCharacter(element);
//			}
//			yPos+=60;
//		}
//		Debug.Log( "cCharLen: " + cCharacters.Length);
//		Debug.Log( "uCharLen: " + uCharacters.Length);

		foreach(Character element in unequippedCharacters)
		{
			if(element.Selected)
			{
				if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), element.CharacterName))
				{
					element.Selected = false;
					god.EquippedCharacterCount--;
				}
			}
			else
			{
				if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), "Empty Slot"))
				{
				}
			}
			yPos+=60;
		}

		foreach(Character element in unequippedCharacters)
		{
			if(!element.Selected)
			{
				if(GUI.Button( new Rect(xPos2,yPos2,xSize,ySize), element.CharacterName))
				{
					if(god.EquippedCharacterCount < 3)
					{
						element.Selected = true;
						god.EquippedCharacterCount++;
					}
				}
			}
			else
			{
				if(GUI.Button( new Rect(xPos2,yPos2,xSize,ySize), ""))
				{
				}
			}
			yPos2+=60;
		}
//		for (int i=0; i<cCharacters.Length; i++)
//		{
//			if(!object.ReferenceEquals(cCharacters[i],null))
//			{
//				if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), cCharacters[i].CharacterName))
//				{
//					Debug.Log("cSize: " + uCharacters.Length);
//					for(int j=0; j<uCharacters.Length; j++)
//					{
//						Debug.Log("Reference: " + object.ReferenceEquals(uCharacters[j],null));
//						if(object.ReferenceEquals(uCharacters[j],null))
//						{
//							Debug.Log("Flaggin");
//							uCharacters[j] = cCharacters[i];
//							cCharacters[i] = null;
//						}
//					}
//				}
//			}
//			else
//			{
//				if(GUI.Button( new Rect(xPos,yPos,xSize,ySize), "Empty Slot"))
//				{
//				}
//			}
//			yPos+=60;
//		}
//
//		for (int i=0; i<uCharacters.Length; i++)
//		{
//			if(!object.ReferenceEquals(uCharacters[i],null))
//			{
//				if(GUI.Button( new Rect(xPos2,yPos2,xSize,ySize), uCharacters[i].CharacterName))
//				{
//					for(int j=0; j<cCharacters.Length; j++)
//					{
//						if(object.ReferenceEquals(cCharacters[j],null))
//						{
//							Debug.Log("Baggin");
//							cCharacters[j] = uCharacters[i];
//							uCharacters[i] = null;
//						}
//					}
//				}
//			}
//			else
//			{
//				if(GUI.Button( new Rect(xPos2,yPos2,xSize,ySize), ""))
//				{
//				}
//			}
//			yPos2+=60;
//		}

//		for(int i=0; i < unequippedCharacters.Count; i++)
//		{
//			if(GUI.Button( new Rect(xPos2+400,yPos2,xSize,ySize), unequippedCharacters[i].CharacterName))
//			{
//				god.addCharacter(unequippedCharacters[i]);
//				unequippedCharacters.Remove (unequippedCharacters[i]);
//			}
//			yPos2+=60;
//		}

//		foreach(Character element in unequippedCharacters)
//		{
//			if(GUI.Button( new Rect(xPos2+400,yPos2,xSize,ySize), element.CharacterName))
//			{
//				god.addCharacter(element);
//				unequippedCharacters.Remove(element);
//			}
//			yPos2+=60;
//		}

//		if( currentCharacters.Count < god.CharacterLimit)
//		{
//			for(int i=currentCharacters.Count; i<god.CharacterLimit; i++)
//			{
//				if(GUI.Button( new Rect (xPos, yPos, xSize, ySize), "Empty Slot"))
//				{
//
//				}
//				yPos+=60;
//			}
//		}

		if( GUI.Button( new Rect (xPos,yPos,xSize,ySize), "Back"))
		{
			NavGate("Team");
		}

//		foreach(charac
	}

	private void menuTeamSkills()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Add Skill"))
		{
			
		}
		
		if( GUI.Button( new Rect (10,70,200,50), "Remove Skill"))
		{
			
		}
		
		if( GUI.Button( new Rect (10,130,200,50), "Back"))
		{
			NavGate("Team");
		}
	}

	private void menuLevelSelect()
	{
		if( GUI.Button( new Rect (10,130,200,50), "Back"))
		{
			NavGate("Main");
		}
	}

	private void menuTeamWeapons()
	{
		if( GUI.Button( new Rect (10,10,200,50), "Add Weapon"))
		{
			
		}
		
		if( GUI.Button( new Rect (10,70,200,50), "Remove Weapon"))
		{
			
		}
		
		if( GUI.Button( new Rect (10,130,200,50), "Back"))
		{
			NavGate("Team");
		}
	}

	private void loadLevel()
	{
		Debug.Log("Load a level");
	}

	private void setNewGame()
	{
		//set values needed for new game.
		gameLoaded = true;
		Debug.Log("Set new game.");

		god.addCharacter("Wizard");
		god.addCharacter("Warrior");
		god.addCharacter("Archer");

		god.unlockCharacter("Wizard");
		god.unlockCharacter("Warrior");
		god.unlockCharacter("Archer");

		currentCharacters.ForEach(e=> { e.Selected = true; god.EquippedCharacterCount++; } );
//		god.EquippedCharacterCount--;
		
		//		for(int i=0; i<cCharacters.Length; i++)
//			cCharacters[i] = god.getCharacterFromSlot(i);
//
//		for(int i=0; i<uCharacters.Length; i++)
//			uCharacters[i] = god.getUnlockedCharacters().ToArray;
//		cCharacters = god.getCurrentCharacters().ToArray();
//		uCharacters = god.getUnlockedCharacters().ToArray();

		//		uCharacters = uCharacters - cCharacters;
//			
//		foreach(Character element in uCharacters)
//		{
//			foreach(Character element0 in cCharacters)
//			{
//				if(element == element0)
//					result[result.Length] = null;
//			}
//		}

//		for(int i=0; i < uCharacters.Length; i++)
//		{
//			for(int j=0; j<cCharacters.Length; j++)
//			{
//				if(uCharacters[i].CharacterName == cCharacters[j].CharacterName)
//					uCharacters[i] = null;
//			}
//		}
	}

	private void loadGame()
	{
		gameLoaded = true;
		Debug.Log("Load old game.");
	}

//	private bool hasValue(int slot, Character[] array)
//	{
//		Character?[] testArray = array;
//
//		return (testArray[slot].HasValue);
//	}



}
