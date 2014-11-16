using UnityEngine;
using System.Collections;

public class FireGUI : MonoBehaviour {
	public Texture2D spellImage;
	private float cooldown;
	private string cooldownDisplay;
	private bool isCooldown;

	// Use this for initialization
	void Start () {
		isCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startCooldown(float theCooldown){
		cooldown = theCooldown;
		isCooldown = true;
	}

	public void endCooldown(){
		isCooldown = false;
	}

	void OnGUI(){


		if (isCooldown == false)
		{
			GUILayout.BeginArea (new Rect (20, 7 * Screen.height / 8, 100, 100));
			GUILayout.Label (spellImage);
			GUILayout.EndArea ();
		}//end if
		else
		{
			cooldownDisplay = cooldown.ToString();

			GUILayout.BeginArea (new Rect (20, 7 * Screen.height / 8, 100, 100));
			GUI.color = Color.gray; GUILayout.Label (spellImage);
			GUILayout.EndArea ();
			GUI.color = Color.white;
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = 30;
			myStyle.fontStyle = FontStyle.Bold;
			myStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(37, 7 * Screen.height / 8+10, 100, 100), cooldownDisplay, myStyle);
		}

	}
}
