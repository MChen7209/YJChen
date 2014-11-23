using UnityEngine;
using System.Collections;

public class ButtonBase : MonoBehaviour 
{
	public void OnMouseEnter()
	{
		renderer.material.color = Color.grey;
	}
	
	public void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
}
