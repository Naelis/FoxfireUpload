using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	//Game object/component references
	public Sprite[] HeartSprites;
	public Image HeartsUI;
	PlayerController player;

	//Player's life, gotten from the PlayerController
	private int playerLife;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	 	playerLife = player.GetCurrentLife ();

	}

	/// <summary>
	/// This instance updates the player hearts sprite array to reflect a correct heart amount, derived from the playerLife's value.
	/// </summary>
	void Update(){
		
		HeartsUI.sprite = HeartSprites[player.GetCurrentLife()];

	}
}
