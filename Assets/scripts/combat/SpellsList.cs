using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellsList : MonoBehaviour {

	public Dictionary<Spells, Transform> spellsPrefabs;
	public SpellsListAttribute[] list;
	public Dictionary<Spells, Spell> spellsScripts;
	void Start () {
		spellsPrefabs = new Dictionary<Spells, Transform>();
		spellsScripts = new Dictionary<Spells, Spell>();
		for (int i = 0; i < list.Length; i++) {
			spellsPrefabs.Add (list[i].spell, list[i].prefab);
			Spell script = list[i].prefab.GetComponent<Spell>();
			spellsScripts.Add (list[i].spell, script);
		}
	}
	public Spell getScript (Spells spell) {
		return spellsScripts[spell];
	}

}
