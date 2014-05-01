using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class blocksList {

	static blocksList () {

		directions.Add (BlockSide.Back, new Vector3(0, 0, -1));
		directions.Add (BlockSide.Front, new Vector3(0, 0, 1));
		directions.Add (BlockSide.Bot, new Vector3(0, -1, 0));
		directions.Add (BlockSide.Top, new Vector3(0, 1, 0));
		directions.Add (BlockSide.Left, new Vector3(-1, 0, 0));
		directions.Add (BlockSide.Right, new Vector3(1, 0, 0));
		Block air =  new Block();
		air.render = false;
		air.collide = false;
		air.solid = false;

		blocks.Add (BlockType.Air, air);

		Block stone = new Block();
		stone.render = true;
		stone.collide = true;
		stone.SetTextures (tStone);
		blocks.Add (BlockType.Stone, stone);

		Block dirt = new Block();
		dirt.render = true;
		dirt.collide = true;
		dirt.SetTextures (tGrassTop);
		blocks.Add (BlockType.Dirt, dirt);

		Block grass = new Block();
		grass.render = true;
		grass.collide = true;
		grass.SetTextures (tGrassTop, tGrassTop, tGrassTop, tGrassTop);
		blocks.Add (BlockType.Grass, grass);
	}

	public static Dictionary<BlockType, Block> blocks = new Dictionary<BlockType, Block>();
	
	private static Vector2 tStone = new Vector2(1, 0);
	private static Vector2 tGrass = new Vector2(2, 0);
	private static Vector2 tGrassTop = new Vector2(0, 0);

	public static Dictionary<BlockSide, Vector3> directions = new Dictionary<BlockSide, Vector3>();

}