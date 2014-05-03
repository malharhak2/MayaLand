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

	public float runCostPerSecond;

	public float rollTime;
	public float rollCost;
	public float rollSpeed;
	private float lastRoll;
	private bool rollInput = false;
	private bool rolling = false;
	public float rollDeadZone = 0.5f;

	public Transform[] spellButtons;
	public Spells[] spellsBar;
	public int spellBarSize = 3;

	// Use this for initialization
	public override void Start () {
		base.Start();
		team = AttackTeam.Players;
		hunger = maxHunger;
		charController.SetMoveSpeed (moveSpeed);
		charController.StartRunning();
		charController.rotateSpeed = rotationSpeed;
	}
	public override void Awake() {
		base.Awake();
	}
	public void ChangeSpell (int id, Spells spell) {
		spellsBar[id] = spell;
		Spell infos = spellsList.getScript (spell);
		Transform btnTr = spellButtons[id];
		SpellButton btn = spellButtons[id].GetComponent<SpellButton>();
		btn.description = infos.description;
		btn.name = infos.name;
		btn.icon = infos.mSpriteName;
		btn.shortcut = id.ToString ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		hunger -= hungerPerSecond * Time.deltaTime;
		stamina += staminaPerSecond * Time.deltaTime;
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		if (hor != 0 || ver != 0) {
			GamepadMove(hor, ver);
		} else {
			StopMoving();
		}
		CheckRoll ();
		if (Input.GetKeyDown (KeyCode.A)) {
			ChangeSpell (1, Spells.Buff_1);
		}
		if (Input.GetMouseButton (0)) {
			Vector3 direction = GetDirectionFromMouse();
			SetDirection (direction);
			SetMoveSpeed (moveSpeed);
			StartRunning ();
		}
		if (Input.GetMouseButton (1)) {
			Vector3 direction = GetDirectionFromMouse();
			SetDirection(direction);
			CastSpell (spellsBar[4]);
		}
	}
	Vector3 GetDirectionFromMouse() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			Debug.DrawLine (transform.position, hit.point);
			Vector3 direction = hit.point - transform.position;
			direction.y = 0;
			return direction.normalized;
		} else {
			return Vector3.zero;
		}
	}
	void GamepadMove (float hor, float ver) {
		Vector3 forward= Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right= new Vector3(forward.z, 0, -forward.x);
		
		float v= ver;
		float h= hor;
		
		// Target direction relative to the camera
		Vector3 targetDirection= h * right + v * forward;
		SetDirection (targetDirection);
		SetMoveSpeed (new Vector2(hor, ver).magnitude * moveSpeed);
		StartRunning ();
	}
	public void CastFirstSpell () {
		CastSpell (spellsBar[0]);
	}
	public void CastSecondSpell () {
		CastSpell (spellsBar[1]);
	}
	public void CastThirdSpell () {
		CastSpell (spellsBar[2]);
	}
	void GamepadRoll () {
		float rH = Input.GetAxis("RightH");
		float rV = -Input.GetAxis ("RightV"); // On check le stick droit
		Vector3 rollDir = transform.forward;
		if (rH != 0 || rV != 0 && !rollInput) {
			rollDir = new Vector3(rH, 0, rV).normalized;
		}
		Roll (rollDir);
	}
	void Roll (Vector3 rollDir) {
		if (!rollInput && stamina >= rollCost) {
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
				GamepadRoll ();
			} else if (Input.GetKeyDown (KeyCode.Q)) {
				Roll (GetDirectionFromMouse());
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
		if (life < 0 || hunger < 0) {
			Die();
		}
	}

	void Die () {
		Application.LoadLevel ("dead");
	}
}
