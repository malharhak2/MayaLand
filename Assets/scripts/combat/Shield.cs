using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	private Buff buffScript;
	// Use this for initialization
	void Start () {
		buffScript = GetComponent<Buff>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = buffScript.target.position;
	}
}
