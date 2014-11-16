using UnityEngine;
using System.Collections;

public class LightningStrike : MonoBehaviour 
{
	public int lightningStrikeOBLITERATIONDAMAGE;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	public void onTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy");
		{
			target.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamage(lightningStrikeOBLITERATIONDAMAGE);
		}
	}
}
