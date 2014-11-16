using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	private Transform Health;
	private UnifiedSuperClass god;
	void Start()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Wizard" || col.gameObject.tag == "Warrior" || col.gameObject.tag == "Archer" )
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//col.GetComponentInChildren<FollowPlayer>().enabled = false;
			//GameObject.FindGameObjectWithTag(col.gameObject.tag).GetComponentInChildren<FollowPlayer>().enabled = false;
			//Find health game object and stop it from going

			// ... destroy the player.
			//Destroy (col.gameObject);
			// ... reload the level.

			god.setAliveWithGodPowers(col.gameObject, false);
//			StartCoroutine("ReloadGame");
		}
		else
		{
			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}
}
