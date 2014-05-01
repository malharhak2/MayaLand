using UnityEngine;
using System.Collections;

public class Player : Unit {

	public int maxGold;
	private int gold;

	public int maxHunger;
	private float hunger;

	public int hungerPerGold;
	public int hungerPerSecond;
	// Use this for initialization
	void Start () {
		hunger = maxHunger;
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= hungerPerSecond * Time.deltaTime;
	}

	void OnPickupGold (int amount) {
		gold += amount;
		hunger += (hungerPerGold * amount);
	}

	void LateUpdate () {
		if (hunger > maxHunger) {
			hunger = maxHunger;
		}
		if (life > maxLife) {
			life = maxLife;
		}
	}
}
