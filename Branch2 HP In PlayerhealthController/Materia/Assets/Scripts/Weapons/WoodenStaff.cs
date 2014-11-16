using UnityEngine;
using System.Collections;

public class WoodenStaff : Weapon
{

	// Use this for initialization
	void Start () 
	{
		setWeaponName("Wooden Staff");
		setWeaponDamage (1f);
		setWeaponDescription("Basic wooden staff.");
		setWeaponType ("Staff");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
