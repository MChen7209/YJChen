using UnityEngine;
using System.Collections;

public class KeyRecieverLv1 : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D target)
	{
		if(target.CompareTag("OpenSeseme"))
		{
			Destroy (target.transform.parent.gameObject);
			transform.FindChild("Door").gameObject.SetActive(false);
//			Destroy (gameObject);
		}
	}
}
