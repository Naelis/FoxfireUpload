using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	//Game object/component reference.
	public Transform[] layers;

	/// <summary>
	/// Parallax variables.
	/// </summary>
	private float[] parallaxScales;
	public float smoothing; 

	//Last camera position.
	private Vector3 previousCameraPosition;

	void Start () {
		previousCameraPosition = transform.position;

		parallaxScales = new float[layers.Length];
		for (int i = 0; i < parallaxScales.Length; i++) {
			parallaxScales [i] = layers [i].position.z * -1;
		}

	}
	/// <summary>
	/// Background layers are moved, when the camera moves, doing so at a set pace depending on their position on the Z -scale.
	/// </summary>
	void LateUpdate () {
		for (int i = 0; i < layers.Length; i++) {
			Vector3 parallax = (previousCameraPosition - transform.position) * (parallaxScales [i] / smoothing);

			layers [i].position = new Vector3 (layers[i].position.x + parallax.x * 0.1f, layers[i].position.y, layers[i].position.z);
		}

		previousCameraPosition = transform.position;
	}
}
