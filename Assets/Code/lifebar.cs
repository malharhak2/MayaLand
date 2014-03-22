using UnityEngine;
using System.Collections;

public class lifebar : MonoBehaviour {

	public GUISkin skin;
	public Rect lifebarRect;
	public Texture2D texture;

	private Vector3 screenPosition;
	private Stats stats;
	public Vector2 offset;
	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		stats = GetComponent<Stats>();
	}
	// Update is called once per frame
	void Update () {
		screenPosition = Camera.main.WorldToViewportPoint(transform.position);
		screenPosition.x *= Screen.width;
		screenPosition.y = 1 - screenPosition.y;
		screenPosition.y *= Screen.height;		
	}

	void OnGUI () {
		float healthPart = stats.health / stats.maxHealth;
		Rect actualHealth = new Rect(screenPosition.x + offset.x, screenPosition.y + offset.y, lifebarRect.width * healthPart, lifebarRect.height);
		GUI.BeginGroup(actualHealth, texture);
		Rect innerRect = new Rect(0, 0, lifebarRect.width, lifebarRect.height);
		GUI.DrawTexture (innerRect, texture);
		GUI.EndGroup ();
	}
}
