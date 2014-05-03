using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	// Use this for initialization
	void Start () {
		//direction = transform.forward;
	}
	void Awake() {
		direction = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (	direction * speed * Time.deltaTime, Space.World);
	}
}
