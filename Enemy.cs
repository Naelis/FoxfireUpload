using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	//Reference to player object
	GameObject player;

	//Movement
	public Transform target;
	private Transform myTransform;
	public float speed = 3f;
	public float maxDistance;

	//Stats
	public int enemyLife = 2;
	public int rotationSpeed = 3;
	public float attack1Range = 1f;
	public int attack1Damage = 1;
	public float timeBetweenAttacks;

	void Awake() {
		myTransform = transform;
	}
	/// <summary>
	/// Enemy starts to idle.
	/// Player is set as target.
	/// </summary>
	void Start() {
//		Rest ();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;
	}
//	/// <summary>
//	/// Enemy starts with idle.
//	/// </summary>
//	public void Rest () {
//
//	}
	/// <summary>
	/// When enemy is far enough from target (player), enemy moves towards player.
	/// </summary>
	void Update() {

		if(Vector3.Distance(target.position, myTransform.position) > maxDistance) {
			
			//move towards player
			myTransform.position += myTransform.forward * speed * Time.deltaTime;
		}

		//If enemy has 0 health, enemy object is destroyed.
		if (enemyLife <= 0) {
			Destroy (this.gameObject);

		}
	}
	/// <summary>
	/// When a collider tagged 'FoxAttack' collides with enemy, enemy loses 1 health.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter2D(Collision2D other){

		if (other.collider.CompareTag("FoxAttack")){
			enemyLife--;
		}
	}
}