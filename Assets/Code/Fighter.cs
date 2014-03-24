using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	public Transform weapon;
	public int maxCombo = 3;
	public float[] attackTimes;
	public float[] attacksEnd;
	public float endComboTime = 0.5f;

	private int comboLvl = -1;
	private float lastAttack = -1;
	private bool wantsToAttack = false;
	private float lastComboEnd = 0;

	private Animator animator;
	private Weapon wpn;
	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		wpn = weapon.GetComponent<Weapon>();
		animator = GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		attack ();
	}

	void attack() {
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

	public void attackInput () {
		if (Time.time - lastComboEnd >= endComboTime) {
			wantsToAttack = true;
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

}
