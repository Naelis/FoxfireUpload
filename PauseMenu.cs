using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

	//Buttons
	Button menuButton;
	Button resumeButton;
	Button quitButton;
	Button restartButton;

	//Gameobject reference
	public GameObject PauseUI;

	//Boolean for whether pause is currently true/false.
	private bool paused = false;

	void Start ()
	{
		menuButton = GameObject.Find("ButtonMenu").GetComponent<Button>();
		resumeButton = GameObject.Find ("ButtonResume").GetComponent<Button> ();
		restartButton = GameObject.Find ("ButtonRestart").GetComponent<Button> ();
		quitButton = GameObject.Find ("ButtonQuit").GetComponent<Button> ();

		PauseUI.SetActive (false);

		menuButton.onClick.AddListener (() => Clicked ());
		resumeButton.onClick.AddListener (() => Resume ());
		restartButton.onClick.AddListener (() => Restart ());
		quitButton.onClick.AddListener (() => Quit ());
	}
	/// <summary>
	/// Update checks if pause is true. If true, timeScale is set to 0. If false, game continues as usual (with a timeScale of 1).
	/// </summary>
	void Update ()
	{
		if (paused) {
			PauseUI.SetActive (true);
			Time.timeScale = 0;
		}
		if (!paused) {
			PauseUI.SetActive (false);
			Time.timeScale = 1;
		}
	}
	/// <summary>
	/// When the menu -button is clicked, pause is set to true.
	/// </summary>
	public void Clicked(){
		paused = true;
	}
	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void Resume(){

		paused = false;
	}

	public void Restart(){
		Application.LoadLevel("FF");
	}

	/// <summary>
	/// Quits the game.
	/// </summary>
	public void Quit(){
		Application.Quit ();
	}

}
