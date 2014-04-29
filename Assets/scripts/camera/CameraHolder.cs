using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraHolder : MonoBehaviour {

	public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position;
	}
}
