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
			healthController.setHP (healthController.getMaxHp());
			health = healthController.getHP ();
		}
		if(amount.CompareTo ("None") == 0)
		{
			healthController.setHP (0f);
			health = healthController.getHP();
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

//	public void setImmunity()
//	{
//		immunity = !immunity;
//	}

//	public bool isAlive()
//	{
//		return alive;
//	}
//
//	public void setAlive(bool state)
//	{
//		alive = state;
//	}

//	public void setHP(float amount)
//	{
//		health = amount;
//	}

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
//			if(transform.parent.CompareTag("Wizard") || transform.parent.CompareTag("Warrior") || transform.parent.CompareTag ("Archer"))
//			{
//				alive=false;
				healthController.setAlive(false);
				god.swapCharacterUponDeath(gameObject);
//			}
//			else
//				Destroy (transform.parent.gameObject);
		}
	}

//	IEnumerator ReloadGame()
//	{			
//		// ... pause briefly
//		yield return new WaitForSeconds(0);
//		// ... and then reload the level.
//		Application.LoadLevel(Application.loadedLevel);
//		//Also reload health game object.
//	}
}