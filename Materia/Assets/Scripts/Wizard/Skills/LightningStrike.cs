using UnityEngine;
using System.Collections;

public class LightningStrike : MonoBehaviour 
{
	public float lightningStrikeOBLITERATIONDAMAGE;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	public void onTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy")
		{
			target.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamage(lightningStrikeOBLITERATIONDAMAGE);
		}
	}

	public float LSDamage
	{
		get	{	return lightningStrikeOBLITERATIONDAMAGE;	}
		set	{	lightningStrikeOBLITERATIONDAMAGE = value;	}
	}
}
