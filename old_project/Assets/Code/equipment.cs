using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class equipment : MonoBehaviour {
	
	public int durabilite = 10; 
	
	public enum slot
	{
		torso,
		legs,
		feet,
		hands,
		head
	};
	
	public string name;
	
	public enum quality
	{
		Pourri,
		Commun,
		Rare,
		Epique,
		Legendaire
	};

	public List<Stats> stats = new List<Stats> ();
	
	// Use this for initialization
	void Start () {
		Debug.Log ("equipment");
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void addStats(Stats newStat)
	{
		stats.Add (newStat);	
	}
	
	public void equiper(Player player)
	{
		
	}
}
