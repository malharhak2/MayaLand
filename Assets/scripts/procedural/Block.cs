using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block {

	public Dictionary<BlockSide, Vector2> faces = new Dictionary<BlockSide, Vector2>();

	public bool render = true;
	public bool collide = true;
	public bool solid = true;

	public void SetTextures (Vector2 tex) {
		faces.Add (BlockSide.Bot, tex);
		faces.Add (BlockSide.Top, tex);
		faces.Add (BlockSide.Back, tex);
		faces.Add (BlockSide.Front, tex);
		faces.Add (BlockSide.Left, tex);
		faces.Add (BlockSide.Right, tex);
		faces.Add (BlockSide.FrontRight, tex);
		faces.Add (BlockSide.FrontLeft, tex);
		faces.Add (BlockSide.BackRight, tex);
		faces.Add (BlockSide.BackLeft, tex);
	}

	public void SetTextures (Vector2 sidesTex, Vector2 topTex, Vector2 botTex) {
		faces.Add (BlockSide.Bot, botTex);
		faces.Add (BlockSide.Top, topTex);
		faces.Add (BlockSide.Back, sidesTex);
		faces.Add (BlockSide.Front, sidesTex);
		faces.Add (BlockSide.Left, sidesTex);
		faces.Add (BlockSide.Right, sidesTex);
		faces.Add (BlockSide.FrontRight, sidesTex);
		faces.Add (BlockSide.FrontLeft, sidesTex);
		faces.Add (BlockSide.BackRight, sidesTex);
		faces.Add (BlockSide.BackLeft, sidesTex);
	}
	public void SetTextures (Vector2 sidesTex, Vector2 topTex, Vector2 botTex, Vector2 cornerTex) {
		faces.Add (BlockSide.Bot, botTex);
		faces.Add (BlockSide.Top, topTex);
		faces.Add (BlockSide.Back, sidesTex);
		faces.Add (BlockSide.Front, sidesTex);
		faces.Add (BlockSide.Left, sidesTex);
		faces.Add (BlockSide.Right, sidesTex);
		faces.Add (BlockSide.FrontRight, cornerTex);
		faces.Add (BlockSide.FrontLeft, cornerTex);
		faces.Add (BlockSide.BackRight, cornerTex);
		faces.Add (BlockSide.BackLeft, cornerTex);
	}
	public void SetTextures (Vector2 leftTex, Vector2 rightTex, Vector2 topTex, Vector2 botTex, Vector2 frontTex, Vector2 backTex) {
		faces.Add (BlockSide.Bot, botTex);
		faces.Add (BlockSide.Top, topTex);
		faces.Add (BlockSide.Back, backTex);
		faces.Add (BlockSide.Front, frontTex);
		faces.Add (BlockSide.Left, leftTex);
		faces.Add (BlockSide.Right, rightTex);
	}
}