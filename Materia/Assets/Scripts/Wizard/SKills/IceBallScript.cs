using UnityEngine;
using System.Collections;

public class IceBallScript : MonoBehaviour 
{
	public GameObject iceBlock;
	private Animator anim;

	void Start()
	{
		anim = GameObject.FindGameObjectWithTag ("Wizard").GetComponent<Animator>();
	}

//	public void OnTriggerStay2D(Collider2D target)
//	{
//		if (target.gameObject.tag == "Ground")
//		{
//			GameObject likeABlawk = Instantiate (iceBlock, transform.position, iceBlock.transform.rotation) as GameObject;
//
//			if(anim.GetBool ("facingRight"))
//				likeABlawk.transform.localPosition = new Vector3 (transform.position.x -1, transform.position.y, transform.position.z);
//				//likeABlawk.transform.position = new Vector3 (transform.position.x -= 20, transform.position.y, transform.position.z); 
//			else
//				likeABlawk.transform.localPosition = new Vector3 (transform.position.x +1, transform.position.y, transform.position.z);
//				//likeABlawk.transform.position = new Vector3 (transform.position.x += 20, transform.position.y, transform.position.z);
//
//			Destroy (likeABlawk,5);
//		}
//		if(target.gameObject.tag == "Enemy")
//		{
//			Debug.Log("ice hit enemy");
//		}
//
//		Destroy (gameObject);
//	}

	public void OnTriggerEnter2D(Collider2D target){

		if(target.gameObject.CompareTag("Freeze"))
		{
			Debug.Log("in freeze trigger");
			target.gameObject.GetComponent<objectUpAndDown>().setFrozen(2);
			Destroy (gameObject);
		}
		if(target.gameObject.CompareTag ("Enemy"))
		{
			//freeze enemy
			Debug.Log ("Hitting Enemy: " + target.gameObject.tag);
			target.GetComponentInChildren<EnemyMove>().setFrozen(2);
			Destroy (gameObject);
		}
	}

//	public void OnTriggerHit2D(Collider2D target)
//	{
//		Debug.Log ("trigger");
//		Debug.Log (target.transform.parent.gameObject.tag);
//		if(target.transform.parent.gameObject.tag == "Enemy")
//		{
//			//freeze enemy
//			Debug.Log ("Hitting Enemy");
//			target.gameObject.GetComponent<EnemyMove>().setFrozen(2);
//		}
//		if(target.gameObject.CompareTag("Freeze"))
//		{
//			Debug.Log("in freeze trigger");
//			target.gameObject.GetComponent<objectUpAndDown>().setFrozen(2);
//
//		}
//
//		Destroy (gameObject);
//	}
}
