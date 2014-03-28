using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Enemy : MonoBehaviour {

	public float pushTime = 80;
	public float pushSpeed = 5;
	public float pushLvl = 1;
	public float maxDistance = 5;
	public float minTimeBetweenMoves = 0.5f;
	public float maxTimeBetweenMoves = 3f;
	public float minMoveDuration = 0.1f;
	public float maxMoveDuration = 1f;

	private bool pushed = false;
	private float lastPush = 0;
	private Vector3 pushDirection;
	private Vector3 home;

	private float currentMove;
	private float nextMove;
	private float moveEnd;

	private bool moving = false;
	private bool tracking = false;
	private Vector3 moveDirection;
	private float moveDuration;

	private Movement movement;
	// Use this for initialization
	void Start () {
	}

	void Awake() {
		home = transform.position;
		movement = GetComponent<Movement>();
	}

	void startMove () {
		moveDuration = chooseMoveDuration ();
		moving = true;
		moveDirection = new Vector3(Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0).normalized;
		moveEnd = Time.time + chooseMoveDuration ();
	}
	void stopMove () {
		moving = false;
		nextMove = Time.time + chooseNextMove ();
	}
	float chooseNextMove() {
		return Random.Range (minTimeBetweenMoves, maxTimeBetweenMoves);
	}
	float chooseMoveDuration() {
		return Random.Range (minMoveDuration, maxMoveDuration);
	}
	// Update is called once per frame
	void Update () {
		isPushed();
		move();
	}

	void move () {
		if (!moving && Time.time > nextMove) {
			startMove();
		} else if (moving && Time.time > moveEnd) {
			stopMove ();
		}
		if (moving) {
			movement.direction = moveDirection;
		} else {
			movement.direction = Vector3.zero;
		}
	}

	void isPushed () {
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
