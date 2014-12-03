using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour 
{

	public void OnTriggerStay2D(Collider2D target)
	{
		if(target.transform.CompareTag("Wizard") || target.transform.CompareTag("Warrior") || target.transform.CompareTag ("Archer"))
		{
			//target.rigidbody2D.AddForce(Vector2(0,10));
			Debug.Log (target.transform.tag);
			target.transform.GetComponentInChildren<PlayerHealth>().TakeDamage(1);
		}
	}
}
