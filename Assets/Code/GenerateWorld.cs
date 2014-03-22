using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {

	public Transform floor;
	public int levelWidth = 10;
	public int levelHeight = 10;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				Transform fl = Instantiate (floor, new Vector3(i - levelWidth/2, j - levelHeight/2, 0), Quaternion.identity) as Transform;
				fl.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
