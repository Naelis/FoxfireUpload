using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

//	[SerializeField]
//	private BoxCollider2D rightBound;

//	Camera cam;
//	public Bounds bounds;

	// Use this for initialization
	void Start () 
	{
//		rightBound = GameObject.FindWithTag ("Right").GetComponent<BoxCollider2D> ();

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
	}

//	void OnTriggerEnter2D(Collision2D other){
//
//		if(other.collider.CompareTag("Right")){
//			Debug.Log("You collided right");
//		} else {
//			Debug.Log("Didn't work");
//		}
//	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		transform.position = player.transform.position + offset;

//		float camVertExtent = cam.orthographicSize;
//		float camHorzExtent = cam.aspect * camVertExtent;
//
//		float leftBound = bounds.min.x + camHorzExtent;
//		float rightBound = bounds.max.x - camHorzExtent;
//		float bottomBound = bounds.min.y + camVertExtent;
//		float topBound = bounds.max.y - camVertExtent;
//
////		(cam.transform.position.x - camHorzExtent) >= bounds.min.x; //left
////		(cam.transform.position.x + camHorzExtent) <= bounds.max.x; //right
////		(cam.transform.position.y - camVertExtent) >= bounds.min.y; //bottom
////		(cam.transform.position.y + camVertExtent) <= bounds.max.y; //top
//
//		float camX = Mathf.Clamp(player.transform.position.x, leftBound, rightBound);
//		float camY = Mathf.Clamp(player.transform.position.y, bottomBound, topBound);
	}
}
