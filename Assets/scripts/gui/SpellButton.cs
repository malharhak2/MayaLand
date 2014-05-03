using UnityEngine;
using System.Collections;

public class SpellButton : MonoBehaviour{
	public string name;
	public string description;
	public string icon;
	public string shortcut;

	public void UpdateSpell (string name, string description, string icon, string shortcut) {
		this.name = name;
		this.description = description;
		this.icon = icon;
		this.shortcut = shortcut;
	}
}
