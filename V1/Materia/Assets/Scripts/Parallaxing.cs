using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	// Array of background and foregrounds to be parallaxed
	public Transform[] backgrounds;
	// Proportion of the camera's movement to move the background by
	private float[] parallaxScales;
	// How smooth the parallax is going to be. Make sure to set this above 0.
	public float smoothing = 1f;

	// Reference to the main cameras transform
	private Transform cam;
	// The position of the camera in the previous frame
	private Vector3 previousCamPos;

	// Is called before Start(). Great for references(cameras).
	void Awake(){

		// Set up the camera reference
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start () {

			// The previous frame had the current frame's camera position
			previousCamPos = cam.position;

			// Assigning corresponding parallaxScales
			parallaxScales = new float[backgrounds.Length];
			for (int i=0; i<backgrounds.Length; i++)
			{
				backgrounds[i].renderer.enabled = false;
				parallaxScales [i] = backgrounds [i].position.z * -1;
			}//end for

	}
	
	// Update is called once per frame
	void Update () {
		if (cam.position.x > 744){
			for(int i = 0; i<backgrounds.Length; i++){
				backgrounds[i].renderer.enabled = true;
			}//end for
		}//end if
	if (cam.position.x > 744){
		// for each background
		for(int i=0; i<backgrounds.Length; i++){
			// Parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// Set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// Create a target position which is the background's current position with its target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// Fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}//end for

		// Set previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
		}//end if
	}

}
