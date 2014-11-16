using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour 
{
	//Administration
	private UnifiedSuperClass god;
	private GameObject belongsTo;
	private PlayerHealth playerHealthScript;


	//Status
	float health = 100f;					// The player's health.
	float armor = 0;
	float maxHP = 100;

	//States
	private bool alive;
	private bool immunity;

	// Use this for initialization
	void Awake () 
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass>();
		alive = true;
		immunity = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void connectToScript(GameObject target)
	{
		belongsTo = target;
		playerHealthScript = target.GetComponentInChildren<PlayerHealth>();
		playerHealthScript.setConnection(this);
	}

	private float calculateDamage (float damage)
	{
		return damage / (armor * .5f);
	}

	public float takeDamage(float damage)
	{
		//This damage is one that is absolute.
		health -= damage;
		if (health < 0)
			health = 0;
		return health;
	}

	public float takeDamage(float damage, string damageType)
	{
		//Damage type can be one that bypasses armor
		health -= damage;
		if (health < 0)
			health = 0;
		return health;
	}

	public float addKnockback(float force)
	{
		//Add a knockback effect.
		return 0f;
	}

	public void setMaxHP(float newMaxHP)  	{  		maxHP = newMaxHP;   	}
	public void setArmor(float newArmor)  	{  		armor = newArmor;   	}
	public void setAlive(bool setAliveTo) 	{  		alive = setAliveTo; 	}
	public void setImmunity(bool state)   	{		immunity = state;		}
	public void setHP(float hp)				{		health = hp;			}
	public void healHP(float hp)			{		health += hp;			}

	public bool isAlive()					{		return alive;			}
	public float getHP()					{		return health;			}
	public float getMaxHp()					{		return maxHP;			}
	public float getArmor()					{		return armor;			}
}
