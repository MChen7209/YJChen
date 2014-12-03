using UnityEngine;
using System.Collections;

public class SlowLightShift : MonoBehaviour 
{
	private bool nightTime;
	public Color nightColor;
	public Color dayColor;
	private Color skyColor;
	//private Camera cam;
	// Use this for initialization
	void Awake()
	{
		//cam = Camera.main;
	}
	void Start () 
	{
		nightTime = true;
		skyColor = Camera.main.backgroundColor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (nightTime)
		{
			//Debug.Log ("Changing it to night");
			skyColor = nightColor;
			Camera.main.backgroundColor = Color.Lerp (Camera.main.backgroundColor, skyColor, Time.deltaTime);
		}
		if (!nightTime)
		{
			//Debug.Log ("Changing it to day");
			skyColor = dayColor;
			Camera.main.backgroundColor = Color.Lerp (Camera.main.backgroundColor, skyColor, Time.deltaTime);
		}
		

	}

	public void OnTriggerEnter2D(Collider2D target)
	{
		//if (target.CompareTag ("Wizard") || target.CompareTag ("Warrior") || target.CompareTag ("Archer"))
		if(target.CompareTag("AbsoluteTarget"))
		{
			nightTime = !nightTime;
			Debug.Log("I'm changing colors: " + nightTime);
		}
	}
}
