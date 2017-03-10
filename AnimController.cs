using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	//Game object/component references
	Animator anim;
	PlayerController player;
	Rigidbody2D playerBody;

	//Current speed
	Vector3 currentSpeed;

	void Start () {
		player = gameObject.GetComponent<PlayerController> ();
		anim = gameObject.GetComponent<Animator> ();
		playerBody = GetComponent<Rigidbody2D>();
	}

	void Update(){
		//currentSpeed checks for playerBody rigidbody's current velocity. If it's anything else than 0, animator's bool 'moving?' is true.
		//If it's false, animator is given the information that the player is not moving.
		currentSpeed = playerBody.velocity; 
		float speedCheck = currentSpeed.magnitude;

		if (speedCheck == 0) {
			anim.SetBool ("Moving?", false);
		} else {
			anim.SetBool ("Moving?", true);
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		anim.SetBool ("Jumping", player.jumping);

	}
}
