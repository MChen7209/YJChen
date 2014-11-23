using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
	//Item stats
	string itemName;
	string itemDescription;
	string itemType;

	public string ItemName
	{
		get	{	return itemName;	}
		set	{	itemName = value;	}
	}

	public string ItemDescription
	{
		get	{	return itemDescription;		}
		set	{	itemDescription = value;	}
	}

	public string ItemType
	{
		get	{	return itemType;	}
		set	{	itemType = value;	}
	}
}