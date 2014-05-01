using UnityEngine;
using System.Collections;

public class Player : Unit {

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
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		if (hor != 0 || ver != 0) {
			charController.StartRunning();
			charController.SetMoveSpeed (new Vector2(hor, ver).magnitude * moveSpeed);
		} else {
			charController.StopMoving();
		}
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
