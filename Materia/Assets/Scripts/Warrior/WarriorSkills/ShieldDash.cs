using UnityEngine;
using System.Collections;

public class ShieldDash : MonoBehaviour {

	public float knockBack;
	
	public void OnTriggerEnter2D(Collider2D target){
		
		if (target.gameObject.tag == "Enemy")
		{
			Vector2 temp = new Vector2(1,1);

			float x = target.transform.position.x;

			target.gameObject.rigidbody2D.AddForce(Vector2.up * 1500);

		}//end if
		
	}

}
