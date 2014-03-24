using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {



	public float timeScale;



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
