using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour 
{
	//Administration
	private UnifiedSuperClass god;
	private Character belongsTo;
	private PlayerHealth playerHealthScript;
	
	//Status
	float health = 100f;					// The player's health.
	float armor = 0;
	float maxHP = 100;

	//States
	private bool alive;
	private bool immunity;

	public PlayerHealthController(float maxHealth, float armor)
	{
		this.maxHP = maxHealth;
		this.armor = armor;
		alive = true;
		immunity = false;
	}

	// Use this for initialization
	void Awake () 
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass>();
	}

	public void connectToScript(Character target)
	{
		Debug.Log(target.CharacterName);
		belongsTo = target;
		playerHealthScript = target.CharacterGameObject.GetComponentInChildren<PlayerHealth>();
		playerHealthScript.setConnection(target.HealthController);
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

	public float MaxHP
	{
		get	{	return maxHP;	}
		set	{	maxHP = value;	}
	}
	public float Armor
	{
		get	{	return armor;	}
		set	{	armor = value;	}
	}
	public bool Alive
	{
		get	{	return alive;	}
		set	{	alive = value;	}
	}
	public bool Immunity
	{
		get	{	return immunity;	}
		set	{	immunity = value;	}
	}
	public float HP
	{
		get	{	return health;	}
		set	{	health = value;	}
	}
}
