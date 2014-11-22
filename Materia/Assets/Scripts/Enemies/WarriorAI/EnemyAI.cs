using UnityEngine;
using System.Collections;
//simple "platformer enemy" AI
public class EnemyAI : MonoBehaviour {

	private float fireBallWait;
	public float fireBallCooldown;
	public GameObject fireBallProjectile;
	public GameObject enemy;
	private bool isFireBallCooldown;

	private bool isRange;

	void Update(){

		if (isRange == true)
						attack ();

	}

	void OnTriggerStay2D (Collider2D other) {
						//rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

						Vector3 initialPosition;
						Vector3 initialVelocity;

						Vector3 moveDirection;
						Quaternion rotation;
						if (other.gameObject.tag == "Wizard")
						{
								Debug.Log ("found the wizard");
								Debug.DrawLine (transform.position, other.gameObject.transform.position, Color.cyan);
	
								// Change angle towards player
								Vector3 dir = other.gameObject.transform.position - transform.position;
								float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
								//Debug.Log(angle);
								transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	
								//			//fire spell code
								if (isFireBallCooldown == false)
								{
										isFireBallCooldown = true;
		
										// Distance between player and enemy
										float distance = Vector3.Distance (transform.position, other.gameObject.transform.position);
										//initial position of projectile
										initialPosition = transform.position;
		
										GameObject createClone = Instantiate (fireBallProjectile, transform.position, transform.rotation) as GameObject;
										if (createClone.transform.rotation.eulerAngles.z > 90)
										{
												Debug.Log ("shot the fireball");
												createClone.rigidbody.velocity = transform.TransformDirection (new Vector3 (30, -distance / 1.3f, 0));
										} else
										{
												createClone.rigidbody.velocity = transform.TransformDirection (new Vector3 (30, distance / 1.3f, 0));
										}
										//initial velocity of projectile
										initialVelocity = createClone.rigidbody.velocity;
										Destroy (createClone, 5f);
										StartCoroutine (simulateFireBallCooldown ());
								}//end if
			Debug.Log("Should be walking");
								transform.Translate(Vector3.right * Time.deltaTime * .1f);
	
						}//end if
	}

	void attack(){

		Debug.Log ("enemy should be attacking");

	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Wizard")
			//Debug.Log ("enter");
			isRange = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Wizard")
			isRange = false;
	}

	private IEnumerator simulateFireBallCooldown(){
		fireBallWait = fireBallCooldown;
		for (var x = 1; x < fireBallCooldown; x++) {
			fireBallWait--;
			yield return new WaitForSeconds(1);
		}//end for
		isFireBallCooldown = false;
		fireBallWait = 0;
	}

}
