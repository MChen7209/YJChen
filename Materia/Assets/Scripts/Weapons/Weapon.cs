using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Weapon : Item 
{
	//Administratior
	//Weapon stats
	float weaponDamage;

	public Weapon()
	{
		ItemName = "unequipped";
		ItemType = "unequipped";
		ItemDescription = "You are unequipped";
		weaponDamage = 1;
	}

	public Weapon(string name, string type, string description, float damage)
	{
		ItemName = name;
		ItemType = type;
		ItemDescription = description;
		weaponDamage = damage;
	}

	public float getWeaponDamage()	{	return weaponDamage;	}
}
