using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform cameraHolder;
	public float angle;
	public float distance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		cameraHolder.localRotation = Quaternion.Euler (angle, 0, 0);
		transform.localPosition = new Vector3(0, 0, -distance);
	}
}
