using UnityEngine;
using System.Collections;

public class CharacterControls : MonoBehaviour {

	public float maxVelocity;
	public float velocityIncrement;
	public float jumpForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = new Vector3(Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")).normalized;
		direction = transform.TransformDirection(direction);
		if (rigidbody.velocity.magnitude < maxVelocity) {
			rigidbody.AddForce(direction * velocityIncrement * Time.deltaTime);
		}
		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}
	}

	void Jump () {
		rigidbody.AddForce (new Vector3(0, jumpForce, 0));
	}
}
