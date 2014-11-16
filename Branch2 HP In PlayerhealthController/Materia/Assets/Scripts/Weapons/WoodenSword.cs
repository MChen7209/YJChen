using UnityEngine;
using System.Collections;

public class WoodenSword : Weapon 
{
	// Use this for initialization
	void Start () 
	{
		setWeaponName("Wooden Sword");
		setWeaponDamage (1f);
		setWeaponDescription("Basic wooden sword.");
		setWeaponType ("Sword");
	}
}
