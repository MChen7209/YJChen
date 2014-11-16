using UnityEngine;
using System.Collections;

public class WoodenBow : Weapon 
{

	// Use this for initialization
	void Start () 
	{
		setWeaponName("Wooden Bow");
		setWeaponDamage (1f);
		setWeaponDescription("Basic wooden bow.");
		setWeaponType ("Bow");	
	}
}
