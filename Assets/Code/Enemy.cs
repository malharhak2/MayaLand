using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Enemy : MonoBehaviour {

	public float pushTime = 80;
	public float pushSpeed = 5;
	public float pushLvl = 1;
	private bool pushed = false;
	private float lastPush = 0;
	private Vector3 pushDirection;

	// Use this for initialization
	void Start () {
		Debug.Log ("coucou");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastPush >= pushTime) {
			pushed = false;
		}
		if (pushed) {
			Debug.Log ("Push" + pushSpeed);
			transform.Translate (pushDirection * pushSpeed * pushLvl * Time.deltaTime);
		}
	}

	void PushObject (PushArgs args) {
		Vector3 direction =  transform.position - args.point;
		pushDirection = direction.normalized;
		pushLvl = args.pushLvl;
		Debug.Log ("X" + direction.x.ToString ());
		pushed = true;
		lastPush = Time.time;
	}
}
