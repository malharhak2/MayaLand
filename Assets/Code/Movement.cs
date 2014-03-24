using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	public float runSpeed;
	public float runTime = 2;
	public Vector3 direction;

	private bool running = false;
	private bool moving = false;
	private float moveStart;

	private Animator animator;
	// Use this for initialization
	void Start () {
		direction = Vector3.zero;
	}

	void Awake() {
		animator = GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log ("dir" + direction.ToString());
		if (direction.magnitude > 0.1) {
			startMoving();
		} else {
			stopMoving();
		}
		if (moving && !running && Time.time - moveStart >= runTime) {
			startRunning();
		}
		move ();
	}
	
	void startMoving () {
		if (!moving) {
			moving = true;
			moveStart = Time.time;
			animator.SetBool("Moving", true);
		}
	}
	
	void stopMoving () {
		moving = false;
		stopRunning();
		animator.SetBool ("Moving", false);
	}
	
	void startRunning () {
		running = true;
		animator.SetBool ("Running", true);
	}
	void stopRunning () {
		running = false;
		animator.SetBool ("Running", false);
	}
	
	void move () {
		if (moving) {
			transform.up = direction;
			float spd = running ? runSpeed : speed;
			transform.Translate (new Vector3(0, spd * Time.deltaTime, 0));
		}
	}
}
