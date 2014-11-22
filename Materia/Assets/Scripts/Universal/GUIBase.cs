using UnityEngine;
using System.Collections;

public class GUIBase : MonoBehaviour 
{
	string GUITag;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void setGUITag(string guiTag)	{	GUITag = guiTag;	}
	
	public string getGUITag()				{	return GUITag;		}
}