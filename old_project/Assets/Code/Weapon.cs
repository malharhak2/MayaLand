using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Weapon : MonoBehaviour {

	public float damage;
	public float pushLvl;
	public bool attacking = false;
	public bool firstAttack = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("Collision 2D");
		if (other.gameObject.tag == "Enemy") {
			if (attacking) {
				attack (other); 
			}
		}

	}

	void OnTriggerInside2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy" && firstAttack) {
			firstAttack = false;
			attack (other);
		}
	}
	public void startAttack (float pushLvl) {
		this.pushLvl = pushLvl;
		attacking = true;
		firstAttack = true;
	}
	public void stopAttack() {
		attacking = false;
		firstAttack = false;
	}
	void attack (Collider2D other) {
		Vector3 contact = transform.position;
		Debug.Log ("Hit an enemy");
		other.gameObject.SendMessage ("ApplyDamage", damage);
		other.gameObject.SendMessage ("PushObject", new PushArgs(contact, pushLvl));
	}

	void OnTriggerExit2D (Collider2D other) {

	}
}
