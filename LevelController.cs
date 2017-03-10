using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	//Component/object references
	public Texture mainMenu;
	public Texture lvl1Complete;
	public AudioClip Chime;
	public GameObject EndTransition;
	PlayerController player;

	void Start() {
		player = GetComponent<PlayerController>();
		EndTransition = GameObject.Find("Transition");
	}

	/// <summary>
	/// If collider triggering the edge of the screen has the tag player, the level ends and the player is sent to the 'victory screen'.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Löytyi osuma");
			Application.LoadLevel("Level 1 complete");
		}
	}

	/// <summary>
	/// Victory screen comes up when this runs. Audio is currently disabled in Unity.
	/// </summary>
	void OnGUI () {
//		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), mainMenu);
		if (GUI.Button(new Rect(Screen.width /2, Screen.height /2, 150, 25), "Play")) {
			Application.LoadLevel ("FF");
		}
		if (GUI.Button(new Rect (Screen.width /2, Screen.height /2 + 25, 150, 25), "Quit")) {
			Application.Quit();
		}
		
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), lvl1Complete);
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /2, 150, 25), "Play")) {
			Application.LoadLevel("FF");
		}
//		if (GUI.Button(new Rect (Screen.width /2, Screen.height /2 + 25, 150, 25), "Main Menu")) {
//			Application.LoadLevel("MainMenu");
//		}
	}
}
