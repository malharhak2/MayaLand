using UnityEngine;
using System.Collections;

public class AggroZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTrigerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			transform.parent.SendMessage ("FoundPlayer", other.transform);
		}
	}
}
