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
	List<Character> unlockedCharacters;

	Character target = null;

	void Start()
	{
		god = GameObject.FindGameObjectWithTag("God").GetComponent<UnifiedSuperClass>();
		currentMenu = "Main";
		gameLoaded = false;
		currentCharacters = god.getCurrentCharacters();
		unlockedCharacters = god.getUnlockedCharacters();
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
	
		foreach(Character element in unlockedCharacters)
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

		foreach(Character element in unlockedCharacters)
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

		if( GUI.Button( new Rect (xPos,yPos,xSize,ySize), "Back"))
		{
			god.updateCharacterList();
			god.Characters.ForEach(e => Debug.Log(e.CharacterName));
			NavGate("Team");
		}
	}

	private void menuTeamSkills()
	{
		int xPos = 10;
		int yPos = 10;
		int xSize = 200;
		int ySize = 50;

		int xPos2 = 400;
		int yPos2 = 10;

		int xPos3 = 610;
		int yPos3 = 10;

		foreach(Character element in unlockedCharacters)
		{
			if(GUI.Button( new Rect( xPos, yPos, xSize, ySize), element.CharacterName + "/" + element.CharacterClass))
			{
				target = element;
			}
			yPos+=60;
		}

		if(!object.ReferenceEquals(target, null))
		{
			foreach(Skills element in god.SkillsController.AllSkillsList.FindAll(e => e.SkillClass == target.CharacterClass))
			{
				if(element.SkillEquipped)
				{
					if(GUI.Button( new Rect(xPos2, yPos2, xSize, ySize), element.SkillName))
					{
						element.SkillEquipped = false;
						target.SkillLimit--;
					}
				}
				else if(!element.SkillEquipped)
				{
					if(GUI.Button( new Rect(xPos3, yPos3, xSize, ySize), element.SkillName))
					{
						if(target.SkillLimit < 3)
						{
							element.SkillEquipped = true;
							target.SkillLimit++;
						}
					}
				}

				yPos3+=60;
				yPos2+=60;
			}

			if(GUI.Button ( new Rect(xPos2, yPos2, xSize, ySize), "Back"))
			{
				target = null;
			}
		}


		
		if( GUI.Button( new Rect (xPos, yPos, xSize, ySize), "Back"))
		{
			god.updateCharacterEquippedSkills();
			currentCharacters[0].SkillsList.ForEach(e => Debug.Log (e.SkillName));
			NavGate("Team");
		}
	}

	private void menuLevelSelect()
	{
		if(GUI.Button (new Rect( 10, 10,200,50), "Prologue"))
		{
			god.levelControl("Prologue");
		}
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

		god.unlockSkill("FireBallSkill");

		god.Characters[0].SkillsList.Add(god.SkillsController.getSkill("FireBallSkill"));
		god.Characters[0].SkillsList[0].SkillEquipped = true;
//		god.addSkill (god.getCharacter("Wizard"), "FireBallSkill");

		currentCharacters.ForEach(e=> { e.Selected = true; god.EquippedCharacterCount++; } );
	}

	private void loadGame()
	{
		gameLoaded = true;
		Debug.Log("Load old game.");
	}
}
