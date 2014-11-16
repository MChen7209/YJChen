using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void activateFlammable()
	{
		//Make a new light or set it in a prefab.
		transform.FindChild("Light").gameObject.SetActive(true);
	}
}
