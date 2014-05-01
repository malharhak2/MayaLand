using UnityEngine;
using System.Collections;

public class Player : Unit {

	public float staminaPerSecond = 1.0f;
	public int maxGold;
	private int gold;

	public int maxHunger;
	private float hunger;
	public float hungerPart;

	public int hungerPerGold;
	public int hungerPerSecond;

	public float moveSpeed;
	public float rotationSpeed;

	public float runCostPerSecond;

	public float rollTime;
	public float rollCost;
	public float rollSpeed;
	private float lastRoll;
	private bool rollInput = false;
	private bool rolling = false;
	public float rollDeadZone = 0.5f;
	private ThirdPersonController charController;
	// Use this for initialization
	public override void Start () {
		base.Start();
		hunger = maxHunger;
		charController = GetComponent<ThirdPersonController>();
		charController.SetMoveSpeed (moveSpeed);
		charController.StartRunning();
		charController.rotateSpeed = rotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= hungerPerSecond * Time.deltaTime;
		stamina += staminaPerSecond * Time.deltaTime;
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		if (hor != 0 || ver != 0) {
			charController.StartRunning();
			charController.SetMoveSpeed (new Vector2(hor, ver).magnitude * moveSpeed);
		} else {
			charController.StopMoving();
		}
		CheckRoll ();

	}

	void Roll () {
		if (!rollInput && stamina >= rollCost) {
			float rH = Input.GetAxis("RightH");
			float rV = -Input.GetAxis ("RightV"); // On check le stick droit
			Vector3 rollDir = transform.forward;
			if (rH != 0 || rV != 0 && !rollInput) {
				rollDir = new Vector3(rH, 0, rV).normalized;
			}
			rollInput = true;
			if (!rolling) {
				if (charController.StartRolling (rollDir, rollSpeed)) {
					lastRoll = Time.time;
					rolling = true;
					stamina -= rollCost;
				}
			}
		}
	}

	void CheckRoll () {
		if (!rolling) {
			if (Mathf.Abs (Input.GetAxis ("RightH")) > rollDeadZone
			    || Mathf.Abs (Input.GetAxis ("RightV")) > rollDeadZone) {
				Roll ();
			} else {
				rollInput = false;
			}
		}
		if (rolling) {
			if (Time.time - lastRoll > rollTime) {
				EndRoll ();
			}
		}
	}
	void EndRoll () {
		rolling = false;
		charController.StopRolling ();
	}
	void OnPickupGold (int amount) {
		gold += amount;
		hunger += (hungerPerGold * amount);
	}

	public override void LateUpdate () {
		base.LateUpdate();
		if (hunger > maxHunger) {
			hunger = maxHunger;
		}
		hungerPart = hunger / maxHunger;
	}
}
