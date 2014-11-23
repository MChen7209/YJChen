using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WeaponController : MonoBehaviour 
{
	//Administration

	//Weapon stats
	public static List<Weapon> listOfWeapons = new List<Weapon>();
	
	public void initialize(string fileName)
	{
		try
		{
			StreamReader textReader = new StreamReader(fileName);
			string input = "";
			
			using(textReader)
			{
				do
				{
					input = textReader.ReadLine();
					if(input != null)
					{
						string[] weaponInfo = input.Split(',');
						listOfWeapons.Add (new Weapon(weaponInfo[0], weaponInfo[1], weaponInfo[2], float.Parse(weaponInfo[3])));
					}
				}
				while(input != null);
			}
		}
		catch (IOException e)
		{
			Debug.Log(e.ToString());
		}
	}

	public Weapon getWeapon(string weaponName)		{	return listOfWeapons.Find (e => e.ItemName.Contains (weaponName));	}
}
