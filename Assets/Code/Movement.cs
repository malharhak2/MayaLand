using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Movement : MonoBehaviour {

	public float speed;
	public float runSpeed;
	public float runTime = 2;
	public Vector3 direction;
	public Transform level;

	private Vector3 previousPosition;
	private bool running = false;
	private bool moving = false;
	private float moveStart;
	private GenerateWorld world;
	private Animator animator;
	// Use this for initialization
	void Start () {
		direction = Vector3.zero;
	}

	void Awake() {
		animator = GetComponent<Animator>();
		previousPosition = transform.position;
		world = level.GetComponent<GenerateWorld>();
	}
	// Update is called once per frame
	void Update () {
		previousPosition = transform.position;
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
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
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
			Vector3 newPosition = new Vector3(0, 0, 0);
			float spd = running ? runSpeed : speed;
			Vector3 moveAmount = transform.up * spd * Time.deltaTime;
			if (!checkCollisions (transform.position.x + moveAmount.x, transform.position.y)) {
				newPosition.x = moveAmount.x;
			}
			if (!checkCollisions (transform.position.x, transform.position.y + moveAmount.y)) {
				newPosition.y = moveAmount.y;
			}
			transform.Translate (newPosition, Space.World);
		}
	}

	bool checkCollisions (float x, float y) {
		if (world.readPosition (x, y) == MapCase.Wall) {
			return true;
		} else {
			return false;
		}
	}
}
