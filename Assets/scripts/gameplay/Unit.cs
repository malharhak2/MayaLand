using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxLife;
	protected float life;
	public float lifePart;

	public int maxStamina;
	protected float stamina;
	public float staminaPart;
	public AttackTeam team;
	public int id;
	public Transform gameSpells;
	public float attackSpeed = 1;

	[SerializeField]
	public Spells[] usableSpells;

	protected Spell[] spellsInfo;
	[SerializeField]
	protected float moveSpeed;
	[SerializeField]
	protected float rotationSpeed;
	protected UnitController charController;


	public SpellsList spellsList;
	private float lastSpell;
	private float lastSpellTimer;
	private bool isCasting = false;
	// Use this for initialization
	public virtual void Start () {
		charController = GetComponent<UnitController>();
		life = maxLife;
		stamina = maxStamina;
		spellsList = gameSpells.GetComponent<SpellsList>();
		Debug.Log (spellsList.ToString ());
		Debug.Log (typeof (SpellsList));
	}
	public virtual void Awake() {

	}
	public void StartRunning () {
		charController.StartRunning ();
	}
	public void StopMoving() {
		charController.StopMoving();
	}
	public void SetMoveSpeed (float speed) {
		charController.SetMoveSpeed(speed);
	}
	public void SetDirection (Vector3 direction) {
		charController.SetDirection (direction);
	}
	// Update is called once per frame
	public virtual void Update () {
		if (isCasting) {
			if (Time.time - lastSpell > lastSpellTimer) {
				Debug.Log ("Stopping spell");
				StopCasting();
			}
		}
	}
	public void CastSpell (Spells spell) {
		if (!isCasting) {
			Spell spellScript = GetSpell (spell).GetComponent<Spell>();
			lastSpellTimer = spellScript.castTime / attackSpeed;
			lastSpell = Time.time;
			UseAttack (spell);
			charController.StartCasting ();
			isCasting = true;
			Debug.Log ("Use Spell " + spell.ToString () + " For " + lastSpellTimer.ToString () + "s");
		}
	}
	public void StopCasting () {
		isCasting = false;
		charController.StopCasting ();
	}
	public virtual void LateUpdate () {
		if (life > maxLife) {
			life = maxLife;
		}
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
		lifePart = life / maxLife;
		staminaPart =  stamina / maxStamina;
	}

	public virtual void Hit (Spell spell) {
		if (spell.attackTeam != team) {
			life -= spell.damages;
		}
	}
	public Transform GetSpell (Spells spl) {
		return spellsList.spellsPrefabs[spl];
	}
	public void UseAttack (Spells spl) {
		Transform spellPrefab = GetSpell (spl);
		Transform instantiated = Instantiate (spellPrefab, transform.position, transform.rotation) as Transform;
		//instantiated.parent = transform;
		Spell spellScript = instantiated.GetComponent<Spell>();
		spellScript.attackTeam = team;
		spellScript.attackerId = id;
		Buff buffScript = instantiated.GetComponent<Buff>();
		if (buffScript != null) {
			buffScript.target = transform;
		}
		
	}
}
