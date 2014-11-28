using UnityEngine;
using System.Collections;

public class Archer : Character 
{
	public GameObject AnchorPrefab;
	public Archer(string name, string type, string description, string health, string armor) : base(name, type, description, float.Parse(health), float.Parse(armor))
	{
		AnchorPrefab = Resources.Load("prefabs/skills/PointAnchor") as GameObject;
	}
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void instantitateAnchorPrefab()
	{
		DontDestroyOnLoad(Instantiate(AnchorPrefab, new Vector3(0,0,0), Quaternion.identity));
	}
}