using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{
	[SerializeField]
	private GameObject frozenThrone;

	[HideInInspector]
	private GameObject enemy;
	private bool facingRight;
	private bool isFrozen;
	private bool moving;

	private Animator anim;

	// Use this for initialization
	void Awake () 
	{
		anim = transform.parent.gameObject.GetComponent<Animator> ();
		facingRight = true;
		enemy = transform.parent.gameObject;
		isFrozen = false;
//		frozenThrone = transform.parent.FindChild ("FrozenThrone").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moving = false;
		anim.SetBool ("Moving", moving);
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (isFrozen)
				return;
		if (other.gameObject.tag == "Wizard" || other.gameObject.tag == "Archer" || other.gameObject.tag == "Warrior")
		{
			Transform target = other.gameObject.transform;

			moving = true;
			anim.SetBool("Moving", moving);
			if ((target.transform.position.x > enemy.transform.position.x) && !facingRight) 
				FlipDirection();
			else if ((target.transform.position.x < enemy.transform.position.x) && facingRight) 
				FlipDirection();

			if(facingRight)
				enemy.transform.Translate (Vector3.right * Time.deltaTime * 4f);
			else if(!facingRight)
				enemy.transform.Translate (Vector3.left * Time.deltaTime * 4f);
		}
	}

	public void setFrozen(int frozenTime)
	{
		if(!isFrozen)
		{
			isFrozen = true;
			GameObject freezeHim = Instantiate(frozenThrone,transform.parent.position,frozenThrone.transform.rotation) as GameObject;
			freezeHim.transform.parent = transform.parent;
			transform.parent.rigidbody2D.isKinematic = true;
			Destroy (freezeHim, frozenTime);
			StartCoroutine(simulateFreeze(frozenTime));
		}
		else
		{
			Debug.Log ("Already frozen");
		}
	}
	
	private IEnumerator simulateFreeze(int theTime)
	{
		yield return new WaitForSeconds (theTime);
		transform.parent.rigidbody2D.isKinematic = false;
		isFrozen = false;
	}
	void FlipDirection()
	{
		Debug.Log ("Should be flipping");
		facingRight = !facingRight;
		Vector3 charScale = enemy.transform.localScale;
		charScale.x *= -1;
		enemy.transform.localScale = charScale;
	}

}
