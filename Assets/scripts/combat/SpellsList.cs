using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellsList : MonoBehaviour {

	public Dictionary<Spells, Transform> spellsPrefabs;
	public SpellsListAttribute[] list;
	void Start () {
		spellsPrefabs = new Dictionary<Spells, Transform>();
		for (int i = 0; i < list.Length; i++) {
			spellsPrefabs.Add (list[i].spell, list[i].prefab);
		}
	}


}
