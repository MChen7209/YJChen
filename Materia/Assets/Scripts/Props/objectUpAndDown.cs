using UnityEngine;
using System.Collections;

public class objectUpAndDown : MonoBehaviour 
{
	public GameObject frozenThrone;

	private bool goDown;
	private float temp;
	private bool isFrozen;

	void Update () 
	{
		if (isFrozen)
			return;
		if (transform.localPosition.y > 10)
			goDown = true;
		if (transform.localPosition.y < -10)
			goDown = false;

		if (goDown)
		{
			transform.Translate (Vector3.up * -10 * Time.deltaTime);
		}
		if (!goDown)
		{
			transform.Translate (Vector3.up * 10 * Time.deltaTime);
		}


		//transform.Translate (Vector3.up * 10 * Time.deltaTime);
		//transform.Translate (Vector3.down * 10 * Time.deltaTime);
	}

	public void setFrozen(int frozenTime)
	{
		if(!isFrozen)
		{
			isFrozen = true;
			GameObject freezeHim = Instantiate(frozenThrone,transform.position,frozenThrone.transform.rotation) as GameObject;
			freezeHim.transform.parent = transform.parent;
			//transform.parent.rigidbody2D.isKinematic = true;
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
		isFrozen = false;
	}

	public void OnTriggerEnter2D(Collider2D target)
	{
		Debug.Log (target.tag);
		if (isFrozen)
			return;
		if (target.gameObject.CompareTag ("Ground"))
		{
			Destroy (target.gameObject);
		}

		if(target.gameObject.CompareTag ("Wizard") || target.gameObject.CompareTag ("Warrior") || target.gameObject.CompareTag ("Archer"))
		{

			StartCoroutine("ReloadGame");
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		//Also reload health game object.
	}
}

