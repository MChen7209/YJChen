using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private UnifiedSuperClass god;

	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	private Transform currentPlayer;		// Reference to the player's transform.

	void Start ()
	{
		// Setting up the reference.
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass> ();
//		currentPlayer = god.getCharacterFromSlot (0).transform;
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - currentPlayer.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - currentPlayer.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		Debug.Log("Inside camera follow fixed update: " + god.CharacterCount);
		if( (god.CharacterCount > 0) && god.isAlive (currentPlayer.gameObject))
			TrackPlayer ();
	}

	public void SwitchPlayer(GameObject newPlayer)
	{
		Debug.Log("Current Player: " + currentPlayer.tag);
		Debug.Log ("New Player: " + newPlayer.tag);
		currentPlayer = newPlayer.transform;

		Debug.Log("Current Player after: " + currentPlayer.tag);
		//Could also use to track a specific character
	}
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, currentPlayer.position.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, currentPlayer.position.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
