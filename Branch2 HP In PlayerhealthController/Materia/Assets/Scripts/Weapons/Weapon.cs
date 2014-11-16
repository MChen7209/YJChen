using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	//Weapon stats
	float weaponDamage;
	string weaponName;
	string weaponDescription;
	string weaponType;

	public void setWeaponName(string wN)			{	weaponName = wN;			}
	public void setWeaponDamage(float wD)			{	weaponDamage = wD;			}
	public void setWeaponDescription(string wDe)	{	weaponDescription = wDe;	}
	public void setWeaponType(string wT)			{	weaponType = wT;			}

	public string getWeaponName()					{	return weaponName;			}
	public float getWeaponDamage()					{	return weaponDamage;		}
	public string getWeaponDesc()					{	return weaponDescription;	}
	public string getWeaponType()					{	return weaponType;			}
}
