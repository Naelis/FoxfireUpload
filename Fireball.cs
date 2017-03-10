using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	[SerializeField]
	private float speed = 22f;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	private Animator anim;

	[SerializeField]
	private CircleCollider2D ballCollider;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();	
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		
		myRigidbody.velocity = direction * speed;

	}

	public void Initialize(Vector2 direction){
		this.direction = direction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){

		if(!collision.collider.CompareTag("Player")){
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
