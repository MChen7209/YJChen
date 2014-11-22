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
//		setFileName(fileName);
		try
		{
//			Debug.Log("File Name: " + getFileName ());
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
						//						setItemName(weaponInfo[0]);
						//						setItemType(weaponInfo[1]);
						//						setItemDescription (weaponInfo[2]);
						//						weaponDamage = float.Parse(weaponInfo[3]);
						//						Debug.Log("Weapon: " + weaponInfo[0] + "| Type: " + weaponInfo[1] + "| Description: " + weaponInfo[2] + "| Damage: " + weaponInfo[3]);
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
//
//	public void setFileName(string nameOfFile)		{	fileName = nameOfFile;	}
//	public string getFileName()						{	return fileName;		}

	public Weapon getWeapon(string weaponName)		{	return listOfWeapons.Find (e => e.ItemName.Contains (weaponName));	}
}
