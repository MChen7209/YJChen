using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeCharacter : MonoBehaviour 
{
	private UnifiedSuperClass god;
	
	private int currentCharacter = 1;

	private List<Character> characters;
	
	private GameObject current;
	private Animator currentAnim;
	private Vector3 lastSafeLocation;
	
	CameraFollow camera;

	void Start()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass> ();

		characters = god.getCurrentCharacters();
		camera = GetComponent<CameraFollow>();
//		Debug.Log("Characters size " + characters.Count);
		current = characters[0].CharacterGameObject;
		lastSafeLocation = current.transform.position;
	}

	void Update()
	{
//		Debug.Log("Current: " + current.tag);
		if(god.CharacterCount == 0)
			return;

		currentAnim = current.GetComponent<Animator>();

		if(currentAnim.GetBool("Grounded"))
			lastSafeLocation = current.transform.position;

		if (Input.GetKeyDown (KeyCode.F1) && currentCharacter != 1 && god.isAlive (0))
		{
			Debug.Log("Changing character into: Character 0");
			characters[0].CharacterGameObject.transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[0].CharacterGameObject;
			current.SetActive(true);
			camera.SwitchPlayer(current);
			currentCharacter = 1;
		}
		else if(Input.GetKeyDown (KeyCode.F1) && currentCharacter != 1 && !god.isAlive (0))
			Debug.Log("Target Character is not alive.");

		if (Input.GetKeyDown (KeyCode.F2) && currentCharacter != 2 && god.isAlive (1))
		{
			Debug.Log("Changing character into: Character 1");
			characters[1].CharacterGameObject.transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[1].CharacterGameObject;
			current.SetActive(true);
			camera.SwitchPlayer(current);
			currentCharacter = 2;
		}
		else if(Input.GetKeyDown (KeyCode.F2) && currentCharacter != 2 && !god.isAlive (1))
			Debug.Log("Target Character is not alive.");

		if (Input.GetKeyDown (KeyCode.F3) && currentCharacter != 3 && god.isAlive (2))
		{
			Debug.Log("Changing character into: Character 2");
			characters[2].CharacterGameObject.transform.position = current.transform.position;
			current.SetActive( false);
			current = characters[2].CharacterGameObject;
			current.SetActive(true);
			camera.SwitchPlayer(current);
			currentCharacter = 3;
		}
		else if(Input.GetKeyDown (KeyCode.F3) && currentCharacter != 3 && !god.isAlive (2))
			Debug.Log("Target Character is not alive.");
	}

//	public void changeCharacterAfterDeath(Character newCharacter, int newCharacterPositionInArray)
//	{
//		if(newCharacter.HealthController.Alive)
//		{
////			Debug.Log("Moving safe location: " + lastSafeLocation);
//			newCharacter.CharacterGameObject.transform.position = lastSafeLocation;
////			Debug.Log("newCharacter location after safe: " + newCharacter.transform.position);
//			current.SetActive(false);
//			current = newCharacter.CharacterGameObject;
//
//			current.SetActive(true);
//			camera.SwitchPlayer(current);
//			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = true;
//			currentCharacter = newCharacterPositionInArray;
//		}
//	}
	public string test()
	{
		return "Test worked";
	}
	public void changeCharacterAfterDeath(Character newCharacter, int newCharacterPositionInArray)
	{
		if(newCharacter.HealthController.Alive)
		{
			//			Debug.Log("Moving safe location: " + lastSafeLocation);
			newCharacter.CharacterGameObject.transform.position = lastSafeLocation;
			//			Debug.Log("newCharacter location after safe: " + newCharacter.transform.position);
			current.SetActive(false);
			current = newCharacter.CharacterGameObject;
			
			current.SetActive(true);
			camera.SwitchPlayer(current);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = true;
			currentCharacter = newCharacterPositionInArray;
		}
	}
}
