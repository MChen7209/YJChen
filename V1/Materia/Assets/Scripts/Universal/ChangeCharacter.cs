using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeCharacter : MonoBehaviour 
{
	private UnifiedSuperClass god;
	
	private int currentCharacter = 1;

	private List<GameObject> characters;
//	private GameObject wizard;
//	private GameObject warrior;
//	private GameObject archer;
	
	private GameObject current;
	private Animator currentAnim;
	private Transform lastSafeLocation;

	private CameraFollow camera;

	void Awake()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass> ();

		characters = god.getCharacters();
		camera = GetComponent<CameraFollow>();

		current = characters[1];

		characters[1].SetActive(false);
		characters[2].SetActive(false);

//		wizard = god
//		warrior = GameObject.FindGameObjectWithTag ("Warrior");
//		archer = GameObject.FindGameObjectWithTag ("Archer");
//
//		current = GameObject.FindGameObjectWithTag ("Wizard");
//		camera = GetComponent<CameraFollow>();
//
//		archer.SetActive (false);
//		warrior.SetActive (false);
	}

	void Update()
	{
//		if (Input.GetKeyDown (KeyCode.F1) && currentCharacter != 1)
//		{
//			wizard.transform.position = current.transform.position;
//			current.SetActive (false);
//			current = wizard;
//
//			wizard.SetActive (true);
//			camera.SwitchPlayer("Wizard");
//			currentCharacter = 1;
//		}
//		if (Input.GetKeyDown (KeyCode.F2) && currentCharacter != 2)
//		{
//			warrior.transform.position = current.transform.position;
//			current.SetActive (false);
//			current = warrior;
//
//			warrior.SetActive (true);
//			camera.SwitchPlayer("Warrior");
//			currentCharacter = 2;
//		}
//		if (Input.GetKeyDown (KeyCode.F3) && currentCharacter != 3)
//		{
//			archer.transform.position = current.transform.position;
//			current.SetActive (false);
//			current = archer;
//
//			archer.SetActive (true);
//			camera.SwitchPlayer ("Archer");
//			currentCharacter = 3;
//		}
		currentAnim = current.GetComponent<Animator>();

		if(currentAnim.GetBool("Grounded"))
			lastSafeLocation = transform;

		if (Input.GetKeyDown (KeyCode.F1) && currentCharacter != 1 && god.isAlive (1))
		{
			characters[0].transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[0];

			current.SetActive(true);
			camera.SwitchPlayer(current.tag);
			currentCharacter = 1;
		}
		if (Input.GetKeyDown (KeyCode.F2) && currentCharacter != 2 && god.isAlive (2))
		{
			characters[1].transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[1];
			
			current.SetActive(true);
			camera.SwitchPlayer(current.tag);
			currentCharacter = 2;
		}
		if (Input.GetKeyDown (KeyCode.F3) && currentCharacter != 3 && god.isAlive (3))
		{
			characters[2].transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[2];
			
			current.SetActive(true);
			camera.SwitchPlayer(current.tag);
			currentCharacter = 3;
		}
	}

	public void setWhosAlive()
	{
		//Lock whos not alive.

	}

	public void changeCharacterCauseDeath(int whoDied, string whatPosition)
	{
		if(--whoDied < 0)
			whoDied = 2;

		if(god.isAlive (whoDied))
		{
			characters[whoDied].transform.position = lastSafeLocation.position;
			current.SetActive(false);
			current = characters[whoDied];
			
			current.SetActive(true);
			camera.SwitchPlayer(current.tag);
			currentCharacter = whoDied;
		}
	}
}
