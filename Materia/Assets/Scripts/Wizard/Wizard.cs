using UnityEngine;
using System.Collections;

public class Wizard : Character 
{
	public Wizard(string name, string type, string description, string health, string armor) : base(name, type, description, float.Parse(health), float.Parse(armor))
	{
		Debug.Log("This is a wizard");
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
