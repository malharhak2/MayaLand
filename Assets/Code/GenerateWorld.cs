using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class GenerateWorld : MonoBehaviour {

	public Transform floor;
	public Transform tree;
	public Transform enemy;
	public MapCase[,] map;
	public float unitSize = 1;

	public int levelWidth = 10;
	public int levelHeight = 10;
	// Use this for initialization
	void Start () {
		map = new MapCase[levelWidth, levelHeight];
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				Transform fl = Instantiate (floor, new Vector3(i - levelWidth/2, j - levelHeight/2, levelHeight + 1), Quaternion.identity) as Transform;
				fl.parent = transform;
				map[i, j] = MapCase.Floor;
				if (Random.Range (0, 1f) > 0.9f) {
					Transform f2 = Instantiate (tree, new Vector3(i - levelWidth / 2, j - levelHeight / 2, j - levelHeight / 2), Quaternion.identity) as Transform;;
					f2.parent = transform;
					map[i, j] = MapCase.Wall;
				} else if (Random.Range (0, 1f) > 0.995f) {
					//Transform f3 = Instantiate (enemy, new Vector3(i - levelWidth / 2, j - levelHeight / 2, 0), Quaternion.identity) as Transform;
					//f3.parent = transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MapCase readPosition (float dX, float dY) {
		int x = (int) (unitSize * (dX + levelWidth / 2) + 0.5);
		int y = (int) (unitSize * (dY + levelWidth / 2) + 0.5);
		if (map.Length > x && map.GetLength (0) > y) {
			return map[x, y];
		} else {
			return MapCase.Empty;
		}
	}
}
