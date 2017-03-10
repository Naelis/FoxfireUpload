using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//Controls
	ButtonController rightButton;
	ButtonController leftButton;
	ButtonController jumpButton;
	ButtonController atkButton;

	//Player stats	
	public int maxLife = 5;
	public int currentLife;
	public int currentFire;
	public int startLife = 3;
	public int startFire = 0;

	//Movement, flip
	public bool moving = false;
	public bool jumping = false;
	public bool attacking;
	public float maxS = 8f; //Movespeed
	Rigidbody2D playerBody;
	public bool facingRight = true;
	public bool CantMove;
	public Vector2 currentSpeed;

	//Jumps
	public float jumpForce = 8f; //Jump height
	public bool grounded = false;
	private bool canJump = false;
	private bool doubleJump = false;
	public int dJumpLimit = 2;

	//Attack (Don't be bothered by the warning message, the value is set in Unity)
	[SerializeField]
	GameObject firePrefab;

	//Class/Object references
	Animator anim;
	Checkpoint onDeath;

	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	
		rightButton = GameObject.Find ("ButtonRight").GetComponent<ButtonController> ();
		leftButton = GameObject.Find ("ButtonLeft").GetComponent<ButtonController> ();
		jumpButton = GameObject.Find ("ButtonJump").GetComponent<ButtonController> ();
		atkButton = GameObject.Find ("ButtonAttack").GetComponent<ButtonController> ();
		onDeath = GameObject.Find ("Checkpoint").GetComponent<Checkpoint> ();

		PlayerPrefs.SetInt ("doublejumps", dJumpLimit);
		currentLife = startLife;
		CantMove = false;
	}
		
	void FixedUpdate()
	{
		//If grounded = true, characater can Jump().
		if (grounded) {
			canJump = true;
			jumping = false;
		}

		//If canJump = true, or character is not grounded and has doubleJump available, character jumps when controls are pressed.
		//Jumping increases a counter, until it reaches a chosen value, and character can no longer Jump in mid-air.
		if (jumpButton.GetPressed() || Input.GetKeyDown(KeyCode.Space)) {
			
			if (canJump || (!grounded && doubleJump))  
			{
				Jump ();

				if(!grounded && dJumpLimit >= 1)
				{
					canJump = true;
					doubleJump = true;
					dJumpLimit--;
				}

				else if(!grounded && dJumpLimit <= 0)
				{
					doubleJump = false;
					canJump = false;
				}
			}
		}

		if (rightButton.GetPressed () || Input.GetKey (KeyCode.D)) {
			MoveForward ();
		}
			
		if (leftButton.GetPressed() || Input.GetKey(KeyCode.A)) {
			MoveBackward ();
		}

		if (atkButton.GetPressed() || Input.GetKeyDown(KeyCode.E)) {
			CantMove = true;
			Attack ();
			attacking = true;
			CantMove = false;
		} else {
			attacking = false;
		}
	}
	/// <summary>
	/// Moves the character forward. Flip() if necessary.
	/// </summary>
	public void MoveForward()
	{
		if (!CantMove) {
			anim.SetBool ("Moving?", true);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (maxS, GetComponent<Rigidbody2D> ().velocity.y);
			if (!facingRight) {
				Flip ();
			}
			facingRight = true;
		}
	}
	/// <summary>
	/// Moves the character backwards, FLip() if necessary.
	/// </summary>
	public void MoveBackward()
	{	
		if (!CantMove) {
			anim.SetBool ("Moving?", true);	
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-maxS, GetComponent<Rigidbody2D> ().velocity.y);
			if (facingRight) {
				Flip ();
			}
			facingRight = false;
		}

	}
	/// <summary>
	/// Character's jump method.
	/// </summary>
	public void Jump()
	{
		if (!CantMove) {
			jumping = true;
//			anim.SetBool ("Jumping", true);
			playerBody.velocity = new Vector2 (playerBody.velocity.x, jumpForce);

		}
	}
	/// <summary>
	/// Character attack.
	/// </summary>
	public void Attack()
	{
		anim = GetComponent<Animator> ();
		anim.SetTrigger ("Attack");

		if (facingRight) {
			GameObject tmp = (GameObject)Instantiate (firePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
			tmp.GetComponent<Fireball> ().Initialize (Vector2.right);
		} if(!facingRight) {
			GameObject tmp = (GameObject)Instantiate (firePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
			tmp.GetComponent<Fireball> ().Initialize (Vector2.left);
		}

	}
	/// <summary>
	/// Rotates the instance 180 degrees on the x -axis.
	/// Turns character when turning backwards.
	/// </summary>
	void Flip()
	{	
		if (facingRight) {
			if (transform.rotation != Quaternion.Euler (0, 180, 0)) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
		}

		if (!facingRight) {
			if (transform.rotation != Quaternion.Euler (0, 0, 0)) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
	}
/// <summary>
/// Detects collision with colliders.
/// </summary>
/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D collision){
//		transform.Translate (-1f, 0, 0);
		//collision.collider.gameObject.SetActive (false); -> Törmäyksen kohde deaktivoidaan törmäyksessä

		if (!collision.collider.CompareTag ("Ground")) {
			
			//If collider has the tag 'nori', increases life count by one.
			if(collision.collider.CompareTag("Nori")){
				GameObject.Destroy(collision.collider.gameObject); //Consumable is destroyed when collided with.
				currentLife++;
			}
			//If collider has the tag 'fire', increases fire count by one.
			if (collision.collider.CompareTag ("Fire")) {
				GameObject.Destroy (collision.collider.gameObject);
				currentFire++;
			}
			//If collider has tag 'wall', character is bumped backwards.
			if (collision.collider.CompareTag ("Wall")) {
				transform.Translate (-1f, 0, 0);
			}

			//If collider has the tag 'enemy', player loses 1 life.
			if (collision.collider.CompareTag ("Enemy")) {
				transform.Translate (-1f, 0, 0);
				currentLife--;
				if (currentLife < 0) {
					currentLife = 0;
				}
				//When current life reaches 0 or less, Die method from Checkpoint -class is called and the player dies.
				if (currentLife == 0) {
					onDeath.Die ();	
				}
			}

			if (collision.collider.CompareTag ("Death")) {
				onDeath.Die ();
			}
		}
		//If collider has the tag 'ground', character is considered grounded.
		if (collision.collider.CompareTag ("Ground")) {
			grounded = true;
		}
	}

	//When no longer colliding with ground, character is no longer 'grounded'.
	void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.CompareTag ("Ground")) {
			grounded = false;
		}
	}

	//Returns the player's current Life, but no more than max life and no less than 1.
	public int GetCurrentLife(){

		if (currentLife < maxLife) {
			return currentLife;
		} 
		if (currentLife >= maxLife) {
			currentLife = maxLife;
			
			return currentLife;

		} else {
			return 1;
		}
	}

	//Returns the player's current fire (power).
	public int GetCurrentFire(){
		return currentFire;
	}
	//Sets the player's current life (for the purpose of respawning). Called by Checkpoint.
	public void SetLife(int l){
		currentLife = l;
	}
	//Sets the player's current fire (for the purpose of respawning). Called by Checkpoint.
	public void SetFire(int f){
		currentFire = f;
	}
	//Gets the player's starting fire (for the purpose of respawning). Called by Checkpoint.
	public int GetStartFire(){
		return startFire;
	}
	//Gets the player's starting life (for the purpose of respawning). Called by Checkpoint.
	public int GetStartLife(){
		return startLife;
	}		
}