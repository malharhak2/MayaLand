using UnityEngine;
using System.Collections;

public class DeadMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play () {
		Application.LoadLevel("testGen");
	}
	public void Menu () {
		Application.LoadLevel("menu");
	}

	public void TerrainPlay () {
		Application.LoadLevel("terrain");
	}
}
