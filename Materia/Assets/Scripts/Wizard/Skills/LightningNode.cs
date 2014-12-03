using UnityEngine;
using System.Collections;

public class LightningNode : MonoBehaviour 
{
	public GameObject lightningStrike;

	private float baseDamage;

	void Awake()
	{
		baseDamage = 1;
	}

	public void OnTriggerEnter2D(Collider2D target)
	{

		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Enemy")
		{
			RaycastHit2D hit = Physics2D.Raycast (new Vector2(transform.position.x, 150), Vector3.down, 1000f, ((1<<10) | (1<<11)));

			Debug.Log ("Inside the gameObject first tag checker");
			if(hit != null && hit.collider != null)
			{
				Debug.Log("I hit something: " + target.tag);
				Debug.Log("In Collider Hit");
				GameObject explodeLikeADeathStar = Instantiate(lightningStrike, hit.point, lightningStrike.transform.rotation) as GameObject;
				explodeLikeADeathStar.GetComponentInChildren<LightningBolt>().LightningBoltDamage = baseDamage;
				explodeLikeADeathStar.GetComponentInChildren<LightningStrike>().LSDamage = baseDamage*2;
				Destroy(explodeLikeADeathStar,1);
			}

			Destroy (gameObject);
		}
	}
}
