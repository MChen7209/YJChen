using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour 
{
	private float fireballBaseDamage;
	
	public void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy")
		{
			target.GetComponentInChildren<PlayerHealth> ().TakeDamage (fireballBaseDamage);
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

	public void setFireballDamage(float baseDamage)	{	fireballBaseDamage = baseDamage;	}
	public float getFireballDamage()				{	return fireballBaseDamage;			}
}
