using UnityEngine;
using System.Collections;

public class InterfacePlayer : MonoBehaviour {
	
	private static Texture2D _staticRectTexture;
	private static GUIStyle _staticRectStyle;
	private Color color;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("LOOOOOOOOOOl");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		GameObject player = GameObject.Find("Player");
		Player joueur = player.GetComponent<Player> (); // on peut ainsi récupérer les infos du player
		int life = joueur.life;
		int stamina = joueur.stamina;
		int money = joueur.money;
		
		GUI.Box (new Rect (5, 5, 270, 70), "");
		GUI.Label (new Rect (35, 5, 50, 50), "Life :");
		GUI.Label (new Rect (7, 30, 60, 50), "Stamina :");
		
		color = new Color (1.0F, 0.3F, 0.3F);
		GUIDrawRect (new Rect (70, 8, 20 * life, 15), color);
		color = new Color (0.2F, 3.0F, 0.4F);
		GUIDrawRect (new Rect (70, 33, stamina * 2, 15), color);
		
		
		GUI.Label (new Rect (16, 55, 100, 50), "Money :  " + money);
	}
	
	public static void GUIDrawRect( Rect position, Color color ) 
	{		
		if( _staticRectTexture == null )			
		{			
			_staticRectTexture = new Texture2D( 1, 1 );			
		}						
		if( _staticRectStyle == null )			
		{			
			_staticRectStyle = new GUIStyle();			
		}						
		_staticRectTexture.SetPixel( 0, 0, color );		
		_staticRectTexture.Apply();						
		_staticRectStyle.normal.background = _staticRectTexture;						
		GUI.Box( position, GUIContent.none, _staticRectStyle );
	}
}
