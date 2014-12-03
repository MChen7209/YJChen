using UnityEngine;
using System.Collections;

public class IceBallScript : MonoBehaviour 
{
	public GameObject iceBlock;

	void Start()
	{
//		anim = GameObject.FindGameObjectWithTag ("Wizard").GetComponent<Animator>();
	}

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
}
