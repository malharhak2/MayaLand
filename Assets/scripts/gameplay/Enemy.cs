using UnityEngine;
using System.Collections;

public class Enemy : Unit {

	private bool isChasing = false;
	private Transform chasedPlayer;

	[Range (0.2f, 1f)] 	public float minMoveTime = 0.2f;
	[Range (1f, 10f)] 	public float maxMoveTime = 2f;
	[Range (0f, 1f)]	public float minMoveDelay = 0.1f;
	[Range (1f, 20f)] 	public float maxMoveDelay  = 15f;
	private bool isMovingAround = false;
	private float moveTime;
	private float moveDelay = 0f;
	private float lastMove;
	private float lastMoveEnd;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		team = AttackTeam.Enemies;
		lastMove = Time.time;
		lastMoveEnd = Time.time;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (!isChasing) {
			MoveAround();
		} else {
			Chase();
		}
	}
	void Chase () {

	}
	void MoveAround () {
		if (isMovingAround) {
			if (Time.time - lastMove > moveTime) {
				StopMovingAround();
			}
		} else {
			if (Time.time - lastMoveEnd > moveDelay) {
				StartMovingAround();
			}
		}
	}
	void StartMovingAround() {
		moveTime = Random.Range (minMoveTime, maxMoveTime);
		lastMove = Time.time;
		Vector3 direction = randomDirection();
		charController.SetDirection(direction);
		charController.StartRunning ();
		charController.SetMoveSpeed (moveSpeed);
		isMovingAround = true;
	}
	void StopMovingAround () {
		moveDelay = Random.Range (minMoveDelay, maxMoveDelay);
		lastMoveEnd = Time.time;
		charController.StopMoving();
		isMovingAround = false;
	}
	void FoundPlayer (Transform player) {
		if (!isChasing) {
			StartChasing (player);
		}
	}
	void StartChasing (Transform player) {
		chasedPlayer = player;
		isChasing = true;
		Debug.Log ("Starts chasing " + player.name);
	}
	void StopChasing () {
		isChasing = false;
	}
	Vector3 randomDirection () {
		return new Vector3 (Random.Range (-1, 1), 0, Random.Range (-1, 1)).normalized;
	}
}
