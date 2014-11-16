using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{
	public int arrowDamage;
	// Use this for initialization
	public void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.tag == "Enemy")
		{
				target.GetComponentInChildren<PlayerHealth> ().TakeDamage (arrowDamage);
				Destroy (gameObject);
		}

		if (target.gameObject.tag == "Ground")
		{
				Destroy (gameObject);
		}
	}
}
