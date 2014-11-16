using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour 
{
	private Animator anim;
	private bool secondSkillLock;
	private float time;
	private GameObject progressBar;

	// Use this for initialization
	void Start () 
	{
		secondSkillLock = false;
		time = 1;
		progressBar.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
