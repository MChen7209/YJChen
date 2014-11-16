using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	private UnifiedSuperClass god;
	public float health = 100f;					// The player's health.
	public float armor = 0;
	public float maxHP = 100;
	
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
//	private PlayerController playerController;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player

	private bool immunity;

	private bool alive;
	
	void Awake ()
	{
		god = GameObject.FindGameObjectWithTag ("God").GetComponent<UnifiedSuperClass>();
		// Setting up references.
//		playerController = GetComponent<PlayerController>();
//		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		healthBar = gameObject.GetComponentInChildren<SpriteRenderer> ();
		anim = GetComponent<Animator>();
		
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
		alive = true;
		immunity = false;
	}

	public void setArmor(float _armor)
	{
		armor = _armor;
	}

	private float calculateDamage (float damage)
	{
		return damage / (armor * .5f);
	}

	public void TakeDamage(float damage, Transform enemyMedium, float force)
	{
		if(immunity)
			return;

		Vector3 hurtVector = transform.position - enemyMedium.position + Vector3.up * 5f;
		rigidbody2D.AddForce (hurtVector * force);

		health -= calculateDamage(damage);
		UpdateHealthBar ();
	}

	public void TakeDamage (float damage)
	{
		health -= damage;
		if (health < 0)
			health = 0;
		Debug.Log ("Health: " + health);
		UpdateHealthBar ();
	}

	public void setImmunity()
	{
		immunity = !immunity;
	}

	public bool isAlive()
	{
		Debug.Log("PlayerHealth.cs/isAlive() " + alive);
		return alive;
	}

	public void setAlive(bool state)
	{
		alive = state;
	}

	public void setHP(float amount)
	{
		health = amount;
	}

	public void setHP(string amount)
	{
		if(amount.CompareTo("Full") == 0)
			health = maxHP;
		if(amount.CompareTo ("None") == 0)
			health = 0;
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
			if(transform.parent.CompareTag("Wizard") || transform.parent.CompareTag("Warrior") || transform.parent.CompareTag ("Archer"))
			{
				alive=false;
				god.saveHealth(gameObject);
				god.swapCharacterUponDeath(gameObject);
			}
			else
				Destroy (transform.parent.gameObject);
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}

	public bool isLiving()
	{
		return alive;
	}
}