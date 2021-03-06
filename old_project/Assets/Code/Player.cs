using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing.Color;

public class Player : MonoBehaviour {

	public int life = 10; 
	public int stamina = 100;
	public int money = 100;

	public float timeScale;

	public List<Stats> stats = new List<Stats> ();

	private Animator animator;
	private Movement movement;
	private Fighter fighter;
	// Use this for initialization
	void Start () {
	}

	void Awake () {
		animator = GetComponent<Animator>();
		movement = GetComponent<Movement>();
		fighter = GetComponent<Fighter>();
	}
	// Update is called once per frame
	void Update() {
		movement.direction = new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		if (Input.GetButtonDown ("X")) {
			fighter.attackInput();
		}
	}

}



