using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour 
{
	public int lightningBoltDamage;

	public void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.tag == "Enemy")
		{
			target.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamage(lightningBoltDamage);
		}
	}
}
