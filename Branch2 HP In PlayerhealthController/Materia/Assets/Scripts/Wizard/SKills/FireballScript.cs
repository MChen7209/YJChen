using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour 
{
	public float fireballDamage;

	public void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy")
		{
			target.GetComponentInChildren<PlayerHealth> ().TakeDamage (fireballDamage);
			Destroy (gameObject);
		}

		if (target.gameObject.tag == "Ground")
		{
			Destroy (gameObject);
		}

		if(target.gameObject.tag== "Flammable")
		{
			target.GetComponentInChildren<Torch>().activateFlammable ();
			Destroy (gameObject);
		}


	}
}
