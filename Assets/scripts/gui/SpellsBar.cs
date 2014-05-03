using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpellsBar : MonoBehaviour {

	public Transform player;
	public Player playerScript;
	public Spells[] spellsList;
	public Transform[] spellButtons;

	public int barSize = 4;
	// Use this for initialization
	void Start () {
		playerScript = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ChangeSpell(int slot, Spells spell, Spell infos) {
		if (slot < barSize) {
			spellsList[slot] = spell;
			Transform icon = spellButtons[slot].FindChild("SpellIcon");
			Transform label = spellButtons[slot].FindChild ("SpellShortcut");

			UISprite sprite = icon.GetComponent<UISprite>();
			sprite.spriteName = infos.mSpriteName;
			return true;
		} else {
			return false;
		}
	}
}
