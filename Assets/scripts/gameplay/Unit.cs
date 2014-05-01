using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int maxLife;
	public float life;
	public float lifePart;

	public int maxStamina;
	public float stamina;
	public float staminaPart;

	// Use this for initialization
	public virtual void Start () {
		life = maxLife;
		stamina = maxStamina;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
