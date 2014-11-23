using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	private UnifiedSuperClass god;
	private PlayerHealthController healthController;
	public float health = 100f;					// The player's health.
	public float armor = 0;
	public float maxHP = 100;
	
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private Animator anim;						// Reference to the Animator on the player

	private bool immunity;

//	private bool alive;
	
	void Awake ()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass>();
		healthBar = gameObject.GetComponentInChildren<SpriteRenderer> ();
		anim = GetComponent<Animator>();
		
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}

	public void setConnection(PlayerHealthController parent)
	{
		healthController = parent;
	}

	public PlayerHealthController getConnection()
	{
		return healthController;
	}

	public void TakeDamage (float damage)
	{
		if(immunity)
			return;

		health = healthController.takeDamage(damage);
		UpdateHealthBar ();
	}

	public void setHP(string amount)
	{
		if(amount.CompareTo("Full") == 0)
		{
			healthController.HP = healthController.MaxHP;
			health = healthController.HP;
		}
		if(amount.CompareTo ("None") == 0)
		{
			healthController.HP = 0f;
			health = healthController.HP;
		}
	}

	public void heal(float amount)
	{
		health += amount;
		if(health > maxHP)
		{
			health = maxHP;
		}
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}

	void Update()
	{
		if (health <= 0)
		{
			healthController.Alive = false;
			god.swapCharacterUponDeath(gameObject);
		}
	}
}