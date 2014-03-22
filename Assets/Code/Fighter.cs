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

	void awake() {
		wpn = weapon.GetComponent<Weapon>();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
