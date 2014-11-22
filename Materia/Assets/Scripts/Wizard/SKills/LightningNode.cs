using UnityEngine;
using System.Collections;

public class LightningNode : MonoBehaviour 
{
	public GameObject lightningStrike;

//	public void OnTriggerEnter2D(Collider2D target)
//	{
//		RaycastHit2D hit;
//		Ray2D downRay = new Ray2D( new Vector3(transform.position.x, 150, transform.position.z), Vector3.down);
//		Debug.DrawRay (downRay.origin, Vector3.down * 1000, Color.black);
//		if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Enemy")
//		{
//			Debug.Log("In Raycast");
//			if(Physics2D.Raycast(downRay.origin, out hit, 1000f))
//			{
//				Debug.Log("In Raycast");
//				Debug.Log ("I hit: " + hit.collider.tag);
//				if(hit.collider.tag == "Enemy" || hit.collider.tag == "Ground")
//				{
//					Debug.Log("In Collider Hit");
//					GameObject explodeLikeADeathStar = Instantiate(lightningStrike, hit.point, lightningStrike.transform.rotation) as GameObject;
//					//Destroy (gameObject);
//					Destroy(explodeLikeADeathStar,1);
//				}
//			}
//			Destroy (gameObject);
//		}
//	}

	public void OnTriggerEnter2D(Collider2D target)
	{

		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Enemy")
		{
			RaycastHit2D hit = Physics2D.Raycast (new Vector2(transform.position.x, 150), Vector3.down, 1000f, ((1<<10) | (1<<11)));

			Debug.Log ("Inside the gameObject first tag checker");
			if(hit != null && hit.collider != null)
			{
				Debug.Log("I hit something: " + target.tag);
//				if(hit.collider.tag == "Enemy" || hit.collider.tag == "Ground")
//				{
					Debug.Log("In Collider Hit");
					GameObject explodeLikeADeathStar = Instantiate(lightningStrike, hit.point, lightningStrike.transform.rotation) as GameObject;
					//Destroy (gameObject);
					Destroy(explodeLikeADeathStar,1);
//				}
			}

			Destroy (gameObject);
		}

	}
}
