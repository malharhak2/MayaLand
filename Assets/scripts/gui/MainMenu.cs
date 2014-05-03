using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame () {
		Application.LoadLevel("testGen");
	}
	public void TerrainPlay () {
		Application.LoadLevel ("terrain");
	}
	public void PrebuiltPlay () {
		Application.LoadLevel("prebuilt");
	}
	public void Menu () {
		Application.LoadLevel ("menu");
	}
	public void Quit () {
		Application.Quit();
	}
}
