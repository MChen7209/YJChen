using UnityEngine;
using System.Collections;

public class SwordHit : MonoBehaviour {

	public float damage;

	public void OnTriggerEnter2D(Collider2D target){

		if (target.gameObject.tag == "Enemy")
		{
			target.GetComponentInChildren<PlayerHealth> ().TakeDamage (damage);
		}//end if

	}

}
