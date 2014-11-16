using UnityEngine;
using System.Collections;

public class WarriorSpinny : MonoBehaviour 
{
	public float spinnyDamage;
	// Use this for initialization

	void Awake()
	{
	}

	public void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy")
		{
			target.GetComponentInChildren<PlayerHealth> ().TakeDamage (spinnyDamage);

			Debug.Log("Hurt Vectoring");
			// Create a vector that's from the enemy to the player with an upwards boost.
			Vector3 hurtVector = transform.position - target.transform.position;
			Debug.Log("Hurt Vectoring2 ");
			// Add a force to the player in the direction of the vector and multiply by the hurtForce.
			//target.transform.rigidbody2D.AddForce(hurtVector * 20f);
			target.transform.rigidbody2D.velocity *= -1;
			Debug.Log("Hurt Vectoring 3");
		}
	}
}
