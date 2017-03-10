using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	//Class and game object references
	public GameObject spawnPoint;
	public GameObject checkPoint;
	public GameObject dieCheck;
	public PlayerController player;

	//Checkpoint states
	public enum state {Inactive, Active};
	public state status;
	public Sprite[] shrineStates;

	//Player saves
	private int currentLife;
	private int currentFire;
	public int saveLife;
	public int saveFire;

	void Start ()
	{
		status = state.Inactive;
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		spawnPoint = GameObject.Find ("SpawnPoint");
		checkPoint = GameObject.Find ("Checkpoint");
		dieCheck = GameObject.Find ("DieCheck");
	}

	void Update ()
	{
		ChangeShrineState ();
		//When key R is pressed, player "dies" and is moved to checkpoint IF ACTIVE. 
		//This is for testing purposes.
		if (Input.GetKeyDown (KeyCode.R)) {

			Die ();
		}
			
	}

	/// <summary>
	/// Changes the sprite of the checkpoint according to state.
	/// </summary>
	void ChangeShrineState ()
	{
		if (status == state.Inactive) {
			GetComponent<SpriteRenderer> ().sprite = shrineStates [0];

		} else if (status == state.Active) {
			GetComponent<SpriteRenderer> ().sprite = shrineStates [1];

			currentLife = player.GetCurrentLife ();
			currentFire = player.GetCurrentFire ();

			currentLife = saveLife;
			currentFire = saveFire;
		}
	}
	/// <summary>
	/// When attached collider is triggered by another collider, this code is initiated.
	/// If the triggering collider is called Player, checkpoint status is set to active.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerEnter2D (Collider2D collision)
	{
		//On collision with player, the checkpoint state is set to Active
		if (collision.tag.Equals ("Player")) {
			if (status == state.Inactive) {
				status = state.Active;
			} 
		}
	}
	/// <summary>
	/// Returns the value stored in saveLife.
	/// </summary>
	/// <returns>The save life.</returns>
	public int GetSaveLife ()
	{
		return saveLife;
	}
	/// <summary>
	/// Returns the value stored in saveFire.
	/// </summary>
	/// <returns>The save fire.</returns>
	public int GetSaveFire ()
	{
		return saveFire; 
	}
	/// <summary>
	/// When method is called, character dies.
	/// </summary>
	public void Die ()
	{
		//When method is called, the player is moved to last checkpoint, IF checkpoint state is set as active.
		if (status == state.Active) {
			player.transform.position = checkPoint.transform.position;
				
			player.SetLife (GetSaveLife ());
			player.SetFire (GetSaveFire ());
			return;
		}
		//Else, player is moved to the gameobject SpawnPoint at the beginning.
		player.transform.position = spawnPoint.transform.position;

		player.SetFire (player.GetStartFire ());
		player.SetLife (player.GetStartLife ());
	}

}