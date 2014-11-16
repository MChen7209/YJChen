using UnityEngine;
using System.Collections;

public class PositionProgressBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		transform.position = Camera.main.ScreenToWorldPoint(pos);
		//Screen.showCursor = false;
	
	}
}
