using UnityEngine;
using System.Collections;

public class LightBallScript : MonoBehaviour 
{
	Vector3 pLocation;
	Vector3 mosLocation;
	private bool initialMovement;
	// Use this for initialization
	void Awake () 
	{
		initialMovement = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(initialMovement)
			transform.position = Vector3.Lerp (transform.position, mosLocation, Time.deltaTime);
		if ((transform.position == mosLocation))// && initialMovement)
			initialMovement = false;
	}
	public void setVectors(Vector3 playerLocation, Vector3 mouseLocation)
	{
		pLocation = playerLocation;
		mosLocation = mouseLocation;
		initialMovement = true;
	}
}
