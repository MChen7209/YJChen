using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour 
{
	private float _lightningBoltDamage;

	public void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.tag == "Enemy")
		{
			target.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamage(_lightningBoltDamage);
		}
	}

	public float LightningBoltDamage
	{
		get	{	return _lightningBoltDamage;	 }
		set	{	_lightningBoltDamage = value; }
	}
}
