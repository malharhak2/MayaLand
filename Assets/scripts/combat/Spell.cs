﻿using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	
	public string name;
	public string description;
	[HideInInspector][SerializeField] 
	public UIAtlas mAtlas;
	 
	public string mSpriteName;

	public float castTime;
	public float damages;
	public float duration;
	private float startTime;
	public int attackerId;
	public AttackTeam attackTeam;
	// Use this for initialization
	void Start () {
		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision collision) {
		Debug.Log ("Collision with" + collision.gameObject.name);
	}
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Trigger enter " + other.name);
		other.SendMessage ("Hit", this);
	}

}
