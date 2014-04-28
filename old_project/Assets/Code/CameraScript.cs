using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	public Transform attachedTo;
	// Use this for initialization
	void Start () {
	
	}
	void Awake() {
		resetPosition();
	}
	
	// Update is called once per frame
	void Update () {
		updatePosition();

	}

	void scroll() {
		Debug.Log ("Scroll");
	}

	void resetPosition () {
		transform.position = new Vector3(attachedTo.position.x, attachedTo.position.y, -10f);
	}
	void updatePosition () {
		transform.position = new Vector3(attachedTo.position.x, attachedTo.position.y, -10f);
	}
}
