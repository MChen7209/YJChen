using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour 
{
	private UnifiedSuperClass god;
	public float health = 100f;					// The player's health.
	public float armor = 0;
	public float maxHP = 100;
	private bool alive;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void setMaxHP(float newMaxHP)
	{
		maxHP = newMaxHP;
	}

	public void setArmor(float newArmor)
	{
		armor = newArmor;
	}

	public void setAlive(bool setAliveTo)
	{
		alive = setAliveTo;
	}
}
