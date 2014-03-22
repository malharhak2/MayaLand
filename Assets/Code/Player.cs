using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform weapon;
	public int maxCombo = 3;
	public float[] attackTimes;
	public float[] attacksEnd;
	public float endComboTime = 0.5f;

	public float timeScale;


	private int comboLvl = -1;
	private float lastAttack = -1;
	private bool wantsToAttack = false;
	private float lastComboEnd = 0;
	private Animator animator;
	private Weapon wpn;
	private Movement movement;
	// Use this for initialization
	void Start () {
		wpn = weapon.GetComponent<Weapon>();
	}

	void Awake () {
		animator = GetComponent<Animator>();
		movement = GetComponent<Movement>();
	}
	// Update is called once per frame
	void Update () {
		movement.direction = new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);


		if (Time.time - lastComboEnd >= endComboTime) {
			attack ();
		}
		Time.timeScale = timeScale;
	}
	void attack() {
		if (Input.GetButtonDown ("X")) {
			wantsToAttack = true;
		}
		if (comboLvl != -1 && Time.time - lastAttack >= attacksEnd[comboLvl]) {
			endCombo();
			return;
		}
		if (comboLvl != -1 && Time.time - lastAttack >= attackTimes[comboLvl]) {
			stopAttack();
		}
		if (wantsToAttack && (comboLvl == -1 || Time.time - lastAttack >= attackTimes[comboLvl])) {
			comboLvl++;
			if (comboLvl >= 3) {
				endCombo();
				return;
			}
			animator.SetTrigger("Attack" + (comboLvl + 1).ToString());
			Debug.Log ("ATTACK " + (comboLvl + 1).ToString());
			wantsToAttack = false;
			lastAttack = Time.time;
			startAttack ();
		}
	}

	void startAttack() {
		wpn.startAttack(comboLvl + 1);
	}
	void stopAttack() {
		wpn.stopAttack();
	}
	void finishCombo () {
		lastComboEnd = Time.time;
		endCombo ();
	}
	void endCombo () {
		comboLvl = -1;
		wantsToAttack = false;
		stopAttack();
	}


	void OnGUI () {
		GUI.Label (new Rect(10f, 10f, 200f, 20f), "Last : " + (Mathf.Floor((Time.time - lastAttack) * 100) / 100).ToString());
	}
}
