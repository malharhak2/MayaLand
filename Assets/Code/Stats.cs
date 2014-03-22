using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int maxHealth;
	public int maxStamina;

	public float health;
	public float stamina;
	// Use this for initialization
	void Start () {
		health = maxHealth;
		stamina = maxStamina;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			die ();
		}
	}

	void ApplyDamage(float damage) {
		health -= damage;
		if (health < 0) {
			health = 0;
			die();
		}
	}
	void OnGUI () {
		GUI.Label (new Rect(20f, 20f, 200f, 20f), "life : " + health);
	}

	void die () {
		Destroy(gameObject);
	}
}
