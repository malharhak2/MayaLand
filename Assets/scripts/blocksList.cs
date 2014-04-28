using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class blocksList {

	static blocksList () {

		Block air =  new Block();
		air.render = false;
		air.collide = false;

		blocks.Add (BlockType.Air, air);

		Block stone = new Block();
		stone.render = true;
		stone.collide = true;
		stone.SetTextures (tStone);
		blocks.Add (BlockType.Stone, stone);

		Block dirt = new Block();
		dirt.render = true;
		dirt.collide = true;
		dirt.SetTextures (tGrass);
		blocks.Add (BlockType.Dirt, dirt);

		Block grass = new Block();
		grass.render = true;
		grass.collide = true;
		grass.SetTextures (tGrass, tGrassTop, tGrass);
		blocks.Add (BlockType.Grass, grass);
	}

	public static Dictionary<BlockType, Block> blocks = new Dictionary<BlockType, Block>();
	
	private static Vector2 tStone = new Vector2(1, 0);
	private static Vector2 tGrass = new Vector2(0, 1);
	private static Vector2 tGrassTop = new Vector2(1, 1);

}